using System.Collections.Generic;

namespace TextCounter
{
    public interface IPrepare
    {
        List<string> Prepare(IEnumerable<string> text);
    }
}
