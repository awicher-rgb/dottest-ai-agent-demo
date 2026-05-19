using System;
using System.Globalization;

namespace Parasoft.Dottest.Examples.Bank.Money
{
    public abstract class Currency
    {
        private CurrencyInfo _currencyInfo;

        protected Currency(CurrencyInfo currencyInfo)
        {
            _currencyInfo = currencyInfo;
        }

        public CurrencyInfo CurrencyInfo { get { return _currencyInfo; } }
        
        public string GetValueWithSign(CultureInfo culture)
        {
            return string.Format(CurrencyInfo.Notation, GetValue(culture), this.CurrencyInfo.Sign);
        }

        public static Currency operator+(Currency c1, Currency c2)
        {
            if (c1.CurrencyInfo == c2.CurrencyInfo)
            {
                return c1.Add(c2);
            }
            else
            {
                throw new NotSupportedException("Cannot add different currencies");
            }
        }

        public static Currency operator -(Currency c1, Currency c2)
        {
            if (c1.CurrencyInfo == c2.CurrencyInfo)
            {
                return c1.Subtract(c2);
            }
            else
            {
                throw new NotSupportedException("Cannot add different currencies");
            }
        }

        public bool IsZero()
        {
            double d = (double)this.GetDecimalValue();
            return  d == 0;
        }

        public abstract decimal GetDecimalValue();

        protected abstract Currency Add(Currency c);

        protected abstract Currency Subtract(Currency c);

        public abstract string GetValue(CultureInfo culture);

        public override bool Equals(object obj)
        {
            return obj == this;
        }
    }
}
