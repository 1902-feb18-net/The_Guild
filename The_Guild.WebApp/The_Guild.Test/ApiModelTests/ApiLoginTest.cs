using System;
using Xunit;
using The_Guild.WebApp.ApiModels;

namespace The_Guild.Test
{
    public class ApiLoginTest
    {
        public readonly ApiLogin _login = new ApiLogin();


        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        public void Test_Username(string arg)
        {
            Assert.Throws<ArgumentException>(() => _login.Username = arg);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        public void Test_Password(string arg)
        {
            Assert.Throws<ArgumentException>(() => _login.Password = arg);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Test_RememberMe(bool arg)
        {
            _login.RememberMe = arg;
            Assert.Equal(arg, _login.RememberMe);
        }
    }
}
