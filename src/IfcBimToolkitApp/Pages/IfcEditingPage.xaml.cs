// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

using System.Diagnostics;
using System.IO;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using IfcModelValidator;
using IfcModelHandler.Elements;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.MeasureResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.PropertyResource;
using Xbim.Ifc4.SharedBldgElements;
using Xbim.Ifc4.SharedComponentElements;
using IfcBimToolkitApp.Commands;

namespace IfcBimToolkitApp.Pages;
/// <summary>
/// Interaction logic for IfcEditingPage.xaml
/// </summary>
public partial class IfcEditingPage : Page
{
    public IfcEditingPage()
    {
        InitializeComponent();
        this.ProgressBar.Visibility = System.Windows.Visibility.Hidden;
        this.LblProgressBar.Visibility = System.Windows.Visibility.Hidden;
    }

    private void Btn_Click_TestButton(object sender, RoutedEventArgs e)
    {
        Stopwatch st = new();
        st.Start();
        ProgressBar.Visibility = Visibility.Visible;
        Thread.Sleep(100);

        var input = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MSKP_OCEL.ifc");
        var output = input.Replace(".ifc", ".done.ifc");

        AddPropertiesWithValuesCommand.AddPropertiesWithData(input, output);

        st.Stop();
        LblProgressBar.Visibility = Visibility.Visible;
        LblProgressBar.Content = $"Hotovo. ({st.ElapsedMilliseconds / 1000}s)";
    }
}
