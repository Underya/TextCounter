using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;

namespace TextCounter
{
    public class HtmlParser:
        IHTMLWebSource
    {
        HtmlDocument doc = new HtmlDocument();

        public void SetStringSource(string v)
        {
            doc.LoadHtml(v);
        }

        public void SetWebSource(string URL)
        {
            HtmlWeb web = new HtmlWeb();
            doc = web.Load(URL);
        }

        public string OrignHtml()
        {
            return doc.Text;
        }

        public List<string> Parse()
        {
            var nodes = doc.DocumentNode.SelectNodes("//body//text()[not(ancestor::script)]");
            List<string> nodesText = nodes.Select(node => node.InnerText).ToList();

            return nodesText;
        }
    }
}
