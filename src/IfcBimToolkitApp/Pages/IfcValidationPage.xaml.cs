// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

using DataStandardRepository.Actions.LiteDB;
using DataStandardRepository.Models;
using IfcModelValidator.Handlers.Ifc;
using IfcModelValidator.Validation;
using IfcModelValidator.Writers;
using IfcModelValidator.Writers.Csv;
using LiteDB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml.Serialization;

using Xbim.Ifc4x3;
using Xbim.Ifc4;
using BimCollabRulesCreator.Models.ClashRules;
using BimCollabRulesCreator;
using Xbim.Ifc4.Interfaces;
using DataStandardRepository.Writers;
using IfcBimToolkitApp.Mappers;
using OfficeOpenXml;


namespace IfcBimToolkitApp.Pages;
/// <summary>
/// Interaction logic for IfcValidationPage.xaml
/// </summary>
public partial class IfcValidationPage : Page
{
    private static bool _success;
    public IfcValidationPage()
    {
        InitializeComponent();
        this.ProgressBar.Visibility = Visibility.Hidden;
        this.LblProgressBar.Visibility = Visibility.Hidden;
    }
    private void TxtBoxInput_GotFocus(object sender, RoutedEventArgs e)
    {
        TxtBoxInput_ObjectType.Text = "";
    }

