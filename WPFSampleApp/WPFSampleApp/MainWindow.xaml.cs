using DatabaseAccessLib;
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
        IDataAccessAPI DataAccessAPI;

        public MainWindow()
        {
            InitializeComponent();

            DataAccessAPI = new DataAccessAPIDB();
        }

        private void Employees_Click(object sender, RoutedEventArgs e)
        {
            NorthwindsContent.Content = new Employees(DataAccessAPI);
        }

        private void ProductCategories_Click(object sender, RoutedEventArgs e)
        {
            NorthwindsContent.Content = new ProductCategories(DataAccessAPI);
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            NorthwindsContent.Content = new Products(DataAccessAPI);
        }

        private void Customers_Click(object sender, RoutedEventArgs e)
        {
            NorthwindsContent.Content = new Customers(DataAccessAPI);
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            NorthwindsContent.Content = new Orders(DataAccessAPI);
        }

        private void Suppliers_Click(object sender, RoutedEventArgs e)
        {
            NorthwindsContent.Content = new Suppliers(DataAccessAPI);
        }

        private void Shippers_Click(object sender, RoutedEventArgs e)
        {
            NorthwindsContent.Content = new Shippers(DataAccessAPI);
        }
    }
}
