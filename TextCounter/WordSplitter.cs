using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCounter
{
    public class WordSplitter
    {
        readonly char[] SplitterConst = new char[] { ' ', ',', '.', '!', '?', '\"', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t'};

        public List<string> Split(List<string> orignsStrings)
        {
            List<string> ReturnedWords = new List<string>();

            foreach (string orignWord in orignsStrings)
            {
                var splitWord = Split(orignWord);
                ReturnedWords.AddRange(splitWord);
            }

            return ReturnedWords;
        }
        public List<string> Split(string orignString)
        {
            IEnumerable<string> DirtyString = orignString.Split(SplitterConst);

            return DeletleEmptyWord(DirtyString);
        }
        List<string> DeletleEmptyWord(IEnumerable<string> words)
        {
            return words.Where(obj => !string.IsNullOrEmpty(obj)).ToList();
        }

        
    }
}