    #region Button Actions
    private async void Btn_Click_ValidateAgainstDataStandard(object sender, RoutedEventArgs e)
    {
        Stopwatch st = new();
        st.Start();
        bool withPsets = CheckBox_WithPsets.IsChecked ?? false;

        OpenFileDialog ofdIfc = new()
        {
            Title = "Vyberte soubory IFC pro validaci.",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            RestoreDirectory = true,
            Multiselect = true,
            Filter = "IFC Files|*.ifc" + "|All Files|*.*"
        };
        ofdIfc.ShowDialog();
        var ifcObjectTypePropertyName = TxtBoxInput_ObjectType.Text;
        var ifcElementTypePropertyName = TxtBoxInput_ElementType.Text;
        var projectStage = TxtBoxInput_ProjectStage.Text;
        var profession = TxtBoxInput_Proffession.Text;
        var selectedIfcFilePaths = ofdIfc.FileNames;

        if (selectedIfcFilePaths.Length == 0)
        {
            ShowErrorLine("Musíte vybrat alespoň 1 soubor IFC.");
            return;
        }

        OpenFileDialog ofdDb = new()
        {
            Title = "Vyberte soubor DB s uloženým datovým standardem nebo soubor IDS.",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            RestoreDirectory = true,
            Filter = "DB Files|*.db" +
                     "|IDS Files|*.ids" +
                     "|All Files|*.*",
            FilterIndex = 0,
        };
        ofdDb.ShowDialog();
        var filePath = ofdDb.FileName;

        foreach (var ifcFilePath in selectedIfcFilePaths)
        {
            var ifcFileName = ifcFilePath.Split("\\").Last();
            IfcParser ifcParser = new(ifcFilePath);
            // OPATRNĚ SOS112
            //profession = ifcFilePath.Substring(ifcFilePath.Length - 7, 3);
            //

            //var parts = ifcFilePath.Split('_');
            //profession = '_' + parts[parts.Length - 2];

            LblProgressBar.Content = $"({ifcFileName}) Načítám parametry elementů z IFC modelu...";
            LblProgressBar.Visibility = Visibility.Visible;
            ProgressBar.Visibility = Visibility.Visible;

            var elementsTask = GetAllElementsWithPropertiesAsync(ifcParser);
            await elementsTask;
            var parsingOutput = elementsTask.Result;

            if (!_success)
            {
                ShowErrorLine("Nebyly nalezeny žádné Elementy v modelu.");
                return;
            }

            ProgressBar.Visibility = Visibility.Hidden;
            LblProgressBar.Content = $"({ifcFileName}) Zapisuji výsledek do souboru...";
            ProgressBar.Visibility = Visibility.Visible;

            IValidationSummary validationSummary = new CsvValidationSummary();
            // IValidationSummary validationSummary = new ExcelValidationSummary();
            WriteManager writeManager = new(ifcFilePath, validationSummary);

            if (filePath.Contains(".db"))
            {
                var validationManager = new DbValidationManager(parsingOutput,
                    filePath, ifcObjectTypePropertyName, ifcElementTypePropertyName, projectStage, profession);
                await ValidateIfcElementsAgainstDataStandardAsync(writeManager, validationManager, withPsets);
            }
            else
            {
                var validationManager = new IdsValidationManager(parsingOutput, filePath);
                await ValidateIfcElementsAgainstDataStandardAsync(writeManager, validationManager, true);
            }
        }

        ProgressBar.Visibility = Visibility.Hidden;
        st.Stop();
        LblProgressBar.Content =
            $"HOTOVO! Zkontrolovány elementy modelu s vlastnostmi. (Čas {st.ElapsedMilliseconds / 1000} s)";
    }
    private async void Btn_Click_GetDataStandardFromExcel(object sender, RoutedEventArgs e)
    {
        Stopwatch st = new();
        st.Start();

        OpenFileDialog xlsxOfd = new()
        {
            Title = "Otevřít Excel s datovým standardem.",
            Filter = "Excel Files|*.xlsx" +
                     "|All Files|*.*"
        };
        xlsxOfd.ShowDialog();

        if (xlsxOfd.FileName == "")
        {
            ShowErrorLine("Musíte vybrat alespoň 1 soubor XLSX.");
            return;
        }

        SaveFileDialog dbSfd = new()
        {
            Title = "Vyberte soubor DB pro uložení datového standardu.",
            Filter = "LiteDB Files|*.db" +
                     "|All Files|*.*"
        };
        dbSfd.ShowDialog();

        if (dbSfd.FileName == "")
        {
            ShowErrorLine("Musíte zvolit jméno souboru pro uložení datového standardu.");
            return;
        }

        LblProgressBar.Content = "Načítám datový standard z Excelu...";
        LblProgressBar.Visibility = Visibility.Visible;
        ProgressBar.Visibility = Visibility.Visible;

        await GetDataStandardFromExcelAsync(xlsxOfd.FileName, dbSfd.FileName);

        if (!_success)
        {
            ShowErrorLine("Něco se asi nepovedlo.");
            return;
        }

        ProgressBar.Visibility = Visibility.Hidden;
        st.Stop();
        LblProgressBar.Content = $"HOTOVO! Datový standard načten. (Čas {st.ElapsedMilliseconds / 1000} s)";
    }
    private async void Btn_Click_ExportDataStandardToExcel(object sender, RoutedEventArgs e)
    {
        Stopwatch st = new();
        st.Start();

        OpenFileDialog ofd = new()
        {
            Title = "Otevřít databázový soubor s datovým standardem.",
            Filter = "Database Files|*.db" +
                     "|All Files|*.*"
        };
        ofd.ShowDialog();

        if (ofd.FileName == "")
        {
            ShowErrorLine("Musíte vybrat alespoň 1 soubor DB.");
            return;
        }

        LblProgressBar.Content = "Načítám datový standard z databáze...";
        LblProgressBar.Visibility = Visibility.Visible;
        ProgressBar.Visibility = Visibility.Visible;

        OpenFileDialog sfd = new()
        {
            Title = "Vyberte soubor XLSX pro uložení datového standardu..",
            Filter = "Excel Files|*.xlsx" +
                     "|All Files|*.*"
        };
        sfd.ShowDialog();

        if (sfd.FileName == "")
        {
            ShowErrorLine("Musíte zvolit jméno souboru pro uložení datového standardu.");
            return;
        }
        using LiteDatabase db = new(ofd.FileName);
        var collection = CollectionRetrieval<ElementModel>.GetCollection(db, "elements");
        await ExportDataStandardToExcelAsync(collection, sfd.FileName);

        if (!_success)
        {
            ShowErrorLine("Něco se nepovedlo.");
            return;
        }

        ProgressBar.Visibility = Visibility.Hidden;
        st.Stop();
        LblProgressBar.Content = $"HOTOVO! Datový standard exportován. (Čas {st.ElapsedMilliseconds / 1000} s)";
    }
    private async void Btn_Click_ExportRevitMappingTable(object sender, RoutedEventArgs e)
    {
        Stopwatch st = new();
        st.Start();

        OpenFileDialog ofd = new()
        {
            Filter = "Database Files|*.db" +
                     "|All Files|*.*"
        };
        ofd.ShowDialog();

        if (ofd.FileName == "")
        {
            ShowErrorLine("Musíte vybrat alespoň 1 soubor DB.");
            return;
        }

        SaveFileDialog sfd = new()
        {
            Title = "Vyberte soubor TXT pro uložení mapovací tabulky.",
            Filter = "TXT Files|*.txt" +
                     "|All Files|*.*"
        };
        sfd.ShowDialog();

        if (sfd.FileName == "")
        {
            ShowErrorLine("Musíte zvolit jméno souboru pro uložení datového standardu.");
            return;
        }

        LblProgressBar.Content = "Exportuji mapovací tabulku pro Revit z databáze...";
        LblProgressBar.Visibility = Visibility.Visible;
        ProgressBar.Visibility = Visibility.Visible;

        using LiteDatabase db = new(ofd.FileName);
        var collection = CollectionRetrieval<ParameterModel>.GetCollection(db, "parameters");
        await ExportRevitMappingTableAsync(collection, sfd.FileName);

        if (!_success)
        {
            ShowErrorLine("Něco se nepovedlo.");
            return;
        }

        ProgressBar.Visibility = Visibility.Hidden;
        st.Stop();
        LblProgressBar.Content = $"HOTOVO! Mapovací tabulka vyexportována. (Čas {st.ElapsedMilliseconds / 1000} s)";
    }    
    #endregion

