using System;
using System.Collections.Generic;

using TextCounter;

using Moq;
using NUnit.Framework;

namespace TextCounter_test
{
    [TestFixture]
    public class 
    Cleaner_test 
    {
        [TestCase("","")]
        [TestCase("    ", "")]
        [TestCase("  text   ", "text")]
        [TestCase("\n", "")]
        [TestCase("\n\r\t", "")]
        [TestCase("\n\r\t text \n\r\t", "text")]
        [TestCase("text \n\r\t text", "text \n\r\t text")]
        public void 
        Cleane_CleanOneString_ReturnCleaneString(string orignString, string ClearedString)
        {
            Cleaner cleaner = new Cleaner();

            string returnedText = cleaner.Cleane(orignString);

            Assert.AreEqual(ClearedString, returnedText);
        }

        [Test]
        public void
        Cleane_CleaneMultipleStrings_ReturnCleaneString()
        {
            Cleaner cleaner = new Cleaner();
            List<string> dirtyString = new List<string> { "  str1", "str2  \n\r" };

            List<string> CleanerString = cleaner.Cleane(dirtyString);

            Assert.Contains("str1", CleanerString);
            Assert.Contains("str2", CleanerString);
        }

        [TestCase("", "", "", 0)]
        [TestCase("text", "", "", 1)]
        [TestCase("", "text2", "text3", 2)]
        [TestCase("text1", "text2", "text3", 3)]
        public void
        Cleane_CleaneMultipleString_NotReturnEmptyString(string s1, string s2, string s3, int CountCleaninWord)
        {
            Cleaner cleaner = new Cleaner();
            var dirtyString = new List<string> { s1, s2, s3};

            var CleanerString = cleaner.Cleane(dirtyString);

            Assert.AreEqual(CountCleaninWord, CleanerString.Count, "Класс должен убирать из результатов пустые строки");
        }
    }
}
