using System;
using Xunit;

namespace Demo.DDD.Domain.UnitTests
{
    public class PhoneNumberTests
    {
        [Theory]
        [InlineData(null, "12345678")]
        [InlineData(null, null)]
        [InlineData("DK", null)]
        public void PhoneNumber_WithNullInput_ShouldThrow(string countryCode, string phone)
        {
            Assert.Throws<ArgumentNullException>(() => new PhoneNumber(countryCode, phone));
        }

        [Theory]
        [InlineData("SE", "12345678")]
        [InlineData("DK", "123412df")]
        [InlineData("DK", "1234")]
        [InlineData("DK", "12ad")]
        public void PhoneNumber_WithInvalidInput_ShouldThrow(string countryCode, string phone)
        {
            Assert.Throws<ArgumentException>(() => new PhoneNumber(countryCode, phone));
        }

        [Fact]
        public void CanCreatePhoneNumber()
        {
            Assert.NotNull(new PhoneNumber("DK", "12345678"));
        }

        [Fact]
        public void PhoneNumber_ToString_ReturnsNumberWithCountryCode()
        {
            Assert.Equal("+4512345678", new PhoneNumber("DK", "12345678").ToString());
        }

        [Fact]
        public void PhoneNumber_WithSameArguments_AreEqual()
        {
            var phoneNumber = new PhoneNumber("DK", "12345678");
            var phoneNumber1 = new PhoneNumber("DK", "12345678");
            Assert.Equal(phoneNumber, phoneNumber1);
        }

        [Fact]
        public void PhoneNumber_WithDifferentArguments_AreNotEqual()
        {
            var phoneNumber = new PhoneNumber("DK", "12345671");
            var phoneNumber1 = new PhoneNumber("DK", "12345678");
            Assert.NotEqual(phoneNumber, phoneNumber1);
        }
    }
}
