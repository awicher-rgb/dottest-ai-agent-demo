using System.Globalization;

namespace Parasoft.Dottest.Examples.Bank.Money
{
    class CurrencyWithNoCents : Currency
    {
        private int _units;

        public CurrencyWithNoCents(int units, CurrencyInfo currencyInfo) : base(currencyInfo)
        {
            _units = units;
        }

        public static CurrencyWithNoCents Parse(string str, CurrencyInfo info, CultureInfo culture)
        {
            int units = int.Parse(str, culture);
            return new CurrencyWithNoCents(units, info);
        }

        public static CurrencyWithNoCents FromDecimal(decimal value, CurrencyInfo info)
        {
            return new CurrencyWithNoCents((int)value, info);
        }

        public override decimal GetDecimalValue()
        {
            return _units;
        }

        protected override Currency Add(Currency currency)
        {
            CurrencyWithNoCents c = (CurrencyWithNoCents) currency;
            return new CurrencyWithNoCents(_units + c._units, this.CurrencyInfo);
        }

        protected override Currency Subtract(Currency currency)
        {
            CurrencyWithNoCents c = (CurrencyWithNoCents)currency;
            return new CurrencyWithNoCents(_units - c._units, this.CurrencyInfo);
        }

        public override string GetValue(CultureInfo culture)
        {
            return _units.ToString(culture);
        }
    }
}
