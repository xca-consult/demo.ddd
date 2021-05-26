using Swashbuckle.AspNetCore.Filters;
using Demo.DDD.Requests;

namespace Demo.DDD.Swagger
{
    public class CreateUserRequestExample : IExamplesProvider<CreateUserRequest>
    {
        public CreateUserRequest GetExamples()
        {
            return new CreateUserRequest { Name = "Jens Østergaard"};
        }
    }
}