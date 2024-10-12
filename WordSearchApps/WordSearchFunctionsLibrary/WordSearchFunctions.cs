using System.Diagnostics.Metrics;

namespace WordSearchFunctionsLibrary
{

    public class WordSearchFunctions
    {
        IEnumerable<string> AllWords;
        List<IEnumerable<string>> WordsByFirstLetter = new List<IEnumerable<string>>();

        public WordSearchFunctions(IEnumerable<string> AllWords, string[] Alphabet)
        {
            this.AllWords = AllWords;


            foreach (string Letter in Alphabet) 
            {
                WordsByFirstLetter.Add(AllWords.Where(t => t.StartsWith(Letter, StringComparison.OrdinalIgnoreCase)).ToList());
            }

        }

        public IEnumerable<string> WordsStartingWith(string startingLetters)
        {
            return WordsStartingWith(startingLetters, 1, 9999);
        }

        public IEnumerable<string> WordsStartingWith(string startingLetters, int MinLin, int MaxLen)
        {
            return AllWords.Where(t => (t.Length >= MinLin && t.Length <= MaxLen) && t.StartsWith(startingLetters));
        }

        public IEnumerable<string> WordsEndingWith(string endingLetters)
        {
            return WordsEndingWith(endingLetters, 1, 9999);
        }

        public IEnumerable<string> WordsEndingWith(string endingLetters, int MinLin, int MaxLen)
        {
            return AllWords.Where(t => (t.Length >= MinLin && t.Length <= MaxLen) && t.EndsWith(endingLetters));
        }


        public IEnumerable<string> WordsContaining(string[] containingLetters, int MinLin, int MaxLen)
        {
            var FilteredWords = AllWords.Where(t => (t.Length >= MinLin && t.Length <= MaxLen));

            foreach (var letter in containingLetters)
            {
                FilteredWords = FilteredWords.Where(t=>t.Contains(letter, StringComparison.OrdinalIgnoreCase));
            }

            return FilteredWords;
        }

        public IEnumerable<string> WordsContainingExclusive(string[] containingLetters, int MinLin, int MaxLen)
        {
            var FilteredWords = AllWords.Where(t => (t.Length >= MinLin && t.Length <= MaxLen));
            List<char> charLetters = ConvertToCharArray(containingLetters);

            FilteredWords = FilteredWords.Where(t => ContainsExclusive(t, charLetters));
                 
            return FilteredWords;
        }

        private bool ContainsExclusive(string word, List<char> containingLetters)
        {
            foreach (char letter in word.ToUpper())
            {
                if (!containingLetters.Contains(letter))
                    return false;
                  
            }
            return true;
        }

        private List<char> ConvertToCharArray(string[] containingLetters)
        {
           List<char> chars = new List<char>();

            foreach (string letter in containingLetters)
            {
                chars.Add(letter[0]);
            }
            return chars;
        }

    
    }
}
