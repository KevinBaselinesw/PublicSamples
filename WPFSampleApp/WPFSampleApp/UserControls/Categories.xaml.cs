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
    /// Interaction logic for Catagories.xaml
    /// </summary>
    public partial class Categories : UserControl
    {
        IDataAccessAPI DataAccessAPI = null;
        ContentControl contentControl;
        IEnumerable<Category> AllCategories;

        public Categories(IDataAccessAPI DataAccessAPI, ContentControl contentControl)
        {
            InitializeComponent();

            this.DataAccessAPI = DataAccessAPI;
            this.contentControl = contentControl;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            AllCategories = DataAccessAPI.GetAllProductCategories();
            ProductCategoryGrid.ItemsSource = AllCategories;
        }

        private void ProductsByCategory_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null)
                return;

            if (btn.Tag.GetType() != typeof(System.Int32))
                return;

            int categoryID = (int)btn.Tag;

            var ProductsByCategory = DataAccessAPI.GetProductsByCategoryID(categoryID);
            var category = AllCategories.First(t => t.CategoryID == categoryID);

            string Message = string.Format($"There are {ProductsByCategory.Count()} products for {category.CategoryName}");

            contentControl.Content = new SimpleText(Message);

            return;
        }
    }
}
