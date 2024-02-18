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

        public Shippers(IDataAccessAPI DataAccessAPI)
        {
            InitializeComponent();
            this.DataAccessAPI = DataAccessAPI;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var AllShippers = DataAccessAPI.GetAllShippers();
            SuppliersGrid.ItemsSource = AllShippers;
        }
    }
}
