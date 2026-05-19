using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Parasoft.Dottest.Examples.Bank.Money;

namespace Parasoft.Dottest.Examples.Bank
{
    public class ExampleBank : Bank
    {
        private ExampleBank()
        {
        }

        public static Bank CreateExampleBank()
        {
            Bank bank = new Bank();
            //parasoft-begin-suppress CS.INTER.ITT
            bank.AddCurrency(new CurrencyInfo("USD", "$", "{1}{0}"));
            bank.AddCurrency(new CurrencyInfo("EUR", "€", "{1}{0}"));
            bank.AddCurrency(new CurrencyInfo("JPY", "¥", "{1}{0}"));
            bank.AddCurrency(new CurrencyInfo("PLN", "zł", "{0} {1}"));
            bank.AddCurrency(new CurrencyInfo("ISK", "kr", "{0} {1}"));

            BankUser user1 = new BankUser("John", "White", "jwhite", "jwhite");
            BankUser user2 = new BankUser("Angela", "Smith", "asmith", "asmith");
            BankUser user3 = new BankUser("Kenta", "Suzuki", "ksuzuki", "ksuzuki");

            bank.AddUser(user1);
            bank.AddUser(user2);
            bank.AddUser(user3);

            bank.AddAccount(new BankAccount(user1, CurrencyProvider.GetCurrency(1323.12m, bank.GetCurrency("USD")),  AccountNumber.Create("84534789450005711")));
            bank.AddAccount(new BankAccount(user1, CurrencyProvider.GetCurrency(782.32m, bank.GetCurrency("EUR")), AccountNumber.Create("12534789451800068")));
            bank.AddAccount(new BankAccount(user1, CurrencyProvider.GetCurrency(2182.98m, bank.GetCurrency("JPY")), AccountNumber.Create("67534000458748357")));
            bank.AddAccount(new BankAccount(user1, CurrencyProvider.GetCurrency(82402m, bank.GetCurrency("ISK")), AccountNumber.Create("67534789455487870")));
            
            bank.AddAccount(new BankAccount(user2, CurrencyProvider.GetCurrency(18681.20m, bank.GetCurrency("EUR")), AccountNumber.Create("32534789459735154")));
            bank.AddAccount(new BankAccount(user3, CurrencyProvider.GetCurrency(5111.71m, bank.GetCurrency("JPY")), AccountNumber.Create("67534789450120008")));

            bank.Coverter = new CurrencyExchangeConverter(bank.GetCurrency("USD"));

            bank.Coverter.AddRatio(bank.GetCurrency("EUR"), 0.775f);
            bank.Coverter.AddRatio(bank.GetCurrency("JPY"), 95.71f);
            bank.Coverter.AddRatio(bank.GetCurrency("ISK"), 125.96f);
            bank.Coverter.AddRatio(bank.GetCurrency("PLN"), 3.243f);

            //Make some transactions

            IList<BankAccount> user1Accounts = bank.GetAccounts(user1);
            IList<BankAccount> user2Accounts = bank.GetAccounts(user2);
            IList<BankAccount> user3Accounts = bank.GetAccounts(user3);

            Currency amount = CurrencyProvider.GetCurrency("1000", 
                        user1Accounts[0].CurrencyInfo, Thread.CurrentThread.CurrentCulture);
            bank.Transfer(user1Accounts[0], user1Accounts[1].Number, amount);

            amount = CurrencyProvider.GetCurrency("1000",
                        user1Accounts[2].CurrencyInfo, Thread.CurrentThread.CurrentCulture);
            bank.Transfer(user1Accounts[2], user2Accounts[0].Number, amount);

            amount = CurrencyProvider.GetCurrency("5000",
                        user1Accounts[3].CurrencyInfo, Thread.CurrentThread.CurrentCulture);
            bank.Transfer(user1Accounts[3], user1Accounts[0].Number, amount);

            amount = CurrencyProvider.GetCurrency("50",
                        user1Accounts[1].CurrencyInfo, Thread.CurrentThread.CurrentCulture);
            bank.Transfer(user1Accounts[1], user1Accounts[2].Number, amount);

            amount = CurrencyProvider.GetCurrency("250",
                        user3Accounts[0].CurrencyInfo, Thread.CurrentThread.CurrentCulture);
            bank.Transfer(user3Accounts[0], user1Accounts[2].Number, amount);

            amount = CurrencyProvider.GetCurrency("350",
                        user3Accounts[0].CurrencyInfo, Thread.CurrentThread.CurrentCulture);
            bank.Transfer(user3Accounts[0], user1Accounts[2].Number, amount);

            //parasoft-end-suppress CS.INTER.ITT));
            return bank;
        }
    }
}
