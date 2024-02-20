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
    /// Interaction logic for OrdersByShipper.xaml
    /// </summary>
    public partial class OrdersByShipper : UserControl
    {
        IDataAccessAPI DataAccessAPI = null;
        int ShipperID = 0;
        ContentControl contentControl;

        public OrdersByShipper(IDataAccessAPI DataAccessAPI, int ShipperID, ContentControl contentControl)
        {
            InitializeComponent();

            this.DataAccessAPI = DataAccessAPI;
            this.ShipperID = ShipperID;
            this.contentControl = contentControl;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var OrdersByShipper = DataAccessAPI.GetOrdersByShipVia(ShipperID);
            var Shippers = DataAccessAPI.GetAllShippers();
            var FirstShipper = Shippers.FirstOrDefault(t => t.ShipperID == ShipperID);  // all records likely have this
            if (FirstShipper != null)
            {
                ReportTitle.Text = string.Format($"Orders sent by {FirstShipper.CompanyName}");
            }
            else
            {
                ReportTitle.Text = string.Format($"Shipping company not found in database!");
            }

            OrdersGrid.ItemsSource = OrdersByShipper;
        }
    }
}
