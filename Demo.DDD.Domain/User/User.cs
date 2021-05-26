using System;
using System.Collections.Generic;
using Demo.DDD.Domain.Events;

namespace Demo.DDD.Domain
{
    public class User : IDispatchableAggregate, IFetchAbleAggregate
    {
        private readonly List<IEvent> _emittedEvents;

        public User()
        {
            _emittedEvents = new List<IEvent>();
        }

        IEnumerable<IEvent> IDispatchableAggregate.EmittedEvents => _emittedEvents;

        public PhoneNumber PhoneNumber { get; private set; }

        public UserId UserId { get; private set; }

        public UserName UserName { get; private set; }

        public static User Create(UserName userName)
        {
            var user = new User 
            {
                // Encapsulate ID creation logic inside domain.
                UserId = new UserId(Guid.NewGuid()), 
                UserName = userName, 
                PhoneNumber = PhoneNumber.EmptyPhoneNumber
            };

            user._emittedEvents.Add(new UserCreatedEvent(user.UserId, user.UserName));
            return user;
        }

        public void UpdatePhoneNumber(PhoneNumber phoneNumber)
        {
            if (UserName.IsDanishName)
            {
                PhoneNumber = phoneNumber;
            }
            else
            {
                throw new InvalidOperationException($"Unable to change {nameof(phoneNumber)} for other users than the ones with a danish name.");
            }

            _emittedEvents.Add(new PhoneNumberUpdatedEvent(UserId, PhoneNumber));
        }

        void IFetchAbleAggregate.Init(UserId userid, UserName userName, PhoneNumber phoneNumber)
        {
            UserId = userid;
            UserName = userName;
            PhoneNumber = phoneNumber;
        }
    }
}
