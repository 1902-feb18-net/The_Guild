using System;
using System.Collections.Generic;
using System.Text;
using The_Guild.WebApp.Models;
using Xunit;

namespace The_Guild.Test
{
    public class UserTest
    {
        public readonly Users user = new Users();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        public void Test_FirstName(string arg)
        {
            Assert.Throws<ArgumentException>(() => user.FirstName = arg);
        }
    }
}
