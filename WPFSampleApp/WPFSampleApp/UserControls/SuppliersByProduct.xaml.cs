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
    /// Interaction logic for SuppliersByProduct.xaml
    /// </summary>
    public partial class SuppliersByProduct : UserControl
    {
        IDataAccessAPI DataAccessAPI = null;
        int SupplierID;
        ContentControl contentControl;
        IEnumerable<Supplier> AllSuppliers;

        public SuppliersByProduct(IDataAccessAPI DataAccessAPI, int SupplierID, ContentControl contentControl)
        {
            InitializeComponent();
            this.DataAccessAPI = DataAccessAPI;
            this.SupplierID = SupplierID;
            this.contentControl = contentControl;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var Suppliers = DataAccessAPI.GetSuppliersByID(SupplierID);
            var Supplier = Suppliers.FirstOrDefault();
            if (Supplier != null)
            {
                ReportTitle.Text = string.Format($"The supplier for this product is {Supplier.CompanyName}");
            }
            else
            {
                ReportTitle.Text = string.Format($"The supplier for this product is unknown");
            }

            SuppliersGrid.ItemsSource = Suppliers;
        }

  
    }
}
