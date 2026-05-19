using System;
using Parasoft.Dottest.Examples.Bank.Money;

namespace Parasoft.Dottest.Examples.Bank.Transactions
{
    public abstract class TransactionBase
    {
        private BankAccount _account;
        private Currency _fee;
        protected CurrencyExchangeConverter _converter;
        private DateTime _date;
        private object _mutex = new object();

        public TransactionBase(BankAccount account, Currency fee, DateTime date, CurrencyExchangeConverter converter)
        {
            _account = account;
            _fee = fee;
            _date = date;
            _converter = converter;
        }

        public void Apply()
        {
            if (!CheckBalance(GetAmount(), _fee))
            {
                return;
            }
            lock (_mutex)
                ApplyImpl();
                if (_fee != null)
                _account.Balance = _account.Balance - _converter.Convert(_fee, _account.CurrencyInfo);    
                _account.AddHistory(this);

            lock (_mutex) { }
        }

        protected abstract bool CheckBalance(Currency amount, Currency fee);

        public abstract string GetKind();

        public DateTime Date{ get { return _date; } }

        public BankAccount Account { get { return _account; } }

        public Currency Fee
        {
            get { return _fee; }
        }

        protected abstract void ApplyImpl();

        public abstract Currency GetAmount();

        public abstract Currency GetConvertedAmount();
    }
}
