using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCounter
{
    public interface IPrepare
    {
        List<string> Prepare(IEnumerable<string> text);
    }
}
