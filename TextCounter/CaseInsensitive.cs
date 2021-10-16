using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCounter
{
    public class CaseInsensitive
    {
        public List<string> Unify(IEnumerable<string> words)
        {
            List<string> unifyingWords = new List<string>();

            foreach (string word in words)
                unifyingWords.Add(Unify(word));

            return unifyingWords;
        }
        public string Unify(string word)
        {
            //Важный момент. К каком регистру приводить слова
            //Я выбрал нижний, т.к. чаще всего в слове 1 буква в верхнем регистре
            return word.ToLower();
        }
    }
}
