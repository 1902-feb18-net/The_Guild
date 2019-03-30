using System;
using The_Guild.WebApp.ViewModel;
using Xunit;

namespace The_Guild.Test.ViewModelTest
{
    public class UserViewModelTest
    {
        public readonly UserViewModel user = new UserViewModel();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        public void Test_FirstName(string arg)
        {
            Assert.Throws<ArgumentException>(() => user.FirstName = arg);
        }

        [Fact]
        public void Test_FirstNameNull()
        {
            Assert.Throws<ArgumentNullException>(() => user.FirstName = null);
        }


        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        public void Test_LastName(string arg)
        {
            Assert.Throws<ArgumentException>(() => user.LastName = arg);
        }

        [Fact]
        public void Test_LastNameNull()
        {
            Assert.Throws<ArgumentNullException>(() => user.LastName = null);
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(41)]
        public void Test_Strength(int arg)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => user.Strength = arg);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(41)]
        public void Test_Constitution(int arg)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => user.Constitution = arg);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(41)]
        public void Test_Charisma(int arg)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => user.Charisma = arg);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(41)]
        public void Test_Dex(int arg)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => user.Dex = arg);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(41)]
        public void Test_Intelligence(int arg)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => user.Intelligence = arg);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(41)]
        public void Test_Wisdom(int arg)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => user.Wisdom = arg);
        }

        [Theory]
        [InlineData(-1)]
        public void Test_Salary(decimal arg)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => user.Salary = arg);
        }
    }
}
