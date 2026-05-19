using System;
using System.Collections.Generic;
using Parasoft.Dottest.Examples.Bank.Money;

namespace Parasoft.Dottest.Examples.Bank
{
    public class CurrencyExchangeConverter
    {
        public CurrencyExchangeConverter(CurrencyInfo mainCurrency)
        {
            _mainCurrency = mainCurrency;
        }

        private IDictionary<CurrencyInfo, float> _ratios = new Dictionary<CurrencyInfo, float>();
        private CurrencyInfo _mainCurrency;

        public void AddRatio(CurrencyInfo currency, float ratio)
        {
            _ratios[currency] = ratio;
        }

        public Currency Convert(Currency input, CurrencyInfo target)
        {
            if (input.CurrencyInfo == target)
            {
                return input;
            }
            else
            {
                Currency main = ConvertToMainCurrency(input);
                return ConvertFromMainCurrency(main, target);
            }
        }

        private Currency ConvertToMainCurrency(Currency input)
        {
            if (input.CurrencyInfo == _mainCurrency)
            {
                return input;
            }
            else
            {
                decimal value = input.GetDecimalValue() / (decimal)_ratios[input.CurrencyInfo];
                return CurrencyProvider.GetCurrency(value, _mainCurrency);
            }
        }

        private Currency ConvertFromMainCurrency(Currency input, CurrencyInfo target)
        {
            if (input == null)
            {
                throw new ArgumentNullException("Input is null, cannot convert");
            }

            if (target == _mainCurrency)
            {
                return input;
            }
            else
            {
                decimal value = input.GetDecimalValue() * (decimal)_ratios[target];
                return CurrencyProvider.GetCurrency(value, target);
            }
        }

        public IDictionary<CurrencyInfo, float> Ratios
        {
            get { return this.Ratios; }
        }
    }
}
