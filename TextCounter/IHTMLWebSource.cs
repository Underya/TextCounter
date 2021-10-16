﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCounter
{
    public interface IHTMLWebSource
    {
        void SetWebSource(string URL);
        string OrignHtml();
        List<string> Parse();
    }
}
