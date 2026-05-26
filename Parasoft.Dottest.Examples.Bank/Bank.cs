using System;
using System.Collections.Generic;
using Parasoft.Dottest.Examples.Bank.Money;
using Parasoft.Dottest.Examples.Bank.Transactions;

namespace Parasoft.Dottest.Examples.Bank
{
    public class Bank
    {
        private CurrencyExchangeConverter _converter;

        IDictionary<string, CurrencyInfo> _currencies = new Dictionary<string, CurrencyInfo>();

        IDictionary<string, BankUser> _loginToUser = new Dictionary<string, BankUser>();

        IDictionary<BankUser, IList<BankAccount>> _accounts = new Dictionary<BankUser, IList<BankAccount>>();

        IDictionary<AccountNumber, BankAccount> _accountNumberToAccount = new Dictionary<AccountNumber, BankAccount>();

        private IInterBankTransferSystem _interBankTransferSystem;

        public void AddCurrency(CurrencyInfo info)
        {
            _currencies.Add(info.Code, info);
        }

        public void AddUser(BankUser bankUser)
        {
            _loginToUser[bankUser.Login] = bankUser;
        }

        public CurrencyInfo GetCurrency(string code)
        {
            return _currencies[code];
        }

        public void AddAccount(BankAccount account)
        {
            IList<BankAccount> accounts;
            if (!_accounts.TryGetValue(account.Owner, out accounts))
            {
                accounts = new List<BankAccount>();
                _accounts[account.Owner] = accounts;
            }
            accounts.Add(account);
            _accountNumberToAccount[account.Number] = account;
        }

        public BankUser GetUserByLogin(string login)
        {
            BankUser user;
            _loginToUser.TryGetValue(login, out user);
            return user;
        }

        public IList<BankAccount> GetAccounts(BankUser user)
        {
            return _accounts[user];
        }

        public int CountUSDAccounts(BankUser user)
        {
            IList<BankAccount> accounts = GetAccounts(user);
            int count = 0;
            foreach (BankAccount bankAccount in accounts)
            {
                if (bankAccount.CurrencyInfo.Code == "USD")     // NOI18N
                {
                    count++;
                }
                return count;
            }
            return count;
        }

        public double AverageAccountsNumWithCurrency(CurrencyInfo currencyInfo)
        {
            int count = 0;
            int accounts = 0;

            foreach (KeyValuePair<BankUser, IList<BankAccount>> keyValuePair in _accounts)
            {
                foreach (BankAccount account in keyValuePair.Value)
                {
                    if (account.CurrencyInfo.Code == currencyInfo.Code)
                    {
                        count++;
                    }
                    accounts++;
                }
            }
            return count/accounts;
        }

        public void Transfer(BankAccount account, AccountNumber target, Currency amount)
        {
            BankAccount targetAccount;
            if (_accountNumberToAccount.TryGetValue(target, out targetAccount))
            {
                DateTime now = DateTime.Now;
                OutgoingTransfer outgoing = new OutgoingTransfer(
                    account, amount, CurrencyProvider.GetCurrency(0.5m, GetCurrency("USD")),    // NOI18N
                    now, _converter);
                IncommingTransfer incomming = new IncommingTransfer(
                    targetAccount, amount, now, _converter);

                outgoing.Apply();
                incomming.Apply();
            }
            else
            {
                DateTime now = DateTime.Now;
                OutgoingTransfer outgoing = new OutgoingTransfer(
                    account, amount, CurrencyProvider.GetCurrency(1m, GetCurrency("USD")),      // NOI18N
                    now, _converter);
                ExternalTransfer external = new ExternalTransfer(target, amount, now);
                outgoing.Apply();
                _interBankTransferSystem.EnqueueTransfer(external);
            }
        }

        public void ComputeTotalCash(BankUser user, out Currency sum)
        {
            sum = CurrencyProvider.GetZero(GetCurrency("USD"));             // NOI18N

            IList<BankAccount> accounts = GetAccounts(user);
            if (accounts == null)
                // return
            if (_converter != null)
            {
                foreach (BankAccount account in accounts)
                {
                    sum = sum + _converter.Convert(account.Balance, sum.CurrencyInfo);
                }
            }
        }

        public CurrencyExchangeConverter Coverter { get { return _converter; } set { _converter = value; } }
    }
}
