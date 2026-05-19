using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parasoft.Dottest.Examples.Bank.Tests.MSTest
{
    [TestClass]
    public class AccountNumberTests
    {
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestInvalidAccountNumberCreation()
        {
            var accountNumber = "invalid";
            AccountNumber number = AccountNumber.Create(accountNumber);
            Assert.IsNotNull(number);
        }

        [TestMethod]
        public void TestAccountNumberCreation()
        {
            var expectedNumber = "123456789123456";
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 6 };
            AccountNumber number = new AccountNumber(bytes);
            string accountNumber = number.ToString();
            Assert.AreEqual(expectedNumber, accountNumber);
        }

        [TestMethod]
        public void TestAccountNumberHashing()
        {
            var accountNumbersCollections = new List<byte[]>
            {
                new byte[]{1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6},
                new byte[]{1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6},
                new byte[]{0,9,8,7,6,5,4,3,2,1,0,9,8,7,6,5},
                new byte[]{0,9,8,7,6,5,4,3,2,1,0,9,8,7,6,5}
            };
            HashSet<AccountNumber> set = new HashSet<AccountNumber>();

            foreach (var accountNumbers in accountNumbersCollections)
            {
                AccountNumber accountNumber = new AccountNumber(accountNumbers);
                set.Add(accountNumber);
            }

            Assert.AreEqual(2, set.Count);
        }
    }
}
