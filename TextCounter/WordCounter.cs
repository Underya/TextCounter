using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCounter
{
    public class WordCounter
    {
        public Dictionary<string, int> Count(IEnumerable<string> wordList)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (string word in wordList)
                UpdateWordWalue(dict, word);
            return dict;
        }
        void UpdateWordWalue(Dictionary<string, int> dict, string word)
        {
            int value;
            if (dict.TryGetValue(word, out value))
                dict[word] = ++value;
            else
                dict[word] = 1;
        }
    }
}
