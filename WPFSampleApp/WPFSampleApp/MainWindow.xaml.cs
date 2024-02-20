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
            SecondaryContent.Content = null;
            gridSplitter.UpdateLayout();
            PrimaryContent.Content = new Employees(DataAccessAPI, SecondaryContent);
        }

        private void ProductCategories_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            PrimaryContent.Content = new ProductCategories(DataAccessAPI, SecondaryContent);
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            PrimaryContent.Content = new Products(DataAccessAPI, SecondaryContent);
        }

        private void Customers_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            PrimaryContent.Content = new Customers(DataAccessAPI, SecondaryContent);
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            PrimaryContent.Content = new Orders(DataAccessAPI, SecondaryContent);
        }

        private void Suppliers_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            PrimaryContent.Content = new Suppliers(DataAccessAPI, SecondaryContent);
        }

        private void Shippers_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            PrimaryContent.Content = new Shippers(DataAccessAPI, SecondaryContent);
        }
    }
}
