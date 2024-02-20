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
    /// Interaction logic for Employees.xaml
    /// </summary>
    public partial class Employees : UserControl
    {
        IDataAccessAPI DataAccessAPI = null;
        ContentControl contentControl;
        IEnumerable<Employee> AllEmployees;

        public Employees(IDataAccessAPI DataAccessAPI, ContentControl contentControl)
        {
            InitializeComponent();

            this.DataAccessAPI = DataAccessAPI;
            this.contentControl = contentControl;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            AllEmployees = DataAccessAPI.GetAllEmployees();
            EmployeeGrid.ItemsSource = AllEmployees;
        }

        private void OrdersByEmployee_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null)
                return;

            if (btn.Tag.GetType() != typeof(System.Int32))
                return;

            int employeeID = (int)btn.Tag;

            var OrdersByEmployee = DataAccessAPI.GetOrdersByEmployeeID(employeeID);
            var employee = AllEmployees.First(t => t.EmployeeID == employeeID);

            string Message = string.Format($"There are {OrdersByEmployee.Count()} orders for {employee.FirstName} {employee.LastName}");

            contentControl.Content = new SimpleText(Message);

            return;
        }
    }
}
