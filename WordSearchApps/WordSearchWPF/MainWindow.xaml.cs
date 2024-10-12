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
        string[] Alphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        WordSearchFunctionsLibrary.WordSearchFunctions? WordSearchFunctions = null;
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
                    WordSearchFunctions = new WordSearchFunctionsLibrary.WordSearchFunctions(AllWords, Alphabet);

                    var WordsStartingWith = WordSearchFunctions.WordsStartingWith("st").ToList();
                    var WordsStartingWith2 = WordSearchFunctions.WordsStartingWith("st", 5,7).ToList();
                    var WordsStartingWith3 = WordSearchFunctions.WordsStartingWith("st", 999, 7).ToList();
                    var WordsEndingWith = WordSearchFunctions.WordsEndingWith("ing").ToList();

                    var WordsContaing1 = WordSearchFunctions.WordsContaining(new string[] { "A", "N", "D" }, 1, 7).ToList();
                    var WordsContaing2 = WordSearchFunctions.WordsContainingExclusive(new string[] { "A", "N", "N", "D" }, 3, 4).ToList();
                    var WordsContaing3 = WordSearchFunctions.WordsContainingExclusive(new string[] { "A", "N", "D" }, 3, 4).ToList();
                    var WordsContaing4 = WordSearchFunctions.WordsContainingExclusive(new string[] { "A", "N", "D" }, 3, 4, true).ToList();

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



        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ListBox[] ListBoxes = new ListBox[] { WordScapeListBox0, WordScapeListBox1, WordScapeListBox2, WordScapeListBox3, WordScapeListBox4, 
                                                  WordScapeListBox5, WordScapeListBox6, WordScapeListBox7, WordScapeListBox8 };


            foreach (var lb in ListBoxes)
            {
                lb.Visibility = Visibility.Collapsed;
                lb.ItemsSource = null;
            }

            string enteredLetters = EnteredLetters.Text.ToUpper();

            List<string> searchLetters = new List<string>();

            foreach (char letter in enteredLetters)
            {
                if (char.IsAsciiLetter(letter))
                {
                    searchLetters.Add(letter.ToString());
                }
            }
            
            var WordsContaining = WordSearchFunctions?.WordsContainingExclusive(searchLetters.ToArray(), 2, searchLetters.Count, true).ToList();

            for (int i = 2; i <= searchLetters.Count; i++)
            {
                ListBoxes[i].ItemsSource = WordsContaining?.Where(t => t.Length == i).ToList();
                ListBoxes[i].Visibility = Visibility.Visible;
            }

    


        }
    }
}