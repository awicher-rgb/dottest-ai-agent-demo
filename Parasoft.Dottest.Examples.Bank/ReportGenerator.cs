using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Parasoft.Dottest.Examples.Bank
{
    class ReportGenerator
    {
        private readonly StreamWriter _writer;
        private readonly Bank _bank;
        private IDictionary<AccountNumber, Comment> _comments = new Dictionary<AccountNumber, Comment>();

        public ReportGenerator(Bank bank, string path)
        {
            _writer = new StreamWriter(path);
            _bank = bank;
        }

        public void AddUser(BankUser user)
        {
            _writer.WriteLine(user.Name + " " + user.Password);
            foreach (BankAccount account in _bank.GetAccounts(user))
            {
                string balance = account.Balance.GetValueWithSign(CultureInfo.InvariantCulture);
                _writer.WriteLine(string.Format("Account: {0} Balance {1} ", account.Number + balance)); // NOI18N
                Comment comment;
                if (_comments.TryGetValue(account.Number, out comment))
                {
                    _writer.WriteLine(comment.Text);
                }
            }
        }

        public void AddInfo(Comment comment)
        {
            _comments.Add(comment.Number, comment);
        }
    }

    internal class Comment
    {
        private string _comment;
        private AccountNumber _number;

        private Comment()
        {
        }

        public string Text { get { return _comment; } set { _comment = value; } }
        public AccountNumber Number { get { return _number; } set { _number = value; }}
    }

}
