using IfcBimToolkitApp.Pages;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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