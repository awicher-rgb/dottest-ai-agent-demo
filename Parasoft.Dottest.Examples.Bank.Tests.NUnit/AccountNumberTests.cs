using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parasoft.Dottest.Examples.Bank.Tests.NUnit
{
    [TestFixture]
    class AccountNumberTests
    {
        private static IEnumerable<string> InvalidAccountNumbers = new List<string>
        {
            "091a151413",
            "todo: create me",
            "invalid"
        };

        [Test]
        [TestCaseSource("InvalidAccountNumbers")]
        public void TestInvalidAccountNumberCreation(string accountNumber)
        {
            Assert.Throws<FormatException>(() => AccountNumber.Create(accountNumber));
        }

        private static IEnumerable<object[]> Parameters = new List<object[]>
        {
            new object[]{"12", new byte[]{1,2}},
            new object[]{"123456789123456", new byte[]{1,2,3,4,5,6,7,8,9,1,2,3,4,5,6}}
        };

        [Test]
        [TestCaseSource("Parameters")]
        public void TestAccountNumberCreation(string expectedNumber, byte[] bytes)
        {
            AccountNumber number = new AccountNumber(bytes);
            string accountNumber = number.ToString();
            Assert.That(accountNumber, Is.EqualTo(expectedNumber));
        }

        private static IEnumerable<List<byte[]>> AccountNumbers = new List<List<byte[]>>
        {
            new List<byte[]>
            {
                new byte[]{1,2,3},
                new byte[]{1,2,3},
                new byte[]{3,2,1},
                new byte[]{3,2,1}
            },

            new List<byte[]>
            {
                new byte[]{1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6},
                new byte[]{1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6},
                new byte[]{0,9,8,7,6,5,4,3,2,1,0,9,8,7,6,5},
                new byte[]{0,9,8,7,6,5,4,3,2,1,0,9,8,7,6,5}
            }
        };

        [Test]
        [TestCaseSource("AccountNumbers")]
        public void TestAccountNumberHashing(List<byte[]> accountNumbersCollections)
        {
            HashSet<AccountNumber> set = new HashSet<AccountNumber>();

            foreach (var accountNumbers in accountNumbersCollections)
            {
                AccountNumber accountNumber = new AccountNumber(accountNumbers);
                set.Add(accountNumber);
            }

            Assert.That(set.Count, Is.EqualTo(2));
        }
    }
}
