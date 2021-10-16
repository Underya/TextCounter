using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCounter
{
    //Класс реализует шаблон программирования стратегия (template)
    public class CounterTemplate
    {
        public CounterTemplate(IHTMLWebSource webSource, IEnumerable<IPrepare> prepares, IWordCounter wordCounter)
        {
            Source = webSource;
            PreparesList = prepares.ToList();
            WordCounter = wordCounter;
        }

        public IHTMLWebSource Source { get; set; }
        public string URL { get; protected set; }
        public List<IPrepare> PreparesList { get; set; }
        public IWordCounter WordCounter { get; set; }

        public void StartParse(string URL)
        {
            this.URL = URL;
            List<string> orignString = Parse(URL);
            List<string> preparedWords = Prepare(orignString);
            Dictionary<string, int> CountedWords = WordCounter.Count(preparedWords);
        }
        List<string> Parse(string URI)
        {
            try
            {
                List<string> orignString = NotSaveParse(URI);
                return orignString;
            } catch(Exception exp)
            {
                //1. Считаю, что если здесь происходит ошибка - мы ничего сделать не можем. 
                //   Остаётся только закончить работу класса
                //2. Здесь должен быть логер
                //TODO: Логирование
                throw exp;
            }
        }
        List<string> NotSaveParse(string URI)
        {
            Source.SetWebSource(URI);
            List<string> orignString = Source.Parse();
            return orignString;
        }

        //Подготовка слов к подсчёту идёт по принципу цепочки обязанностей
        List<string> Prepare(List<string> orignString)
        {
            List<string> oldResult = orignString;
            List<string> NewResult = new List<string>();
            
            foreach(IPrepare preapre in PreparesList)
            {
                OldResultPrepare(oldResult, NewResult, preapre);

                oldResult = NewResult;
                NewResult = new List<string>();
            }

            return oldResult;
        }
        void OldResultPrepare(List<string> oldResult, List<string> NewResult, IPrepare preapre)
        {
            //Косяк проектирования. Объекты должны получать строку, а возвращать список, в том числе пустой список
            string[] adapterStringToIEnumerable = new string[1];
            foreach (string text in oldResult)
            {
                adapterStringToIEnumerable[0] = text;
                List<string> results = PrepareWord(preapre, adapterStringToIEnumerable);
                NewResult.AddRange(results);
            }
        }
        List<string> PrepareWord(IPrepare preapre, string[] adapterStringToIEnumerable)
        {
            try
            {
                return NotSavePrepareWord(preapre, adapterStringToIEnumerable);
            } catch(PrepareException prepareException)
            {
                //Если мы не смогли подготовить одно слово - всё ок, парсим дальше
                //TODO: Логирование
                return new List<string>();
            } catch(Exception excp)
            {
                //Если какая та другая проблема - закончить работу
                //TODO: Логирование
                throw excp;
            }
        }
        List<string> NotSavePrepareWord(IPrepare preapre, string[] adapterStringToIEnumerable)
        {
            return preapre.Prepare(adapterStringToIEnumerable);
        }
    }
}
