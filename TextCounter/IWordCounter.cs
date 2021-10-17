using System.Collections.Generic;

namespace TextCounter
{
    public interface IWordCounter
    {
        Dictionary<string, int> Count(IEnumerable<string> wordList);
    }
}
