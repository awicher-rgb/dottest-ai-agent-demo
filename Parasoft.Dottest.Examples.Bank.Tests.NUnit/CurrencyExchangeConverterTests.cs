using NUnit.Framework;
using Parasoft.Dottest.Examples.Bank.Money;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parasoft.Dottest.Examples.Bank.Tests.NUnit
{
    [TestFixture]
    class CurrencyExchangeConverterTests
    {
        [Test]
        public void TestUsdConversion()
        {
            CurrencyInfo usd = new CurrencyInfo("USD", "$", "{1}{0}");
            CurrencyInfo eur = new CurrencyInfo("EUR", "€", "{1}{0}");

            CurrencyExchangeConverter converter = new CurrencyExchangeConverter(usd);
            converter.AddRatio(eur, 0.75f);
            Currency currency = CurrencyProvider.GetCurrency(100, eur);
            Currency convertedCurrency = converter.Convert(currency, usd);

            Currency expectedCurrency = CurrencyProvider.GetCurrency(100 / 0.75m, usd);

            Assert.That(convertedCurrency.GetDecimalValue(), Is.EqualTo(expectedCurrency.GetDecimalValue()));
        }
    }
}
