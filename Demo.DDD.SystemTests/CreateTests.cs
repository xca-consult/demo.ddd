using Demo.DDD.SystemTests.Setup;
using Xunit;

namespace Demo.DDD.SystemTests
{
    public class CreateTests : IClassFixture<ApplicationFixture>
    {
        private readonly ApplicationFixture _applicationFixture;
        private readonly UserApi _userApi;

        public CreateTests(ApplicationFixture applicationFixture)
        {
            _applicationFixture = applicationFixture;
            _userApi = new UserApi();
        }

        [Fact]
        public async void TestCreate()
        {
            // Arrange
            var client = _applicationFixture.HttpClient;

            var id = await _userApi.CreateUser(client, "erik");

            var details = await _userApi.GetUserDetails(client, id);

            //Assert
            Assert.NotNull(details);
            Assert.Equal(id, details.UserId);
            Assert.Equal("erik", details.UserName);
            Assert.Equal(string.Empty, details.PhoneNumber);
        }
    }
}
