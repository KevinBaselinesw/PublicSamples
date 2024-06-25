using DatabaseAccessLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        ObservableCollection<CreateNewOrderInfo> EnteredProducts;

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

            EnteredProducts = new ObservableCollection<CreateNewOrderInfo>();
            ProductGrid.ItemsSource = EnteredProducts;

            ProductNameCB.ItemsSource = DataAccessAPI.GetAllProducts();
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

                var RemovedItem = EnteredProducts.FirstOrDefault(t => t.ProductID == ProductID);
                EnteredProducts.Remove(RemovedItem);
            }
            return;
        }

        private void ProductGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.SortMemberPath == nameof(CreateNewOrderInfo.Quantity))
            {
                CreateNewOrderInfo OrderInfo = e.Row.DataContext as CreateNewOrderInfo;
                var QuantityTB = e.EditingElement as TextBox;

                int EnteredQuantity = 0;
                if (int.TryParse(QuantityTB.Text,  out EnteredQuantity))
                {
                    OrderInfo.SubTotal = OrderInfo.UnitPrice * EnteredQuantity;
                }
                else
                {
                    OrderInfo.SubTotal = 0;
                    OrderInfo.Quantity = 0;
                }

                
                return;
            }
            return;
        }

    
        private void ProductNameCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductDTO productDTO = e.AddedItems[0] as ProductDTO;

            var rowToModify = ProductGrid.CurrentItem as CreateNewOrderInfo;
    
            // modify what you want
            rowToModify.ProductID = productDTO.ProductID;
            rowToModify.ProductName = productDTO.ProductName;
            rowToModify.UnitPrice = productDTO.UnitPrice;
 
            e.Handled = true;

            return;
        }

    }

    public class ComboBoxEmployee
    {
        public string Name { get; set; }
        public int id { get; set; }
    }

    public class CreateNewOrderInfo : INotifyPropertyChanged
    {
        public CreateNewOrderInfo()
        {
        }

        // INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
   
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // INotifyPropertyChanged

        private int _ProductID;
        public int ProductID
        {
            get
            {
                return this._ProductID;
            }

            set
            {
                if (value != this._ProductID)
                {
                    this._ProductID = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ProductName { get; set; }

        public int? _Quantity;
        public int? Quantity
        {
            get
            {
                return this._Quantity;
            }

            set
            {
                if (value != this._Quantity)
                {
                    this._Quantity = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private decimal? _UnitPrice;
        public decimal? UnitPrice
        {
            get
            {
                return this._UnitPrice;
            }

            set
            {
                if (value != this._UnitPrice)
                {
                    this._UnitPrice = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private decimal? _SubTotal;
        public decimal? SubTotal
        {
            get
            {
                return this._SubTotal;
            }

            set
            {
                if (value != this._SubTotal)
                {
                    this._SubTotal = value;
                    NotifyPropertyChanged();
                }
            }
        }


    }


}
