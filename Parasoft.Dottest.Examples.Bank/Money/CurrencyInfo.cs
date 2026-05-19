namespace Parasoft.Dottest.Examples.Bank.Money
{
    public class CurrencyInfo
    {
        private string _code;

        private string _sign;

        private string _notation;

        public CurrencyInfo(string code, string sign, string notation)
        {
            if (notation == null)
            {
                notation = "";
            }

            _code = code;
            _sign = sign;
            _notation = notation;
        }

        public string Code { get { return _code; } }

        public string Notation { get { return _notation; } }

        public string Sign { get { return _sign; } }
    }
}
