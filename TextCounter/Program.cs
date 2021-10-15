using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            while (true)
            {
                string URL = "";
                URL = Console.ReadLine();
                HtmlParser parser = new HtmlParser();
                Cleaner cleaner = new Cleaner();
                WordSplitter splitter = new WordSplitter();

                parser.SetWebSource(URL);
                var text = parser.Parse();
                Console.WriteLine("{0} Count Element {1}", "text", text.Count);
                var text2 = cleaner.Cleane(text);
                Console.WriteLine("{0} Count Element {1}", "text2", text2.Count);
                var text3 = splitter.Split(text2);
                Console.WriteLine("{0} Count Element {1}", "text3", text3.Count);

                foreach (string textParagraph in text3.OrderBy(obj => obj))
                    Console.WriteLine(textParagraph);

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
