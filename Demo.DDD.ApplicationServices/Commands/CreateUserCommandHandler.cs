using System;
using System.Threading;
using System.Threading.Tasks;
using Demo.DDD.Domain;
using Integration.Database;
using MediatR;

namespace Demo.DDD.ApplicationServices.Commands
{
    public class CreateUserCommandHandler : CommandHandlerBase<CreateUserCommand, Guid>
    {
        private readonly IWriteRepository<User> _userAggregateRepository;

        public CreateUserCommandHandler(IWriteRepository<User> userAggregateRepository, IMediator mediator) : base(mediator)
        {
            _userAggregateRepository = userAggregateRepository;
        }

        public override async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        { 
            var user = User.Create(request.UserName);
            
            await _userAggregateRepository.AddAsync(user);

            await Publish(user);

            return user.UserId.Value;
        }
    }
}
