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
    /// Interaction logic for CategoriesByProduct.xaml
    /// </summary>
    public partial class CategoriesByProduct : UserControl
    {
        IDataAccessAPI DataAccessAPI = null;
        int CategoryID;
        ContentControl contentControl;

        public CategoriesByProduct(IDataAccessAPI DataAccessAPI, int CategoryID, ContentControl contentControl)
        {
            InitializeComponent();

            this.DataAccessAPI = DataAccessAPI;
            this.CategoryID = CategoryID;
            this.contentControl = contentControl;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var Categories = DataAccessAPI.GetProductCategoriesByID(CategoryID);
            var Category = Categories.FirstOrDefault();
            if (Category != null)
            {
                ReportTitle.Text = string.Format($"The category for this product is {Category.CategoryName}");
            }
            else
            {
                ReportTitle.Text = string.Format($"The category for this product is unknown");
            }

            ProductCategoryGrid.ItemsSource = Categories;
        }

    }
}
