// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

using BimCollabRulesCreator;
using DataStandardRepository.Actions.LiteDB;
using DataStandardRepository.Models;
using IfcBimToolkitApp.Mappers;
using LiteDB;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;

namespace IfcBimToolkitApp.Pages;
/// <summary>
/// Interaction logic for BimcollabToolsPage.xaml
/// </summary>
public partial class BimcollabToolsPage : Page
{
    public BimcollabToolsPage()
    {
        InitializeComponent();
        this.ProgressBar.Visibility = Visibility.Hidden;
        this.LblProgressBar.Visibility = Visibility.Hidden;
    }
    private void Btn_Click_CreateBimCollabSmartViews(object sender, RoutedEventArgs e)
    {
        Stopwatch st = new();
        st.Start();

        OpenFileDialog ofd = new()
        {
            Title = "Vyberte soubor s datovým standardem.",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            RestoreDirectory = true,
            Multiselect = false,
            Filter = "DB Files|*.db" + "|All Files|*.*"
        };
        ofd.ShowDialog();

        var dbFilePath = ofd.FileName;

        if (dbFilePath is null)
        {
            ShowErrorLine("Musíte vybrat soubor se standardem.");
            return;
        }
        SaveFileDialog sfd = new()
        {
            Title = "Vyberte soubor BCSV pro uložení pravidel.",
            Filter = "BCSV Files|*bcsv" +
                     "|All Files|*.*"
        };
        sfd.ShowDialog();

        if (sfd.FileName == "")
        {
            ShowErrorLine("Musíte zvolit jméno souboru pro uložení pravidel.");
            return;
        }

        LblProgressBar.Content = "Načítám parametry elementů...";
        LblProgressBar.Visibility = Visibility.Visible;
        ProgressBar.Visibility = Visibility.Visible;
        using LiteDatabase db = new(ofd.FileName);
        var data = CollectionRetrieval<ElementModel>.GetCollection(db, "elements");
        bool separateElements = CheckBox_SeparateElements.IsChecked ?? false;
        bool showIncorrect = CheckBox_ShowIncorrect.IsChecked ?? false;

        ProgressBar.Visibility = Visibility.Hidden;
        LblProgressBar.Content = "Zapisuji do Excelu...";
        ProgressBar.Visibility = Visibility.Visible;

        CreateBimCollabRules(sfd.FileName, data, separateElements, showIncorrect);

        ProgressBar.Visibility = Visibility.Hidden;
        st.Stop();
        LblProgressBar.Content = $"HOTOVO! (Čas {st.ElapsedMilliseconds / 1000} s)";
    }
    private static void CreateBimCollabRules(string outputPath,
        IEnumerable<ElementModel> input,
        bool separateElements,
        bool onlyIncorrect)
    {
        var data = ClassMapper.MapElementModelToRuleElement(input);
        SmartViewSetFactory factory = new(outputPath.Split("\\").Last(), data, separateElements, onlyIncorrect);
        var root = factory.CreateSmartViewSet();

        XmlSerializerNamespaces ns = new();
        ns.Add("", "");

        XmlSerializer xs = new(root.GetType());
        using TextWriter tw = new StreamWriter(outputPath);
        xs.Serialize(tw, root, ns);
        tw.Close();
        tw.Dispose();

        var oldFile = File.ReadAllLines(outputPath).ToList();
        var newFile = oldFile.Where(x => !x.Contains("ROOT_TO_DELETE")).ToArray();
        File.WriteAllLines(outputPath, newFile);
    }
    private void ShowErrorLine(string message)
    {
        LblProgressBar.Visibility = Visibility.Visible;
        LblProgressBar.Foreground = new SolidColorBrush(Colors.Red);
        LblProgressBar.FontSize = 14;
        LblProgressBar.Content = message;
    }
}
