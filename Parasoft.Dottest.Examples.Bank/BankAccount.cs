using System.Collections.Generic;
using Parasoft.Dottest.Examples.Bank.Money;
using Parasoft.Dottest.Examples.Bank.Transactions;

namespace Parasoft.Dottest.Examples.Bank
{
    public class BankAccount
    {
        private BankUser _owner;
        private CurrencyInfo _currencyInfo;
        private AccountNumber _number;
        private Currency _balance;

        private IList<TransactionBase> _history = new List<TransactionBase>();
        public static IList<TransactionBase> GlobalHistory = new List<TransactionBase>();

        public BankAccount(BankUser owner, Currency balance, AccountNumber number)
        {
            _owner = owner;
            _currencyInfo = balance.CurrencyInfo;
            _number = number;
            _balance = balance;
        }

        public BankUser Owner { get { return _owner; } }

        public CurrencyInfo CurrencyInfo { get { return _currencyInfo; } }

        public Currency Balance { get { return _balance; } set { _balance = value; } }

        public AccountNumber Number { get { return _number; } }

        public IEnumerable<TransactionBase> History
        {
            get { return _history; }
        }

        public void Transfer(AccountNumber target, Currency amount)
        {
            
        }

        public void AddHistory(TransactionBase transaction)
        {
            _history.Add(transaction);
            GlobalHistory.Add(transaction);
        }
    }
}
