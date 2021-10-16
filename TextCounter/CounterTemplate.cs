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
        public CounterTemplate(IHTMLWebSource webSource)
        {
            Source = webSource;
        }

        public IHTMLWebSource Source { get; set; }
        public string URL { get; protected set; }

        public void StartParse(string URL)
        {
            this.URL = URL;
            List<string> orignString = Parse(URL);
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
                throw exp;
            }
        }
        List<string> NotSaveParse(string URI)
        {
            Source.SetWebSource(URI);
            List<string> orignString = Source.Parse();
            return orignString;
        }
    }
}
