using System;

namespace Demo.DDD.Domain.Events
{
    public class UserCreatedEvent : IEvent
    {
        public UserCreatedEvent(UserId userId, UserName userName)
        {
            UserId = userId;
            UserName = userName;
        }

        public UserId UserId { get; private set; }

        public UserName UserName { get; private set; }
    }
}