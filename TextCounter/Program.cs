﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace TextCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            NewMethod();
            return;

            while (true)
            {
                string URL = "";
                URL = Console.ReadLine();
                HtmlParser parser = new HtmlParser();
                Cleaner cleaner = new Cleaner();
                WordSplitter splitter = new WordSplitter();
                CaseInsensitive unifying = new CaseInsensitive();
                WordCounter counter = new WordCounter();

                parser.SetWebSource(URL);
                var text = parser.Parse();
                var text2 = cleaner.Cleane(text);
                var text3 = unifying.Unify(text2);
                var text4 = splitter.Split(text3);

                var countWord = counter.Count(text4);
                foreach (var dictPair in countWord.OrderByDescending(obj => obj.Value))
                    Console.WriteLine("Word:{0} Count:{1}", dictPair.Key, dictPair.Value);

                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void NewMethod()
        {
            var prepareList = new List<IPrepare>();
            prepareList.Add(new Cleaner());
            prepareList.Add(new WordSplitter());
            prepareList.Add(new CaseInsensitive());
            var recipientList = new List<IRecipientWord> { new ShowConsoleResult() };

            CounterTemplate template = new CounterTemplate(new HtmlParser(), prepareList, new WordCounter());
            template.RecipientList = recipientList;
            string URL = "";
            URL = Console.ReadLine();

            template.ParseAndCount(URL);

        }
    }
}
