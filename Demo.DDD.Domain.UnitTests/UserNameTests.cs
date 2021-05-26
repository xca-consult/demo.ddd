using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Demo.DDD.Domain;

namespace Demo.DDD.Domain.UnitTests
{
    public class UserNameTests
    {

        [Fact]
        public void UserName_WithValidInput_Succeeds()
        {
            new UserName("foo");
        }

        [Fact]
        public void UserName_WithOneCharacterName_Throws()
        {
            Assert.Throws<ArgumentException>(() => new UserName("a"));
        }

        [Fact]
        public void UserName_WithValidName_ImplicitlyConvertsToString()
        {
            var name = "foo";
            var username = new UserName(name);
            Assert.Equal(name, username);
        }

        [Theory]
        [InlineData("østergaard")]
        [InlineData("æstergaard")]
        [InlineData("åstergaard")]
        public void UserName_WithDanishName_ShouldHaveDanishName(string name)
        {
            var username = new UserName(name);
            Assert.True(username.IsDanishName);
        }

        [Theory]
        [InlineData("ostergaard")]
        public void UserName_WithNonDanishName_ShouldNotHaveDanishName(string name)
        {
            var username = new UserName(name);
            Assert.False(username.IsDanishName);
        }
    }
}
