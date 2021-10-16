using System;
using System.Collections.Generic;

using TextCounter;

using Moq;
using NUnit.Framework;

namespace TextCounter_test
{
    [TestFixture]
    public class CaseInsensitive_test
    {
        [Test]
        public void
        Unify_RetunWordToLowerCase()
        {
            string word = "aBcАбВгД";
            CaseInsensitive unifying = new CaseInsensitive();

            string unifyingWord = unifying.Unify(word);

            Assert.AreEqual("abcабвгд", unifyingWord);
        }

        [Test]
        public void
        Unify_UnifyMultipleString_ReturnAllStringToLowerCase()
        {
            List<string> words = new List<string> {"abC", "аБв" };
            CaseInsensitive unifyingWord = new CaseInsensitive();

            var returnedWords = unifyingWord.Unify(words);

            Assert.Contains("abc", returnedWords);
            Assert.Contains("абв", returnedWords);
        }
    }
}
