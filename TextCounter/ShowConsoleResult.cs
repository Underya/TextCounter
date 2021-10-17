using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCounter
{
    class ShowConsoleResult :
        IRecipientWord
    {
        public void SetResult(string URL, Dictionary<string, int> CountWord)
        {
            Console.WriteLine("URL:{0}", URL);
            foreach(var pair in CountWord.OrderByDescending(obj => obj.Value))
            {
                Console.WriteLine("word:{0} count:{1}", pair.Key, pair.Value);
            }
        }
    }
}
