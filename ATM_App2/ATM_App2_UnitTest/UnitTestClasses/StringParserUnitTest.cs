using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;
using NSubstitute;
using NUnit.Framework;

namespace ATM_App2_UnitTest.UnitTestClasses
{
    class StringParserUnitTest
    {
        private StringParser _uut;
        [SetUp]
        [Author("Kasper Andersen")]
        public void Setup()
        {

            _uut = new StringParser();
        }


    
        [TestCase("ATR423;39045;8500;14000;20151006213456789", "ATR423", "39045", "8500", "14000", "20151006213456789")]
        [TestCase("ATR600;500;500;14000;20151006213456789", "ATR600", "500", "500", "14000", "20151006213456789")]
        [TestCase("ATR700;12000;12000;14000;20151006213456789", "ATR700", "12000", "12000", "14000", "20151006213456789")]
        [TestCase("ATR500;8000;7000;14000;20151006213456789", "ATR500", "8000", "7000", "14000", "20151006213456789")]
        public void ParseDataString_Test(string toParse, string token1, 
                                         string token2, string token3, string token4, string token5)
        {
            string[] tokens_;
            string[] rightTokens = new string[]{token1, token2, token3, token4, token5};
            tokens_ = _uut.ParseDataString(toParse);

            for (int i = 0; i < 5; i++)
            {
                Assert.That(tokens_[i], Is.EqualTo(rightTokens[i]));
            }

        }
    }
}
