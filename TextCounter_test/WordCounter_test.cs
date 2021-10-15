using System;
using System.Collections.Generic;

using TextCounter;

using Moq;
using NUnit.Framework;

namespace TextCounter_test
{

    [TestFixture]
    public class WordCounter_test
    {
        [Test]
        public void
        Counter_CounterOneWord_ReturnSameWord()
        {
            WordCounter counter = new WordCounter();
            List<string> wordList = new List<string> { "word1", "word2", "word1" };

            Dictionary<string, int> keyCount = counter.Count(wordList);

            Assert.Contains("word1", keyCount.Keys);
            Assert.Contains("word2", keyCount.Keys);
        }

        [Test]
        public void
        Counter_CounterWord_ReturnRightCountWords()
        {
            WordCounter counter = new WordCounter();
            List<string> wordList = new List<string> { "word3", "word2", "word1", "word3", "word2", "word3" };

            Dictionary<string, int> keyCount = counter.Count(wordList);

            Assert.AreEqual(3, keyCount["word3"]);
            Assert.AreEqual(2, keyCount["word2"]);
            Assert.AreEqual(1, keyCount["word1"]);
        }

        [Test]
        public void
        Counter_CounterSameWord_Return2Word()
        {
            WordCounter counter = new WordCounter();
            List<string> wordList = new List<string> { "Word", "word" };

            Dictionary<string, int> keyCount = counter.Count(wordList);

            Assert.AreEqual(2, keyCount.Count);
        }

    }
}
