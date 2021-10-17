using NUnit.Framework;
using System.Collections.Generic;
using TextCounter;

namespace TextCounter_test
{

    [TestFixture]
    public class
    WordSplitter_test
    {
        WordSplitter GetSplitter()
        {
            return new WordSplitter();
        }

        [TestCase(" ")]
        [TestCase(",")]
        [TestCase(".")]
        [TestCase("!")]
        [TestCase("?")]
        [TestCase("\"")]
        [TestCase(";")]
        [TestCase(":")]
        [TestCase("[")]
        [TestCase("]")]
        [TestCase("(")]
        [TestCase(")")]
        [TestCase("\n")]
        [TestCase("\r")]
        [TestCase("\t")]
        public void
        Split_SpitlTwoWord_Return2Word(string separatorCharacter)
        {
            WordSplitter splitter = GetSplitter();
            string OrignString = "word1" + separatorCharacter + "word2";

            List<string> splitterWord = splitter.Split(OrignString);

            Assert.Contains("word1", splitterWord, "Не разедлил по символу:" + separatorCharacter);
            Assert.Contains("word2", splitterWord, "Не разедлил по символу:" + separatorCharacter);
        }

        [Test]
        public void
        Split_SplitMultiplinWord_Return3Word()
        {
            WordSplitter splitter = GetSplitter();
            string OrignString = "word ,.!?\nword2;:[]()\rword3\t\r";

            List<string> splitterWord = splitter.Split(OrignString);

            Assert.AreEqual(3, splitterWord.Count);
            Assert.Contains("word", splitterWord);
            Assert.Contains("word2", splitterWord);
            Assert.Contains("word3", splitterWord);
        }

        [Test]
        public void
        Split_SplitListString_Return6Word()
        {
            WordSplitter splitter = GetSplitter();
            List<string> OrignsStrings = new List<string> { "w1 . w2", "w3 ,! w4", "w5 ?\" w6" };

            List<string> splitterWord = splitter.Split(OrignsStrings);

            Assert.AreEqual(6, splitterWord.Count);
            Assert.Contains("w1", splitterWord);
            Assert.Contains("w2", splitterWord);
            Assert.Contains("w3", splitterWord);
            Assert.Contains("w4", splitterWord);
            Assert.Contains("w5", splitterWord);
            Assert.Contains("w6", splitterWord);
        }
    }
}
