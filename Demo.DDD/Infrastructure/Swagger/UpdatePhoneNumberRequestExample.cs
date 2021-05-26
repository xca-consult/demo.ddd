using Swashbuckle.AspNetCore.Filters;
using Demo.DDD.Requests;

namespace Demo.DDD.Swagger
{
    public class UpdatePhoneNumberRequestExample : IExamplesProvider<UpdatePhoneNumberRequest>
    {
        public UpdatePhoneNumberRequest GetExamples()
        {
            return new UpdatePhoneNumberRequest{ CountryCode = "DK", PhoneNumber = "23232323"};
        }
    }
}
