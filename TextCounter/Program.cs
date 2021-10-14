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
                parser.SetWebSource(URL);
                var text = parser.Parse();
                foreach(string textParagraph in text)
                    Console.WriteLine(textParagraph);

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
