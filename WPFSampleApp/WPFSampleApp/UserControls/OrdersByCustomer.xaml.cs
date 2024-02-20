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
    /// Interaction logic for OrdersByCustomer.xaml
    /// </summary>
    public partial class OrdersByCustomer : UserControl
    {
        IDataAccessAPI DataAccessAPI = null;
        string CustomerID = null;
        ContentControl contentControl;

        public OrdersByCustomer(IDataAccessAPI DataAccessAPI, string CustomerID, ContentControl contentControl)
        {
            InitializeComponent();

            this.DataAccessAPI = DataAccessAPI;
            this.CustomerID = CustomerID;
            this.contentControl = contentControl;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var OrdersByCustomer = DataAccessAPI.GetOrdersByCustomerID(CustomerID);
            var FirstOrder = OrdersByCustomer.FirstOrDefault(t => t.CustomerID == CustomerID);  // all records likely have this
            if (FirstOrder != null)
            {
                ReportTitle.Text = string.Format($"Sales orders for {FirstOrder.Customer.CompanyName}");
            }
            else
            {
                ReportTitle.Text = string.Format($"Customer not found in database!");
            }

            OrdersGrid.ItemsSource = OrdersByCustomer;
        }
    }
}
