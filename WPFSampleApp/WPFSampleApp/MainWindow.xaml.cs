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
            PrimaryContent.Content = new Categories(DataAccessAPI, SecondaryContent);
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

        private void AdjustDBDatesToday_Click(object sender, RoutedEventArgs e)
        {
            var bResult = MessageBox.Show(
                "This operation will adjust all dates in the NorthWind demo database to be relative to today.  Do you want to continue?",
                "Adjust Dates?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (bResult == MessageBoxResult.Yes)
            {
                // get the most recent date in all the database records.
                var latestDate = DataAccessAPI.GetLatestDateInDatabase();

                // generate a datetime with today's date and latestDate time (we only want to adjust days)
                DateTime Now = DateTime.Now;
                DateTime Current = new DateTime(Now.Year, Now.Month, Now.Day, latestDate.Hour, latestDate.Minute, latestDate.Second);

                // calculate the difference between current date and latestDate.
                var diff = Current - latestDate;

                // diff.Days contains the number of days to adjust each datetime value in the database.  The latestDate will be set to today.
                DataAccessAPI.AdjustAllDatesInDatabaseByDays(diff.Days);
            }
        }
    }
}
