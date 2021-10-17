using System.Collections.Generic;

namespace TextCounter
{
    public interface IHTMLWebSource
    {
        void SetWebSource(string URL);
        string OrignHtml();
        List<string> Parse();
    }
}
