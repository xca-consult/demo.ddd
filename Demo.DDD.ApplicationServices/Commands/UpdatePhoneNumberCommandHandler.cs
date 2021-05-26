using System.Threading;
using System.Threading.Tasks;
using Demo.DDD.Domain;
using Integration.Database;
using MediatR;

namespace Demo.DDD.ApplicationServices.Commands
{
    public class UpdatePhoneNumberCommandHandler : CommandHandlerBase<UpdatePhoneNumberCommand, Unit>
    {
        private readonly IReaderWriterRepository<User> _userAggregateRepository;

        public UpdatePhoneNumberCommandHandler(IReaderWriterRepository<User> userAggregateRepository, IMediator mediator) : base(mediator)
        {
            _userAggregateRepository = userAggregateRepository;
        }

        public override async Task<Unit> Handle(UpdatePhoneNumberCommand command, CancellationToken cancellationToken)
        {
            var user = await _userAggregateRepository.FindByIdAsync(command.Userid);

            if (user == null)
            {
                throw new AggregateNotFoundException(command.Userid);
            }

            user.UpdatePhoneNumber(command.PhoneNumber);

            await _userAggregateRepository.AddAsync(user);

            await Publish(user);

            return Unit.Value;
        }
    }
}