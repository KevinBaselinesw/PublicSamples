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

namespace WCFSampleClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WCFSampleService.WCFSampleServiceClient Client = new WCFSampleService.WCFSampleServiceClient();

            var Employees = Client.GetAllEmployees();
            MessageBox.Show(Employees[0].LastName);

            //var str = Client.GetData(97);
            //MessageBox.Show(str);
        }

        private void SOAPExamplButton_Click(object sender, RoutedEventArgs e)
        {
            SOAPExample dlg = new SOAPExample();
            dlg.Show();
        }

        private void RESTExamplButton_Click(object sender, RoutedEventArgs e)
        {
            RESTExample dlg = new RESTExample();
            dlg.Show();
        }
    }
}
