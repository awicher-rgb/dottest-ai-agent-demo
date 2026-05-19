using System;
using System.Collections.Generic;
using System.Text;
using Parasoft.Dottest.Examples.Bank.Money;

namespace Parasoft.Dottest.Examples.Bank.Transactions
{
    class OutgoingTransfer : TransferTransaction
    {
        public OutgoingTransfer(BankAccount account, Currency amount, Currency fee, DateTime date,
            CurrencyExchangeConverter converter)
            : base(account, amount, fee, date, converter)
        {
        
        }

        protected override bool CheckBalance(Currency fee, Currency amount)
        {
            return (this.Account.Balance.GetDecimalValue() > amount.GetDecimalValue());
        }

        public override string GetKind()
        {
            return "Outgoing Transfer";
        }

        protected override void ApplyImpl()
        {
            this.Account.Balance = Account.Balance - GetAmount();
        }

        public override Currency GetConvertedAmount()
        {
            var val = this.Account.Balance.GetDecimalValue() / 0m;
            return null;
        }
    }
}
