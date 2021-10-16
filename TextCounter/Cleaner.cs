using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCounter
{
    public class Cleaner:
        IPrepare
    {
        public List<string> Prepare(IEnumerable<string> text)
        {
            return Cleane(text);
        }
        public List<string> Cleane(IEnumerable<string> dirtyLines)
        {
            List<string> cleaneString = new List<string>();

            foreach(string str in dirtyLines)
                if(!string.IsNullOrEmpty(str))
                    cleaneString.Add(Cleane(str));

            return cleaneString;
        }
        public string Cleane(string orignString)
        {
            return orignString.Trim();
        }
        
    }
}
