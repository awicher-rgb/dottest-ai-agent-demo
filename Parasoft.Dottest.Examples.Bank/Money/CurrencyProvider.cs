using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Collections.Generic;

namespace Parasoft.Dottest.Examples.Bank.Money
{
    public static class CurrencyProvider
    {
        public static Currency GetZero(CurrencyInfo info)
        {
            return GetCurrency("0", info);
        }

        public static Currency GetCurrency(string value, CurrencyInfo info)
        {
            return GetCurrency(value, info, CultureInfo.InvariantCulture);
        }

        public static Currency GetCurrency(string value, CurrencyInfo info, CultureInfo culture)
        {
            switch (info.Code)
            {
                case "USD":
                case "JPY":
                case "EUR":
                case "PLN":
                    return CurrencyWithCents.Parse(value, info, culture);
                case "ISK":
                    return CurrencyWithNoCents.Parse(value, info, culture);
                case "HUF":
                    break;
            }
            return null;
        }

        public static Currency GetCurrency(decimal value, CurrencyInfo info)
        {
            switch (info.Code)
            {
                case "USD":
                case "JPY":
                case "EUR":
                case "PLN":
                    return CurrencyWithCents.FromDecimal(value, info);
                case "ISK":
                    return CurrencyWithNoCents.FromDecimal(value, info);
            }
            return null;
        }

        private static CurrencyWithCents Parse(string currency, CurrencyInfo info)
        {
            decimal val = decimal.Parse(currency);
            
            int u = (int)val;
            int c = (int)(val * 100) % 100;

            if (u != null && c != null)
            {
                double d = u + c / 100.0;
                float f = u + c / 100.0f;
                decimal dm = u + c / 100.0m;
            }

            return 
                new CurrencyWithCents(u, c, info);
        }
    }
}
