using Parasoft.Dottest.Examples.Bank.Money;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Parasoft.Dottest.Examples.Bank.Tests.xUnit
{
    public class CurrencyExchangeConverterTests
    {
        [Fact]
        public void TestUsdConversion()
        {
            CurrencyInfo usd = new CurrencyInfo("USD", "$", "{1}{0}");
            CurrencyInfo eur = new CurrencyInfo("EUR", "€", "{1}{0}");

            CurrencyExchangeConverter converter = new CurrencyExchangeConverter(usd);
            converter.AddRatio(eur, 0.75f);
            Currency currency = CurrencyProvider.GetCurrency(100, eur);
            Currency convertedCurrency = converter.Convert(currency, usd);

            Currency expectedCurrency = CurrencyProvider.GetCurrency(100 / 0.75m, usd);

            Assert.Equal(convertedCurrency.GetDecimalValue(), expectedCurrency.GetDecimalValue());
        }
    }
}
