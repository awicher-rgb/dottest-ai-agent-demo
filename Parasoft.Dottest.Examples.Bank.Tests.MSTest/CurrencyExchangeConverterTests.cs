using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parasoft.Dottest.Examples.Bank.Money;

namespace Parasoft.Dottest.Examples.Bank.Tests.MSTest
{
    [TestClass]
    public class CurrencyExchangeConverterTests
    {
        [TestMethod]
        public void TestUsdConversion()
        {
            CurrencyInfo usd = new CurrencyInfo("USD", "$", "{1}{0}");
            CurrencyInfo eur = new CurrencyInfo("EUR", "€", "{1}{0}");

            CurrencyExchangeConverter converter = new CurrencyExchangeConverter(usd);
            converter.AddRatio(eur, 0.75f);
            Currency currency = CurrencyProvider.GetCurrency(100, eur);
            Currency convertedCurrency = converter.Convert(currency, usd);

            Currency expectedCurrency = CurrencyProvider.GetCurrency(100 / 0.75m, usd);

            Assert.AreEqual(expectedCurrency.GetDecimalValue(), convertedCurrency.GetDecimalValue());
        }
    }
}
