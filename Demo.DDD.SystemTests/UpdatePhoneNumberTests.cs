using System;
using System.Net;
using Xunit;
using Demo.DDD.ApplicationServices.ReadModels;

namespace Demo.DDD.SystemTests
{
    public class UpdatePhoneNumberTests : IClassFixture<ApplicationFixture>
    {
        private readonly ApplicationFixture _applicationFixture;
        private readonly UserApi _userApi;

        public UpdatePhoneNumberTests(ApplicationFixture applicationFixture)
        {
            _applicationFixture = applicationFixture;
            _userApi = new UserApi();
        }

        [Fact]
        public async void TestUpdatePhoneSunshine()
        {
            var name = "Ole Østergaard";

            var id = await _userApi.CreateUser(_applicationFixture.HttpClient, name);

            var details = await _userApi.GetUserDetails(_applicationFixture.HttpClient, id);

            AssertDetails(id, name, "", details);

            await _userApi.UpdatePhone(_applicationFixture.HttpClient, id, "DK", "12345678");

            details = await _userApi.GetUserDetails(_applicationFixture.HttpClient, id);

            AssertDetails(id, name, "+4512345678", details);
        }

        [Fact]
        public async void TestUpdatePhoneNoDanishName()
        {
            var name = "Ole Olsen";

            var id = await _userApi.CreateUser(_applicationFixture.HttpClient, name);

            var details = await _userApi.GetUserDetails(_applicationFixture.HttpClient, id);

            AssertDetails(id, name, "", details);

            var statusCode = await _userApi.UpdatePhone(_applicationFixture.HttpClient, id, "DK", "12345678");
            Assert.Equal(HttpStatusCode.Conflict, statusCode);
        }

        private void AssertDetails(Guid id, string name, string phone, UserDetailsReadModel details)
        {
            //Assert
            Assert.NotNull(details);
            Assert.Equal(id, details.UserId);
            Assert.Equal(name, details.UserName);
            Assert.Equal(phone, details.PhoneNumber);
        }

      
    }
}
