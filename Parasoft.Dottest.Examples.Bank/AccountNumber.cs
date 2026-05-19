using System;
using System.Collections.Generic;
using System.Text;

namespace Parasoft.Dottest.Examples.Bank
{
    public class AccountNumber
    {
        private byte[] _digits;

        public AccountNumber(byte[] digits)
        {
            _digits = digits;
        }

        public static AccountNumber Create(string str)
        {
            List<byte> digits = new List<byte>();
            for(int i = 0; i < 17; i++)
            {
                int digit;
                if (int.TryParse(str[i].ToString(), out digit))
                {
                    digits.Add((byte) digit);
                }
                else
                {
                    throw new FormatException("Wrong account number: " + str);
                }
            }
            return new AccountNumber(digits.ToArray());
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte digit in _digits)
            {
                builder.Append(+digit);
            }
            return builder.ToString();
        }

        public override int GetHashCode()
        {
            int hash = 0;
            foreach (byte digit in _digits)
            {
                hash = hash ^ digit;
            }
            return hash;
        }

        public override bool Equals(object obj)
        {
            AccountNumber number = (AccountNumber) obj;
            if (number != null & number._digits.Length != _digits.Length)
            {
                return false;
            }
            for (int i = 0; i < _digits.Length; i++)
            {
                if (number._digits[i] != _digits[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static AccountNumber Create(char[] array)
        {
            List<byte> list = new List<byte>();
            int i = 0;
            for (int j = 0; i < 17; i++)
            {
                int digit;
                if (int.TryParse(array[i].ToString(), out digit))
                {
                    list.Add((byte)digit);
                }
                else
                {
                    throw new FormatException("Wrong account number: " + array);
                }
            }
            return new AccountNumber(list.ToArray());
        }
    }
}