    #region Async Tasks
    private static async Task ValidateIfcElementsAgainstDataStandardAsync(WriteManager writeManager,
        IValidationManager validationManager,
        bool withPsets)
    {
        await Task.Run(() =>
        {
            Stopwatch st = new();
            st.Start();
            var validatedElements = validationManager.GetValidation(withPsets);

            st.Stop();
            var t1 = st.ElapsedMilliseconds / 1000.000;
            st.Start();
            if (validatedElements.Any())
                _success = writeManager.WriteValidationSummary(validatedElements);
            st.Stop();
            var t2 = st.ElapsedMilliseconds / 1000.000;
            Thread.Sleep(1000);
        });
    }
    private static async Task<IEnumerable<IIfcElement>> GetAllElementsWithPropertiesAsync(IfcParser ifcParser)
    {
        IEnumerable<IIfcElement> result = [];
        await Task.Run(() =>
        {
            result = ifcParser.GetElementsWithProperties(out _success);
            Thread.Sleep(1000);
        });
        return result;
    }
    private static async Task GetDataStandardFromExcelAsync(string inputFilePath,
        string outputFilePath)
    {
        await Task.Run(() =>
        {
            var inputManager = new DataStandardRepository.Parsers.Excel.InputManager(inputFilePath, 3);
            var data = inputManager.GetAllElements();
            var dbImporter = new ImportDataStandard(outputFilePath, data);
            _success = dbImporter.CreateCompleteDataStandard();
        });
    }
    private static async Task ExportDataStandardToExcelAsync(IEnumerable<ElementModel> collection,
        string outputFilePath)
    {
        await Task.Run(() =>
        {
            var writer = new ExcelWriter();
            _success = writer.WriteDataStandard(collection, outputFilePath);
        });
    }
    private static async Task ExportRevitMappingTableAsync(IEnumerable<ParameterModel> collection,
        string inputFilePath)
    {
        await Task.Run(() =>
        {
            _success = RevitMappingTable.CreateMappingTable(collection, inputFilePath);

            Thread.Sleep(1000);
        });
    }
    #endregion  
    
    private void ShowErrorLine(string message)
    {
        LblProgressBar.Visibility = Visibility.Visible;
        LblProgressBar.Foreground = new SolidColorBrush(Colors.Red);
        LblProgressBar.FontSize = 14;
        LblProgressBar.Content = message;
    }
}
