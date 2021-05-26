using System;
using System.Threading.Tasks;
using Demo.DDD.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Demo.DDD.ApplicationServices.Commands;
using Demo.DDD.ApplicationServices.QueryServices;
using Demo.DDD.ApplicationServices.ReadModels;
using Demo.DDD.Requests;
using Demo.DDD.Security;

namespace Demo.DDD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserDetailsQueryService _queryService;
        private readonly IMediator _commandBus;

        public UserController(IUserDetailsQueryService queryService, IMediator commandBus)
        {
            _queryService = queryService;
            _commandBus = commandBus;
        }

        [Authorize(Policy = ReadPolicy.PolicyName)]
        [HttpGet]
        [Route("{id}/details")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserDetailsReadModel), 200)]
        [ProducesResponseType(typeof(ProblemDetails),400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<UserDetailsReadModel> Get([FromRoute]Guid id)
        {
            return await _queryService.GetUserDetailsAsync(id);
        }

        [Authorize(Policy = WritePolicy.PolicyName)]
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<CreatedAtActionResult> CreateUser(CreateUserRequest createUserRequest)
        {
            var user = await _commandBus.Send(new CreateUserCommand(new UserName(createUserRequest.Name)));

            return CreatedAtAction(nameof(CreateUser), new { Id = user});
        }

        [Authorize(Policy = WritePolicy.PolicyName)]
        [HttpPatch]
        [Route("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(202)]
        [ProducesResponseType( 400)]
        [ProducesResponseType( 401)]
        [ProducesResponseType(typeof(ProblemDetails), 403)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<AcceptedAtActionResult> UpdatePhoneNumber([FromRoute]Guid id, UpdatePhoneNumberRequest updatePhoneNumberRequest)
        {
            var phoneNumber = new PhoneNumber(updatePhoneNumberRequest.CountryCode, updatePhoneNumberRequest.PhoneNumber);
            await _commandBus.Send(new UpdatePhoneNumberCommand(id, phoneNumber));

            return AcceptedAtAction(nameof(UpdatePhoneNumber));
        }
    }
}
