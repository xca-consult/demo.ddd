using System;

namespace Demo.DDD.Domain.Events
{
    public class PhoneNumberUpdatedEvent : IEvent
    {
        public UserId UserId { get; private set; }

        public PhoneNumber PhoneNumber { get; private set;}

        public PhoneNumberUpdatedEvent(UserId userId, PhoneNumber phoneNumber)
        {
            UserId = userId;
            PhoneNumber = phoneNumber;
        }
    }
}
