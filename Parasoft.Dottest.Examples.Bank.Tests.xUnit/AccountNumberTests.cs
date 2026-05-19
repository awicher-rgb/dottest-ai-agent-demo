using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Parasoft.Dottest.Examples.Bank.Tests.xUnit
{
    public class AccountNumberTests
    {
        public static IEnumerable<object[]> InvalidAccountNumbers = new List<object[]>
        {
            new object[] { "091a151413" },
            new object[] { "todo: create me" },
            new object[] { "invalid" }
        };

        [Theory]
        [MemberData("InvalidAccountNumbers")]
        public void TestInvalidAccountNumberCreation(string accountNumber)
        {
            Assert.Throws<FormatException>(() => AccountNumber.Create(accountNumber));
        }

        public static IEnumerable<object[]> Parameters = new List<object[]>
        {
            new object[]{"12", new byte[]{1,2}},
            new object[]{"123456789123456", new byte[]{1,2,3,4,5,6,7,8,9,1,2,3,4,5,6}}
        };

        [Theory]
        [MemberData("Parameters")]
        public void TestAccountNumberCreation(string expectedNumber, byte[] bytes)
        {
            AccountNumber number = new AccountNumber(bytes);
            string accountNumber = number.ToString();
            Assert.Equal(expectedNumber, accountNumber);
        }
    }
}
