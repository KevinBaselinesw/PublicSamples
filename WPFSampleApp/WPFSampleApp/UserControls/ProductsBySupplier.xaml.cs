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
    /// Interaction logic for ProductsBySupplier.xaml
    /// </summary>
    public partial class ProductsBySupplier : UserControl
    {
        IDataAccessAPI DataAccessAPI = null;
        int SupplierID = 0;
        ContentControl contentControl;

        public ProductsBySupplier(IDataAccessAPI DataAccessAPI, int SupplierID, ContentControl contentControl)
        {
            InitializeComponent();
            this.DataAccessAPI = DataAccessAPI;
            this.SupplierID = SupplierID;
            this.contentControl = contentControl;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var ProductsBySupplier = DataAccessAPI.GetProductsBySupplier(SupplierID);
            var FirstSupplier = DataAccessAPI.GetSuppliersByID(SupplierID).FirstOrDefault();
            if (FirstSupplier != null)
            {
                ReportTitle.Text = string.Format($"Products from {FirstSupplier.CompanyName}");
            }
            else
            {
                ReportTitle.Text = string.Format($"Supplier is not found in database!");
            }

            ProductGrid.ItemsSource = ProductsBySupplier;
        }
  
   
    }
}
