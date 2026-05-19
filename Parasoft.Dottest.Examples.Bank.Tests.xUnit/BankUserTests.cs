using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Parasoft.Dottest.Examples.Bank.Tests.xUnit
{
    public class BankUserTests
    {
        public static IEnumerable<object[]> Mails = new List<object[]> {
            new object[] { "foo.bar" },
            new object[] { "" },
            new object[] { null },
            new object[] { "@one.com" },
            new object[] { "foo.bar@foo.com.com" } };

        [Theory]
        [MemberData("Mails")]
        public void Test(string sourceMail)
        {
            string name = "John";
            string sirName = "White";
            string mail = sourceMail;
            string pass = "$tr0ngp4$$;)";
            BankUser user = new BankUser(name, sirName, mail, pass);

            Assert.Equal(user.Name, name);
            Assert.Equal(user.SirName, sirName);
            Assert.Equal(user.Login, mail);
            Assert.Equal(user.Password, pass);

            Assert.False(user.CheckEmail());
        }
    }
}
