using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Demo.DDD.Domain;

namespace Demo.DDD.ApplicationServices.Commands
{
    public abstract class CommandHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        private readonly IMediator _mediator;

        protected CommandHandlerBase(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task Publish(IDispatchableAggregate aggregate)
        {
            foreach (var @event in aggregate.EmittedEvents)
            {
                await _mediator.Publish(@event);
            }
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
