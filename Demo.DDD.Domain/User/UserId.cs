using System;

namespace Demo.DDD.Domain
{
    public class UserId : SingleValueObject<Guid>
    {
        public UserId(Guid value) : base(value)
        {
        }
    }
}