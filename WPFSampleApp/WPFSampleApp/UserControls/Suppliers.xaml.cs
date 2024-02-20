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
    /// Interaction logic for Suppliers.xaml
    /// </summary>
    public partial class Suppliers : UserControl
    {
        IDataAccessAPI DataAccessAPI = null;
        ContentControl contentControl;
        IEnumerable<Supplier> AllSuppliers;

        public Suppliers(IDataAccessAPI DataAccessAPI, ContentControl contentControl)
        {
            InitializeComponent();
            this.DataAccessAPI = DataAccessAPI;
            this.contentControl = contentControl;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            AllSuppliers = DataAccessAPI.GetAllSuppliers();
            SuppliersGrid.ItemsSource = AllSuppliers;
        }

        private void ProductsBySupplier_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null)
                return;

            if (btn.Tag.GetType() != typeof(System.Int32))
                return;

            int supplierID = (int)btn.Tag;

            var OrdersByShipper = DataAccessAPI.GetProductsBySupplier(supplierID);

            var supplier = AllSuppliers.First(t => t.SupplierID == supplierID);

            string Message = string.Format($"There are {OrdersByShipper.Count()} products from {supplier.CompanyName}");

            contentControl.Content = new SimpleText(Message);


            return;
        }
    }
}
