/*
 * BSD 3-Clause License
 *
 * Copyright (c) 2024
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice,
 *    this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution.
 *
 * 3. Neither the name of the copyright holder nor the names of its
 *    contributors may be used to endorse or promote products derived from
 *    this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

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
using UtilityFunctions;
using WCFSampleClient.UserControls;
using WCFSampleClient.WCFSampleService;

namespace WCFSampleClient.Dialogs
{
    /// <summary>
    /// Interaction logic for CreateNewOrder.xaml
    /// </summary>
    public partial class CreateNewOrder : Window
    {
        WCFType WCFType;

        ObservableCollection<CreateNewOrderInfo> EnteredProducts;

        public CreateNewOrder(WCFType WCFType)
        {
            this.WCFType = WCFType;
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (WCFType == WCFType.SOAP)
            {
                WCFSampleServiceClient Client = null;

                try
                {
                    Client = new WCFSampleService.WCFSampleServiceClient();
                    var Employees = Client.GetAllEmployees();
                    SalesmanCB.ItemsSource = Convert(Employees);
                    SalesmanCB.DisplayMemberPath = "Name";

                    var Customers = Client.GetAllCustomers();
                    CustomerCB.ItemsSource = Customers;
                    CustomerCB.DisplayMemberPath = "CompanyName";

                    var Shippers = Client.GetAllShippers();
                    ShipperCB.ItemsSource = Shippers;
                    ShipperCB.DisplayMemberPath = "CompanyName";

                    EnteredProducts = null;
                    ProductGrid.ItemsSource = EnteredProducts;

                    ProductNameCB.ItemsSource = Client.GetAllProducts();
                }
                catch (Exception)
                {
                    if (Client != null)
                    {
                        Client.Abort();
                    }
                }
            }

            if (WCFType == WCFType.REST)
            {
                RESTClient RestClient = new RESTClient(App.NorthwindsServerBaseURL);

                var Employees = await RestClient.Get<List<EmployeeDTO>>("GetAllEmployees", null);
                SalesmanCB.ItemsSource = Convert(Employees);
                SalesmanCB.DisplayMemberPath = "Name";

                var Customers = await RestClient.Get<List<CustomerDTO>>("GetAllCustomers", null);
                CustomerCB.ItemsSource = Customers;
                CustomerCB.DisplayMemberPath = "CompanyName";

                var Shippers = await RestClient.Get<List<ShipperDTO>>("GetAllShippers", null);
                ShipperCB.ItemsSource = Shippers;
                ShipperCB.DisplayMemberPath = "CompanyName";


                EnteredProducts = null;
                ProductGrid.ItemsSource = EnteredProducts;

                ProductNameCB.ItemsSource = await RestClient.Get<List<ProductDTO>>("GetAllProducts", null);

                WCFSampleServiceClient Client = null;

                try
                {
                    Client = new WCFSampleService.WCFSampleServiceClient();
   


                    EnteredProducts = null;
                    ProductGrid.ItemsSource = EnteredProducts;

                    ProductNameCB.ItemsSource = Client.GetAllProducts();
                }
                catch (Exception)
                {
                    if (Client != null)
                    {
                        Client.Abort();
                    }
                }
            }


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

        private async void SaveOrderButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxEmployee cbe = SalesmanCB.SelectedValue as ComboBoxEmployee;
            if (cbe == null)
            {
                MessageBox.Show("Must select an employee for this order", "Create New Order", MessageBoxButton.OK);
                return;
            }

            CustomerDTO customer = CustomerCB.SelectedValue as CustomerDTO;
            if (customer == null)
            {
                MessageBox.Show("Must select a customer for this order", "Create New Order", MessageBoxButton.OK);
                return;
            }

            ShipperDTO shipper = ShipperCB.SelectedValue as ShipperDTO;
            if (shipper == null)
            {
                MessageBox.Show("Must select a shipper for this order", "Create New Order", MessageBoxButton.OK);
                return;
            }

            if (EnteredProducts == null || EnteredProducts.Count() == 0)
            {
                MessageBox.Show("No products have been entered for this order", "Create New Order", MessageBoxButton.OK);
                return;
            }


            OrderDTO newOrder = new OrderDTO();
            newOrder.EmployeeID = cbe.id;
            newOrder.CustomerID = customer.CustomerID;
            newOrder.OrderDate = DateTime.Now;
            newOrder.RequiredDate = ShippingDate.SelectedDate ?? DateTime.Now.AddDays(3);

            newOrder.ShipVia = shipper.ShipperID;

            if (ValidateEnteredOrderDetails() < 0)
                return;

            newOrder.Order_Details = EnteredOrderDetails();

            // Fill out the rest of the order.  Doesn't appear to be used
            newOrder.Freight = 9.99m;
            newOrder.ShipName = "Kevin McKenna";
            newOrder.ShipAddress = "1600 Pennsylvania Avenue";
            newOrder.ShipCity = "Washington";
            newOrder.ShipRegion = "DC";
            newOrder.ShipPostalCode = "99999";
            newOrder.ShipCountry = "USA";
            // Fill out the rest of the order.  Doesn't appear to be used

            if (WCFType == WCFType.SOAP)
            {
                WCFSampleServiceClient Client = null;

                try
                {
                    Client = new WCFSampleService.WCFSampleServiceClient();
                    newOrder = Client.CreateNewOrder(newOrder);
                    return;
                }
                catch (Exception)
                {
                    if (Client != null)
                    {
                        Client.Abort();
                    }
                }
            }


            if (WCFType == WCFType.REST)
            {
                try
                {
                    RESTClient RestClient = new RESTClient(App.NorthwindsServerBaseURL);
                    newOrder = await RestClient.Post<OrderDTO, OrderDTO>("CreateNewOrder", newOrder);
                }
                catch
                {

                }

            }

            if (newOrder != null && newOrder.OrderID > 0)
            {
                MessageBox.Show("Order successfully added to system", "Create New Order", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Order not added to database!", "Create New Order", MessageBoxButton.OK);
            }

            return;

        }

        private int ValidateEnteredOrderDetails()
        {
            foreach (var enteredProduct in EnteredProducts)
            {
                if (enteredProduct.ProductID <= 0)
                    continue;

                if (enteredProduct.Quantity == null || enteredProduct.Quantity <= 0)
                {
                    MessageBox.Show($"{enteredProduct.ProductName} has an invalid quantity!");
                    return -1; 
                }

                if (enteredProduct.SubTotal == null || enteredProduct.SubTotal <= 0)
                {
                    MessageBox.Show($"{enteredProduct.ProductName} has an invalid subtotal!");
                    return -1;
                }
  
            }

            EnteredProducts = new ObservableCollection<CreateNewOrderInfo>(EnteredProducts.Where(t=>t.ProductID > 0));
            return 0;
        }

        private Order_DetailDTO[] EnteredOrderDetails()
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

            return EnteredOrders.ToArray();
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
