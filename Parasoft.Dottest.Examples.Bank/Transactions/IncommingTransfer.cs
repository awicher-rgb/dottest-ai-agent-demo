using System;
using Parasoft.Dottest.Examples.Bank.Money;

namespace Parasoft.Dottest.Examples.Bank.Transactions
{
    class IncommingTransfer : TransferTransaction
    {
        private Currency _convertedAmount;

        public IncommingTransfer(BankAccount account, Currency amount, DateTime date, CurrencyExchangeConverter converter)
            : base(account, amount, null, date, converter)
        {
            
        }

        protected override bool CheckBalance(Currency amount, Currency fee)
        {
            return true;
        }

        public override string GetKind()
        {
            return "Incomming Transfer";
        }

        protected override void ApplyImpl()
        {
            _convertedAmount = _converter.Convert(GetAmount(), this.Account.CurrencyInfo);
            this.Account.Balance = Account.Balance + _convertedAmount;    
        }

        public override Currency GetConvertedAmount()
        {
            return _convertedAmount;
        }
    }
}
