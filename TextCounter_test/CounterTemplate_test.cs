using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TextCounter;

namespace TextCounter_test
{
    [TestFixture]
    public class CounterTemplate_test
    {
        CounterTemplate TemplateWithStub(List<string> HTMLReturns = null, List<string> PrepareReturns = null, Dictionary<string, int> WordCounter = null)
        {
            var webSourceStub = new Mock<IHTMLWebSource>();
            webSourceStub.Setup(obj => obj.Parse()).Returns(HTMLReturns ?? new List<string> { });
            var PreapareStub = new Mock<IPrepare>();
            PreapareStub.Setup(obj => obj.Prepare(It.IsAny<IEnumerable<string>>())).Returns(PrepareReturns ?? new List<string>());
            var WordCounterStub = new Mock<IWordCounter>();
            WordCounterStub.Setup(obj => obj.Count(It.IsAny<IEnumerable<string>>())).Returns(WordCounter ?? new Dictionary<string, int>());
            return new CounterTemplate(webSourceStub.Object, new List<IPrepare> { PreapareStub.Object }, WordCounterStub.Object);
        }

        [Test]
        public void
        ParseAndCount_GetURLToParser_SetURLSource()
        {
            CounterTemplate template = TemplateWithStub();
            var parserMock = new Mock<IHTMLWebSource>();
            parserMock.Setup(obj => obj.Parse()).Returns(new List<string>());
            template.Source = parserMock.Object;

            template.ParseAndCount("https://test.url.com/");

            parserMock.Verify(obj => obj.SetWebSource("https://test.url.com/"));
        }

        [Test]
        public void
        ParseAndCount_GetHTMLSource_CallHTMLSource()
        {
            CounterTemplate template = TemplateWithStub();
            var parserMock = new Mock<IHTMLWebSource>();
            parserMock.Setup(obj => obj.Parse()).Returns(new List<string>());
            template.Source = parserMock.Object;

            template.ParseAndCount("test.url.com");

            parserMock.Verify(obj => obj.Parse());
        }

        [Test]
        public void
        ParseAndCount_ThrowExcrption_ThrowSameException()
        {
            CounterTemplate template = TemplateWithStub();
            var parseStub = new Mock<IHTMLWebSource>();
            parseStub.Setup(obj => obj.Parse()).Throws(new Exception("Не правильный URL"));
            template.Source = parseStub.Object;

            Assert.Throws<Exception>(() => template.ParseAndCount(""), "Не правильный URL");
        }

        [Test]
        public void
        ParseAndCount_CallPrepare_CallAllPrepare()
        {
            CounterTemplate template = TemplateWithStub(new List<string> { "word" });
            var prepareMock1 = new Mock<IPrepare>(); prepareMock1.Setup(obj => obj.Prepare(It.IsAny<IEnumerable<string>>())).Returns(new List<string> { "word" });
            var prepareMock2 = new Mock<IPrepare>(); prepareMock2.Setup(obj => obj.Prepare(It.IsAny<IEnumerable<string>>())).Returns(new List<string> { "word" });
            var prepareMock3 = new Mock<IPrepare>(); prepareMock3.Setup(obj => obj.Prepare(It.IsAny<IEnumerable<string>>())).Returns(new List<string> { "word" });
            template.PreparesList = new List<IPrepare> { prepareMock1.Object, prepareMock2.Object, prepareMock3.Object };

            template.ParseAndCount("test");

            prepareMock1.Verify(obj => obj.Prepare(It.IsAny<IEnumerable<string>>()), Times.Once);
            prepareMock2.Verify(obj => obj.Prepare(It.IsAny<IEnumerable<string>>()), Times.Once);
            prepareMock3.Verify(obj => obj.Prepare(It.IsAny<IEnumerable<string>>()), Times.Once);
        }

        [Test]
        public void
        ParseAndCount_PrepareReturn0Word_TwoPreapreNotCall()
        {
            CounterTemplate template = TemplateWithStub(new List<string> { "word" });
            var prepareMock1 = new Mock<IPrepare>(); prepareMock1.Setup(obj => obj.Prepare(It.IsAny<IEnumerable<string>>())).Returns(new List<string>());
            var prepareMock2 = new Mock<IPrepare>(); prepareMock2.Setup(obj => obj.Prepare(It.IsAny<IEnumerable<string>>())).Returns(new List<string>());
            template.PreparesList = new List<IPrepare> { prepareMock1.Object, prepareMock2.Object };

            template.ParseAndCount("test");

            prepareMock2.Verify(obj => obj.Prepare(It.IsAny<IEnumerable<string>>()), Times.Never);
        }

        [Test]
        public void
        ParseAndCount_1PrepareRerturn3Word_2PrepareCall3()
        {
            CounterTemplate template = TemplateWithStub(new List<string> { "word" });
            var prepareMock1 = new Mock<IPrepare>(); prepareMock1.Setup(obj => obj.Prepare(It.IsAny<IEnumerable<string>>())).Returns(new List<string> { "1", "2", "3" });
            var prepareMock2 = new Mock<IPrepare>(); prepareMock2.Setup(obj => obj.Prepare(It.IsAny<IEnumerable<string>>())).Returns(new List<string>());
            template.PreparesList = new List<IPrepare> { prepareMock1.Object, prepareMock2.Object };

            template.ParseAndCount("test");

            prepareMock2.Verify(obj => obj.Prepare(It.IsAny<IEnumerable<string>>()), Times.Exactly(3));
        }

