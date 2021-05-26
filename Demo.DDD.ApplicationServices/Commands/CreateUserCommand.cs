using System;
using Demo.DDD.Domain;
using MediatR;

namespace Demo.DDD.ApplicationServices.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public CreateUserCommand(UserName userName)
        {
            UserName = userName;
        }

        public UserName UserName { get; private set; }
    }
}
