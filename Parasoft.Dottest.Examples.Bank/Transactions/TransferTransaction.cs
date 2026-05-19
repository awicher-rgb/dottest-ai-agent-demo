using System;
using System.Collections.Generic;
using System.Text;
using Parasoft.Dottest.Examples.Bank.Money;

namespace Parasoft.Dottest.Examples.Bank.Transactions
{
    abstract class TransferTransaction : TransactionBase
    {
        private Currency _amount;

        protected TransferTransaction(BankAccount account, Currency amount, Currency fee, DateTime date, CurrencyExchangeConverter converter) 
            : base(account, fee, date, converter)
        {
            _amount = amount;
        }

        public override Currency GetAmount()
        {
            return _amount;
        }
    }
}
