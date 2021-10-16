using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCounter
{
    public interface IWordCounter
    {
        Dictionary<string, int> Count(IEnumerable<string> wordList);
    }
}
