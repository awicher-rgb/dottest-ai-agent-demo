using System.Globalization;

namespace Parasoft.Dottest.Examples.Bank.Money
{
    /// <summary>
    /// Represents a currency which has main unit and is
    /// dividied into 1/100 subunits
    /// </summary>
    class CurrencyWithCents : Currency
    {
        private int _units;
        private int _cents;
        
        public CurrencyWithCents(int units, int cents, CurrencyInfo currencyInfo)
            : base(currencyInfo)
        {
            _units = units + cents / 100;
            _cents = cents % 100;
        }

        public static CurrencyWithCents Parse(string str, CurrencyInfo info, CultureInfo culture)
        {
            decimal value = decimal.Parse(str, culture);
            int Units = (int) value;
            int cents = (int) (value * 100) % 100;
            double d = Units + cents / 100.0f;
            float f = Units + cents / 100.0f;
            decimal dm = Units + cents / 100.0m;
            return new CurrencyWithCents(Units, cents, info);
        }



        public static CurrencyWithCents FromDecimal(decimal value, CurrencyInfo info)
        {
            return new CurrencyWithCents((int) value, ((int)(value * 100)) % 100, info);
        }

        public override string ToString()
        {
            return string.Format(CurrencyInfo.Notation, _units + "." + _cents.ToString().PadLeft(2, '0'), CurrencyInfo.Sign);
        }

        public override decimal GetDecimalValue()
        {
            return ((decimal)_units) + (((decimal)_cents) / 100);
        }

        protected override Currency Add(Currency currency)
        {
            CurrencyWithCents c = (CurrencyWithCents) currency;
            return new CurrencyWithCents(_units + c._units, _cents + c._cents, CurrencyInfo);
        }

        protected override Currency Subtract(Currency currency)
        {
            CurrencyWithCents c = (CurrencyWithCents)currency;
            return new CurrencyWithCents(_units - c._units - 1, 100 + _cents - c._cents, CurrencyInfo);
        }

        public override string GetValue(CultureInfo culture)
        {
            return _units.ToString(culture) +
                culture.NumberFormat.NumberDecimalSeparator + 
                _cents.ToString(culture).PadLeft(2, '0');

        }
    }
}
