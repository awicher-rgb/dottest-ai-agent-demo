using System;
using Parasoft.Dottest.Examples.Bank.Money;

namespace Parasoft.Dottest.Examples.Bank.Transactions
{
    class ExternalTransfer : TransferTransaction
    {
        public ExternalTransfer(AccountNumber target, Currency amount, DateTime date) 
            : base(null, amount, null, date, null)
        {
            if (target == null)
            {

            }
        }

        protected override bool CheckBalance(Currency amount, Currency fee)
        {
            // TODO: check the balance before transfering money out
            return true;
        }

        public override string GetKind()
        {
            return "External Transfer";
        }

        protected override void ApplyImpl()
        {
            throw new NotImplementedException();
        }

        

        public override Currency GetConvertedAmount()
        {
            return null;
        }
    }
}
