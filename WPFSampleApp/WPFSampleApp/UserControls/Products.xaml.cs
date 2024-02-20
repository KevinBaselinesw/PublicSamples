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
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : UserControl
    {
        IDataAccessAPI DataAccessAPI = null;
        ContentControl contentControl;

        public Products(IDataAccessAPI DataAccessAPI, ContentControl contentControl)
        {
            InitializeComponent();
            this.DataAccessAPI = DataAccessAPI;
            this.contentControl = contentControl;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var AllCategories = DataAccessAPI.GetAllProducts();
            ProductGrid.ItemsSource = AllCategories;
        }

        private void GetSupplier_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null)
                return;

            if (btn.Tag.GetType() != typeof(System.Int32))
                return;

            int supplierID = (int)btn.Tag;

            contentControl.Content = new SuppliersByProduct(DataAccessAPI, supplierID, contentControl);
            return;
        }

        private void GetCategory_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null)
                return;

            if (btn.Tag.GetType() != typeof(System.Int32))
                return;

            int categoryID = (int)btn.Tag;

            var categories = DataAccessAPI.GetProductCategoriesByID(categoryID);

            string Message = string.Format($"The category for this product is {categories.First().CategoryName}");

            contentControl.Content = new SimpleText(Message);

            return;
        }

        private void GetOrderDetails_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null)
                return;

            if (btn.Tag.GetType() != typeof(System.Int32))
                return;

            int productID = (int)btn.Tag;

            var orderDetails = DataAccessAPI.GetOrderDetailsByProductID(productID);

            string Message = string.Format($"There are {orderDetails.Count()} order details for {orderDetails.First().Product.ProductName}");

            contentControl.Content = new SimpleText(Message);

            return;
        }
    }
}
