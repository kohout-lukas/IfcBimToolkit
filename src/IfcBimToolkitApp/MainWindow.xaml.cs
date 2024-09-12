// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

using IfcBimToolkitApp.Pages;
using System.Windows;

namespace IfcBimToolkitApp;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private void Btn_Click_IfcValidation(object sender, RoutedEventArgs e)
    {
        Frame_Content.Content = new IfcValidationPage();
    }
    private void Btn_Click_IfcEditing(object sender, RoutedEventArgs e)
    {
        Frame_Content.Content = new IfcEditingPage();
    }
    private void Btn_Click_BimcollabTools(object sender, RoutedEventArgs e)
    {
        Frame_Content.Content = new BimcollabToolsPage();
    }
    private void Btn_Click_RevitTools(object sender, RoutedEventArgs e)
    {
        Frame_Content.Content = new RevitToolsPage();
    }
}