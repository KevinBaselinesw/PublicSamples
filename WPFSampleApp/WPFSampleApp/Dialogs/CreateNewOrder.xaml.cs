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

            EnteredProducts = null;
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

            if (EnteredProducts == null || EnteredProducts.Count() == 0)
            {
                MessageBox.Show("No products have been entered for this order");
                return;
            }


            OrderDTO newOrder = new OrderDTO();
            newOrder.EmployeeID = cbe.id;
            newOrder.CustomerID = customer.CustomerID;
            newOrder.OrderDate = DateTime.Now;
            newOrder.RequiredDate = ShippingDate.SelectedDate ?? DateTime.Now.AddDays(3);

            newOrder.ShipVia = shipper.ShipperID;
            newOrder.Order_Details = EnteredOrderDetails();


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

        private List<Order_DetailDTO> EnteredOrderDetails()
        {
            List<Order_DetailDTO> EnteredOrders = new List<Order_DetailDTO>();

            foreach (var enteredProduct in EnteredProducts)
            {
                Order_DetailDTO Order = new Order_DetailDTO();
                Order.ProductID = enteredProduct.ProductID;
                Order.Quantity = enteredProduct.Quantity.Value;
                Order.UnitPrice = enteredProduct.UnitPrice.Value;
                Order.Discount = 0.0f;
                EnteredOrders.Add(Order);
            }

            return EnteredOrders;
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                if (btn.Tag != null)
                {
                    int ProductID = (int)btn.Tag;

                    var RemovedItem = EnteredProducts.FirstOrDefault(t => t.ProductID == ProductID);
                    EnteredProducts.Remove(RemovedItem);
                }

            }
            return;
        }


        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            if (EnteredProducts == null)
            {
                EnteredProducts = new ObservableCollection<CreateNewOrderInfo>();
                ProductGrid.ItemsSource = EnteredProducts;
            }
            else
            {
                EnteredProducts.Add(new CreateNewOrderInfo());
            }
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


                decimal OrderTotal = 0;
                foreach (var enteredProduct in EnteredProducts)
                {
                    if (enteredProduct.SubTotal != null)
                    {
                        OrderTotal += enteredProduct.SubTotal.Value;
                    }
                }

                OrderTotalTB.Text = string.Format("Order Total: ${0:0.00}", OrderTotal);
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

        public short? _Quantity;
        public short? Quantity
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
