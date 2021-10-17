using System.Collections.Generic;
using System.Linq;

namespace TextCounter
{
    public class WordSplitter :
        IPrepare
    {
        readonly char[] SplitterConst = new char[] { ' ', ',', '.', '!', '?', '\"', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t' };

        public List<string> Prepare(IEnumerable<string> text)
        {
            return Split(text);
        }
        public List<string> Split(IEnumerable<string> orignsStrings)
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
