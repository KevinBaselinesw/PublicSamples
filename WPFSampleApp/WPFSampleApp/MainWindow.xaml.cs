using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WPFSampleApp.UserControls;

namespace WPFSampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Employees_Click(object sender, RoutedEventArgs e)
        {
            NorthwindsContent.Content = new Employees();
        }

        private void ProductCategories_Click(object sender, RoutedEventArgs e)
        {
            NorthwindsContent.Content = new ProductCategories();
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            NorthwindsContent.Content = new Products();
        }

        private void Customers_Click(object sender, RoutedEventArgs e)
        {
            NorthwindsContent.Content = new Customers();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            NorthwindsContent.Content = new Orders();
        }

        private void Suppliers_Click(object sender, RoutedEventArgs e)
        {
            NorthwindsContent.Content = new Suppliers();
        }

        private void Shippers_Click(object sender, RoutedEventArgs e)
        {
            NorthwindsContent.Content = new Shippers();
        }
    }
}
