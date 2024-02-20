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

namespace WPFSampleApp.UserControls
{
    /// <summary>
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Customers : UserControl
    {
        IDataAccessAPI DataAccessAPI = null;
        ContentControl contentControl;

        public Customers(IDataAccessAPI DataAccessAPI, ContentControl contentControl)
        {
            InitializeComponent();

            this.DataAccessAPI = DataAccessAPI;
            this.contentControl = contentControl;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var AllCustomers = DataAccessAPI.GetAllCustomers();
            CustomerGrid.ItemsSource = AllCustomers;
        }

        private void OrdersByCustomer_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null)
                return;

            if (btn.Tag.GetType() != typeof(System.String))
                return;

            string customerID = (string)btn.Tag;

            contentControl.Content = new OrdersByCustomer(DataAccessAPI, customerID, contentControl);

            return;
        }

    }
}
