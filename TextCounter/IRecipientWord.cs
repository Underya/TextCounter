using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCounter
{
    public interface IRecipientWord
    {
        void SetResult(string URL, Dictionary<string, int> CountWord);
    }
}
