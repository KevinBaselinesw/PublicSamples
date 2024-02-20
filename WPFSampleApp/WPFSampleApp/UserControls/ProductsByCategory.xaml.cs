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
    /// Interaction logic for ProductsByCategory.xaml
    /// </summary>
    public partial class ProductsByCategory : UserControl
    {
        IDataAccessAPI DataAccessAPI = null;
        int CategoryID = 0;
        ContentControl contentControl;

        public ProductsByCategory(IDataAccessAPI DataAccessAPI, int CategoryID, ContentControl contentControl)
        {
            InitializeComponent();
            this.DataAccessAPI = DataAccessAPI;
            this.CategoryID = CategoryID;
            this.contentControl = contentControl;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var ProductsByCategory = DataAccessAPI.GetProductsByCategoryID(CategoryID);
            var FirstCategory = DataAccessAPI.GetProductCategoriesByID(CategoryID).FirstOrDefault();
            if (FirstCategory != null)
            {
                ReportTitle.Text = string.Format($"Products in the {FirstCategory.CategoryName} category");
            }
            else
            {
                ReportTitle.Text = string.Format($"This category is not found in the database!");
            }

            ProductGrid.ItemsSource = ProductsByCategory;
        }
  
   
    }
}
