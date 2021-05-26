using System;
using System.Linq;

namespace Demo.DDD.Domain
{
    public class UserName : SingleValueObject<string>
    {
        private readonly string _name;

        public UserName(string name):base(name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (name.Length < 2)
            {
                throw new ArgumentException(nameof(name));
            }

            this._name = name;
        }

        public bool IsDanishName => this._name.ToLower().Any(v => v.ToString().Contains('æ') || v.ToString().Contains('ø') || v.ToString().Contains('å'));

        public static implicit operator string(UserName d) => d._name;
    }
}