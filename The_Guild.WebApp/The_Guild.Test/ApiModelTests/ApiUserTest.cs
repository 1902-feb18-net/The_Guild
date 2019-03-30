using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using The_Guild.WebApp.ApiModels;

namespace The_Guild.Test.ApiModelTests
{
    public class ApiUserTest
    {
        private readonly ApiUsers user = new ApiUsers();
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        public void Test_FirstName(string arg)
        {
            Assert.Throws<ArgumentException>(() => user.FirstName = arg);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        public void Test_LastName(string arg)
        {
            Assert.Throws<ArgumentException>(() => user.FirstName = arg);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1000000)]
        public void Test_Salary(int arg)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => user.Salary = arg);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(100000)]
        public void Test_Strength(int arg)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => user.Strength = arg);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(100000)]
        public void Test_Wisdom(int arg)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => user.Wisdom = arg);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(100000)]
        public void Test_Intelligence(int arg)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => user.Intelligence = arg);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(100000)]
        public void Test_Dexterity(int arg)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => user.Dex = arg);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(100000)]
        public void Test_Constitution(int arg)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => user.Constitution = arg);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(100000)]
        public void Test_Charisma(int arg)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => user.Charisma = arg);
        }
    }
}
