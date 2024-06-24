using DatabaseAccessLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace WPFSampleApp.Dialogs
{
    /// <summary>
    /// Interaction logic for CreateNewOrder.xaml
    /// </summary>
    public partial class CreateNewOrder : Window
    {
        IDataAccessAPI DataAccessAPI;
        ObservableCollection<ProductDTO> AllProducts;

        public CreateNewOrder(IDataAccessAPI DataAccessAPI)
        {
            this.DataAccessAPI = DataAccessAPI;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var Employees = DataAccessAPI.GetAllEmployees();
            SalesmanCB.ItemsSource = Convert(Employees);
            SalesmanCB.DisplayMemberPath = "Name";

            var Customers = DataAccessAPI.GetAllCustomers();
            CustomerCB.ItemsSource = Customers;
            CustomerCB.DisplayMemberPath = "CompanyName";

            var Shippers = DataAccessAPI.GetAllShippers();
            ShipperCB.ItemsSource = Shippers;
            ShipperCB.DisplayMemberPath = "CompanyName";

            AllProducts = new ObservableCollection<ProductDTO>(DataAccessAPI.GetAllProducts());
            ProductGrid.ItemsSource = AllProducts;
        }

        private IEnumerable<ComboBoxEmployee> Convert(IEnumerable<EmployeeDTO> employeeDTOs)
        {
            List<ComboBoxEmployee> CBEmployees = new List<ComboBoxEmployee>();

            foreach (var employeeDTO in employeeDTOs)
            {
                ComboBoxEmployee comboboxEmployee = new ComboBoxEmployee();
                comboboxEmployee.id = employeeDTO.EmployeeID;
                comboboxEmployee.Name = string.Format($"{employeeDTO.LastName}, {employeeDTO.FirstName}");

                CBEmployees.Add(comboboxEmployee);
            }

            return CBEmployees;
        }

        private void CancelOrderButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private void SaveOrderButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxEmployee cbe = SalesmanCB.SelectedValue as ComboBoxEmployee;
            if (cbe == null)
            {
                MessageBox.Show("Must select an employee for this order");
                return;
            }

            CustomerDTO customer = CustomerCB.SelectedValue as CustomerDTO;
            if (customer == null)
            {
                MessageBox.Show("Must select a customer for this order");
                return;
            }

            ShipperDTO shipper = ShipperCB.SelectedValue as ShipperDTO;
            if (shipper == null)
            {
                MessageBox.Show("Must select a shipper for this order");
                return;
            }


            OrderDTO newOrder = new OrderDTO();
            newOrder.EmployeeID = cbe.id;
            newOrder.CustomerID = customer.CustomerID;
            newOrder.OrderDate = DateTime.Now;
            newOrder.RequiredDate = DateTime.Now.AddDays(3);

            newOrder.ShipVia = shipper.ShipperID;
            newOrder.Order_Details = FakedOrderDetails();


            newOrder = DataAccessAPI.CreateNewOrder(newOrder);
            if (newOrder != null && newOrder.OrderID > 0)
            {
                MessageBox.Show("Order successfully added to system", "success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Order not added to database!", "failure", MessageBoxButton.OK);
            }

            return;

        }

        private List<Order_DetailDTO> FakedOrderDetails()
        {
            List<Order_DetailDTO> FakedOrders = new List<Order_DetailDTO>();

            Order_DetailDTO Order1 = new Order_DetailDTO();
            Order1.ProductID = 1;
            Order1.Quantity = 2;
            Order1.UnitPrice = 3.5m;
            Order1.Discount = 0.0f;
            FakedOrders.Add(Order1);

            Order_DetailDTO Order2 = new Order_DetailDTO();
            Order2.ProductID = 2;
            Order2.Quantity = 3;
            Order2.UnitPrice = 4.5m;
            Order2.Discount = 0.1f;
            FakedOrders.Add(Order2);

            return FakedOrders;
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                int ProductID = (int)btn.Tag;

                var RemovedItem = AllProducts.FirstOrDefault(t => t.ProductID == ProductID);
                AllProducts.Remove(RemovedItem);
               // ProductGrid.
            }
            return;
        }

        private void ProductGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            return;
        }

        private void ProductGrid_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            return;
        }
    }




    public class ComboBoxEmployee
    {
        public string Name { get; set; }
        public int id { get; set; }
    }
}
