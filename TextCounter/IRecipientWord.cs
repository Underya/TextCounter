using System.Collections.Generic;

namespace TextCounter
{
    public interface IRecipientWord
    {
        void SetResult(string URL, Dictionary<string, int> CountWord);
    }
}
