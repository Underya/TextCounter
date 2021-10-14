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
                parser.SetWebSource(URL);
                var text = parser.Parse();
                var text2 = cleaner.Cleane(text);
                foreach(string textParagraph in text2)
                    Console.WriteLine(textParagraph);

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
