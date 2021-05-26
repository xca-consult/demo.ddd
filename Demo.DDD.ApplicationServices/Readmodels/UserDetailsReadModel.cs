using System;

namespace Demo.DDD.ApplicationServices.ReadModels
{
    public class UserDetailsReadModel
    {
        public Guid UserId { get; }
        
        public string UserName { get; }

        public string PhoneNumber { get; }

        public UserDetailsReadModel(Guid userid, string username, string phoneNumber)
        {
            UserId = userid;
            UserName = username;
            PhoneNumber = phoneNumber;
        }
    }
}