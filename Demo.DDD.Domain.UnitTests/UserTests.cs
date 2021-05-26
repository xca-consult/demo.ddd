using Demo.DDD.Domain;
using Xunit;
using System;

namespace Demo.DDD.Domain.UnitTests
{
    public class UserTests
    {
        [Fact]
        public void CreateUser_WithCorrectInput_ShouldHaveBlankPhoneNumber()
        {
            var user = User.Create(new UserName("erik"));

            Assert.Equal(user.PhoneNumber, PhoneNumber.EmptyPhoneNumber);
        }

        [Theory]
        [InlineData("østergaard")]
        [InlineData("æstergaard")]
        [InlineData("åstergaard")]
        public void UpdatePhoneNumber_WithDanishName_Succeeds(string name)
        {
            var user = User.Create(new UserName(name));

            var phoneNumber = new PhoneNumber("DK", "13445566");
            user.UpdatePhoneNumber(phoneNumber);

            Assert.Equal(user.PhoneNumber, phoneNumber);
        }

        [Fact]
        public void UpdatePhoneNumber_WithNonDanishName_ShouldThrow()
        {
            var user = User.Create(new UserName("foo"));

            var phoneNumber = new PhoneNumber("DK", "13445566");
            Assert.Throws<InvalidOperationException>(()=> user.UpdatePhoneNumber(phoneNumber));
        }
    }
}
