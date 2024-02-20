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
    /// Interaction logic for OrdersByEmployee.xaml
    /// </summary>
    public partial class OrdersByEmployee : UserControl
    {
        IDataAccessAPI DataAccessAPI = null;
        int EmployeeID = 0;
        ContentControl contentControl;

        public OrdersByEmployee(IDataAccessAPI DataAccessAPI, int EmployeeID, ContentControl contentControl)
        {
            InitializeComponent();

            this.DataAccessAPI = DataAccessAPI;
            this.EmployeeID = EmployeeID;
            this.contentControl = contentControl;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var OrdersByEmployee = DataAccessAPI.GetOrdersByEmployeeID(EmployeeID);
            var FirstOrder = OrdersByEmployee.FirstOrDefault(t => t.EmployeeID == EmployeeID);  // all records likely have this
            if (FirstOrder != null)
            {
                ReportTitle.Text = string.Format($"Sales orders for {FirstOrder.Employee.FirstName} {FirstOrder.Employee.LastName}");
            }
            else
            {
                ReportTitle.Text = string.Format($"Employee not found in the database!");
            }

            OrdersGrid.ItemsSource = OrdersByEmployee;
        }
    }
}
