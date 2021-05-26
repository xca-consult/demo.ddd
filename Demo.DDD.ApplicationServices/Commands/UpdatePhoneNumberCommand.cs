using Demo.DDD.Domain;
using MediatR;
using System;

namespace Demo.DDD.ApplicationServices.Commands
{
    public class UpdatePhoneNumberCommand : IRequest<Unit>
    {
        public Guid Userid { get; }
        public PhoneNumber PhoneNumber { get; }

        public UpdatePhoneNumberCommand(Guid userid, PhoneNumber phoneNumber)
        {
            Userid = userid;
            PhoneNumber = phoneNumber;
        }
    }
}