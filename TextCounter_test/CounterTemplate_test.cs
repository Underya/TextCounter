using System;
using System.Collections.Generic;

using TextCounter;

using Moq;
using NUnit.Framework;

namespace TextCounter_test
{
    [TestFixture]
    public class CounterTemplate_test
    {
        CounterTemplate TemplateWithStub()
        {
            var webSourceMock = new Mock<IHTMLWebSource>();
            return new CounterTemplate(webSourceMock.Object);
        }

        [Test]
        public void
        Start_GetURLToParser_SetURLSource()
        {
            CounterTemplate template = TemplateWithStub();
            var parserMock = new Mock<IHTMLWebSource>();
            template.Source = parserMock.Object;

            template.StartParse("https://test.url.com/");

            parserMock.Verify(obj => obj.SetWebSource("https://test.url.com/"));
        }

        [Test]
        public void
        Start_GetHTMLSource_CallHTMLSource()
        {
            CounterTemplate template = TemplateWithStub();
            var parserMock = new Mock<IHTMLWebSource>();
            template.Source = parserMock.Object;

            template.StartParse("test.url.com");

            parserMock.Verify(obj => obj.Parse());
        }

        [Test]
        public void
        Start_ThrowExcrption_ThrowSameException()
        {
            CounterTemplate template = TemplateWithStub();
            var parseStub = new Mock<IHTMLWebSource>();
            parseStub.Setup(obj => obj.Parse()).Throws(new Exception("Не правильный URL"));
            template.Source = parseStub.Object;

            Assert.Throws<Exception>(() => template.StartParse(""), "Не правильный URL");
        }
    }
}
