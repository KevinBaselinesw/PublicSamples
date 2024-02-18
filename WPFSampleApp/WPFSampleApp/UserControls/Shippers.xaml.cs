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
    /// Interaction logic for Shippers.xaml
    /// </summary>
    public partial class Shippers : UserControl
    {
        IDataAccessAPI DataAccessAPI = null;
        IEnumerable<Shipper> AllShippers;

        public Shippers(IDataAccessAPI DataAccessAPI)
        {
            InitializeComponent();
            this.DataAccessAPI = DataAccessAPI;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            AllShippers = DataAccessAPI.GetAllShippers();
            SuppliersGrid.ItemsSource = AllShippers;
        }

        private void OrdersByShipper_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null)
                return;

            if (btn.Tag.GetType() != typeof(System.Int32))
                return;

            int shipperID = (int)btn.Tag;
       
            var OrdersByShipper = DataAccessAPI.GetOrdersByShipVia(shipperID);

            var shipper = AllShippers.First(t => t.ShipperID == shipperID);

            MessageBox.Show(string.Format($"There are {OrdersByShipper.Count()} orders for {shipper.CompanyName}"));

            return;
        }
    }
}
