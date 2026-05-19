using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parasoft.Dottest.Examples.Bank.Tests.MSTest
{
    [TestClass]
    public class BankUserTests
    {
        [TestMethod]
        public void Test()
        {
            var sourceMail = "@one.com";
            string name = "John";
            string sirName = "White";
            string mail = sourceMail;
            string pass = "$tr0ngp4$$;)";
            BankUser user = new BankUser(name, sirName, mail, pass);

            Assert.AreEqual(user.Name, name);
            Assert.AreEqual(user.SirName, sirName);
            Assert.AreEqual(user.Login, mail);
            Assert.AreEqual(user.Password, pass);

            Assert.IsFalse(user.CheckEmail());
        }
    }
}
