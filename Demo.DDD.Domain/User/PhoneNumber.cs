using System;
using System.Collections.Generic;

namespace Demo.DDD.Domain
{
    public sealed class PhoneNumber
    {
        public static readonly PhoneNumber EmptyPhoneNumber = new PhoneNumber();

        private static readonly Dictionary<string, string> PhoneNumberExtensions = new Dictionary<string, string> { { "DK", "+45" } };

        private static readonly Dictionary<string, int> PhoneNumberMap = new Dictionary<string, int> { { "DK", 8 } };

        public PhoneNumber(string countryCode, string number)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
                throw new ArgumentNullException(nameof(countryCode));

            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentNullException(nameof(number));


            ValidateCountryCode(countryCode);
            ValidatePhoneNumber(countryCode, number);

            CountryCode = countryCode;
            Number = number;
        }

        private PhoneNumber()
        {
        }

        public string CountryCode { get; private set; }

        public string Number { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return Equals(obj as PhoneNumber);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CountryCode, Number);
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Number))
            {
                return String.Empty;
            }

            return PhoneNumberExtensions[CountryCode] + Number;
        }

        private bool Equals(PhoneNumber other)
        {
            return CountryCode == other.CountryCode && Number == other.Number;
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }

            return true;
        }

        private void ValidateCountryCode(string countryCode)
        {
            if (!PhoneNumberMap.ContainsKey(countryCode.ToUpperInvariant()))
            {
                throw new ArgumentException(nameof(countryCode));
            }
        }

        private void ValidatePhoneNumber(string countrycode, string phoneNumber)
        {
            if (!IsDigitsOnly(phoneNumber))
            {
                throw new ArgumentException("Number must be digits only");
            }

            if (phoneNumber.Length != PhoneNumberMap[countrycode])
            {
                throw new ArgumentException($"{nameof(phoneNumber)} must be exactly {PhoneNumberMap[countrycode]} long");
            }
        }
    }
}