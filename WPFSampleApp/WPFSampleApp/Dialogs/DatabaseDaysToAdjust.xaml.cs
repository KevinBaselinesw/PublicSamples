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
using System.Windows.Shapes;

namespace WPFSampleApp.Dialogs
{
    /// <summary>
    /// Interaction logic for DatabaseDaysToAdjust.xaml
    /// </summary>
    public partial class DatabaseDaysToAdjust : Window
    {
        public int DaysToAdjust = 0;

        public DatabaseDaysToAdjust()
        {
            InitializeComponent();

            EnteredDaysTB.Text = DaysToAdjust.ToString();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            int temp = 0;

            if (int.TryParse(EnteredDaysTB.Text, out temp))
            {
                DaysToAdjust = temp;
                this.DialogResult = true;
                return;
            }

            MessageBox.Show("Entered Days value is not an integer!", "Entry error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;

        }
    }
}