        [Test]
        public void
        ParseAndCount_ParserReturn0Word_PreparedNotCall()
        {
            CounterTemplate template = TemplateWithStub(new List<string>());
            var prepareMock1 = new Mock<IPrepare>();
            var prepareMock2 = new Mock<IPrepare>();
            var prepareMock3 = new Mock<IPrepare>();
            template.PreparesList = new List<IPrepare> { prepareMock1.Object, prepareMock2.Object, prepareMock3.Object };

            template.ParseAndCount("test");

            prepareMock1.Verify(obj => obj.Prepare(It.IsAny<IEnumerable<string>>()), Times.Never);
            prepareMock2.Verify(obj => obj.Prepare(It.IsAny<IEnumerable<string>>()), Times.Never);
            prepareMock3.Verify(obj => obj.Prepare(It.IsAny<IEnumerable<string>>()), Times.Never);
        }

        [Test]
        public void
        ParseAndCount_RarseThrowExceptionParse_NotThrow()
        {
            CounterTemplate template = TemplateWithStub(new List<string> { "word" });
            var prepareMock = new Mock<IPrepare>();
            prepareMock.Setup(obj => obj.Prepare(It.IsAny<IEnumerable<string>>())).Throws(new PrepareException());
            template.PreparesList = new List<IPrepare> { prepareMock.Object };

            template.ParseAndCount("test");
        }

        [Test]
        public void
        ParseAndCount_RarseThrowException_ThrowException()
        {
            CounterTemplate template = TemplateWithStub(new List<string> { "word" });
            var prepareMock = new Mock<IPrepare>();
            prepareMock.Setup(obj => obj.Prepare(It.IsAny<IEnumerable<string>>())).Throws(new Exception());
            template.PreparesList = new List<IPrepare> { prepareMock.Object };

            Assert.Throws<Exception>(() => template.ParseAndCount("test"));
        }

        [Test]
        public void
        ParseAndCount_CallWordCounter_CallWith3Word()
        {
            CounterTemplate template = TemplateWithStub(new List<string> { "word" });
            var prepareStab = new Mock<IPrepare>();
            List<string> prepareWordList = new List<string> { "w1", "w2", "w3" };
            prepareStab.Setup(obj => obj.Prepare(It.IsAny<IEnumerable<string>>())).Returns(prepareWordList);

            template.PreparesList = new List<IPrepare> { prepareStab.Object };
            var wordCounterMock = new Mock<IWordCounter>();
            template.WordCounter = wordCounterMock.Object;

            template.ParseAndCount("test");

            wordCounterMock.Verify(obj => obj.Count(prepareWordList), Times.Once);
        }

        [Test]
        public void
        ParseAndCount_WordCounterThrowException_NotCathException()
        {
            CounterTemplate template = TemplateWithStub(new List<string> { "word" }, new List<string> { "word" });
            var WordCounterStub = new Mock<IWordCounter>();
            WordCounterStub.Setup(obj => obj.Count(It.IsAny<IEnumerable<string>>())).Throws(new Exception());
            template.WordCounter = WordCounterStub.Object;

            Assert.Throws<Exception>(() => template.ParseAndCount("test"));
        }

        [Test]
        public void
        ParseAndCount_GetResult_CallALLRecipient()
        {
            Dictionary<string, int> wordCount = new Dictionary<string, int>(); wordCount["word"] = 1;
            CounterTemplate template = TemplateWithStub(new List<string> { "word" }, new List<string> { "word" }, wordCount);
            var IResultRecipientMock1 = new Mock<IRecipientWord>();
            var IResultRecipientMock2 = new Mock<IRecipientWord>();
            template.RecipientList = new List<IRecipientWord> { IResultRecipientMock1.Object, IResultRecipientMock2.Object };

            template.ParseAndCount("test");

            IResultRecipientMock1.Verify(obj => obj.SetResult(It.IsAny<string>(), It.IsAny<Dictionary<string, int>>()), Times.Once);
            IResultRecipientMock2.Verify(obj => obj.SetResult(It.IsAny<string>(), It.IsAny<Dictionary<string, int>>()), Times.Once);
        }

        [Test]
        public void
        ParseAndCount_GetResult_GetRightResult()
        {
            Dictionary<string, int> wordCount = new Dictionary<string, int>(); wordCount["word"] = 1;
            CounterTemplate template = TemplateWithStub(new List<string> { "word" }, new List<string> { "word" }, wordCount);
            var IResultRecipientMock1 = new Mock<IRecipientWord>();
            var IResultRecipientMock2 = new Mock<IRecipientWord>();
            template.RecipientList = new List<IRecipientWord> { IResultRecipientMock1.Object, IResultRecipientMock2.Object };

            template.ParseAndCount("test");

            IResultRecipientMock1.Verify(obj => obj.SetResult("test", wordCount));
            IResultRecipientMock2.Verify(obj => obj.SetResult("test", wordCount));
        }

        [Test]
        public void
        ParseAndCount_RecipientThrowException_CathcException()
        {
            Dictionary<string, int> wordCount = new Dictionary<string, int>(); wordCount["word"] = 1;
            CounterTemplate template = TemplateWithStub(new List<string> { "word" }, new List<string> { "word" }, wordCount);
            var IResultRecipientStub = new Mock<IRecipientWord>();
            IResultRecipientStub.Setup(obj => obj.SetResult("test", wordCount)).Throws(new Exception());
            template.RecipientList = new List<IRecipientWord> { IResultRecipientStub.Object };

            template.ParseAndCount("test");
        }

        [Test]
        public void
        ParseAndCount_ReturnResult_ReturnDicitionary()
        {
            Dictionary<string, int> wordCount = new Dictionary<string, int>(); wordCount["word"] = 3; wordCount["word2"] = 2;
            CounterTemplate template = TemplateWithStub(new List<string> { "word" }, new List<string> { "word" }, wordCount);

            var result = template.ParseAndCount("test");

            Assert.AreEqual(wordCount, result);
        }
    }
}
