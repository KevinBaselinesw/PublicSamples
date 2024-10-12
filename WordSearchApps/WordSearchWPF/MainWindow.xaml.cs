using System.Configuration;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WordSearchWPF
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string? AllWordsFile = ConfigurationManager.AppSettings["AllWordsFile"];

            if (AllWordsFile != null) 
            {
                if (!File.Exists(AllWordsFile))
                {
                    MessageBox.Show($"The file {AllWordsFile} is not found!");
                    return;
                }

                try
                {
                    IEnumerable<string> AllWords = ParseWordsFile(AllWordsFile);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Reading the file {AllWordsFile} causes exception: {ex.Message}!");
                }

            }

        }

        private IEnumerable<string> ParseWordsFile(string allWordsFile)
        {
            List<string> AllWords = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(allWordsFile))
                {
                    string? line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        AllWords.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return AllWords;
        }
    }
}