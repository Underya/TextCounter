using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace TextCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            SetConfig();
            StartCount();

            return;
        }

        private static void SetConfig()
        {
            //Установка параметров для загрузки данных от сайтов
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        private static void StartCount()
        {
            var prepareList = new List<IPrepare>();
            prepareList.Add(new Cleaner());
            prepareList.Add(new WordSplitter());
            prepareList.Add(new CaseInsensitive());
            var recipientList = new List<IRecipientWord> { new ShowConsoleResult(), new SaveWordCount() };

            CounterTemplate template = new CounterTemplate(new HtmlParser(), prepareList, new WordCounter());
            template.RecipientList = recipientList;
            Console.Write("Введите адрес сайта:");
            string URL = Console.ReadLine();

            template.ParseAndCount(URL);

        }
    }
}
