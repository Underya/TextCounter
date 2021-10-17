using NUnit.Framework;
using System.Collections.Generic;
using TextCounter;

namespace TextCounter_test
{
    [TestFixture]
    public class HtmlParser_test
    {
        string HTMLText()
        {
            return @"
                <!DOCTYPE html>
                <html>
                <head>
                    Головной тег
                </head>
                <body>
                    <script>Скрипт</script>
	                <h1>Заголовок</h1>
	                <h2>Заголовок 2</h2>
                    <p> Текст </p>
                    <br> абзац <i>Красивый</i>
                </body>
                <
                </html>
                    ";
        }

        [Test]
        public void
        LoadFromString_SetSource_ReturnHtmlText()
        {
            HtmlParser parser = new HtmlParser();

            parser.SetStringSource(HTMLText());

            Assert.AreEqual(HTMLText(), parser.OrignHtml());
        }


        [TestCase("Заголовок")]
        [TestCase("Заголовок 2")]
        [TestCase("Текст")]
        [TestCase("абзац")]
        [TestCase("Красивый")]
        public void
        Parse_ParseTextHTMLTag_ReturnTagText(string text)
        {
            HtmlParser parser = new HtmlParser();
            parser.SetStringSource(HTMLText());

            List<string> textNodes = parser.Parse();

            Assert.IsTrue(textNodes.Exists(node => node.Contains(text)), "Не найден текст из тега");
        }

        [TestCase("Заголовок Заголовок 2")]
        [TestCase("абзац Красивый")]
        public void
        Parse_ParseTextHTMLTag_NotContaintText(string text)
        {
            HtmlParser parser = new HtmlParser();
            parser.SetStringSource(HTMLText());

            List<string> textNodes = parser.Parse();

            Assert.IsFalse(textNodes.Exists(node => node.Contains(text)), "Текст из двух тегов попал в один тег");
        }

        [TestCase("Головной тег")]
        [TestCase("Скрипт")]
        public void
        Parse_ParseOnlyText_NotContainTextOtherTag(string notContain)
        {
            HtmlParser parser = new HtmlParser();
            parser.SetStringSource(HTMLText());

            List<string> textNodes = parser.Parse();

            Assert.IsFalse(textNodes.Exists(node => node.Contains(notContain)), "Среди текста не должно быть текста из других скриптов (CSS, Script)");
        }


    }
}
