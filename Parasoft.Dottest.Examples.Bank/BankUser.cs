using System.Text.RegularExpressions;
using Parasoft.Dottest.Examples.Bank.Money;

namespace Parasoft.Dottest.Examples.Bank
{
    public class BankUser
    {
        private const string EmailRegularExpression = @"^[\w\-\+\&\*]+(?:\.[\w\-\_\+\&\*]+)*@(?:[\w-]+\.)+[a-zA-Z]2,7}$";

        private string _name;
        private string _sirName;
        private string _login;
        private string _password;
        private string _email;

        public BankUser(string name, string sirName, string login, string password)
        {
            _name = name;
            _sirName = sirName;
            _login = login;
            _password = password;
        }

        public string Email { get { return _email; } set { _email = value; } }

        public bool CheckEmail()
        {
            if (Regex.IsMatch(_email, EmailRegularExpression))
            {
                return true;
            }
            return false;
        }

        public string Name { get { return _name; } }
        
        public string SirName { get { return _sirName; } }

        public string Password { get { return _password; } }

        public string Login { get { return _login; } }
    }
}
