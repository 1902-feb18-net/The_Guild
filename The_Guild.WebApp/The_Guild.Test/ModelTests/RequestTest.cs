using System;
using The_Guild.WebApp.Models;
using Xunit;

namespace The_Guild.Test.ModelTests
{
    public class RequestTest
    {
        public readonly Request request = new Request();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        public void Test_Description(string arg)
        {
            Assert.Throws<ArgumentException>(() => request.Descript = arg);
        }

        [Fact]
        public void Test_DescriptionNull()
        {
            Assert.Throws<ArgumentNullException>(() => request.Descript = null);
        }


        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        public void Test_Requirements(string arg)
        {
            Assert.Throws<ArgumentException>(() => request.Requirements = arg);
        }

        [Fact]
        public void Test_RequirementsNull()
        {
            Assert.Throws<ArgumentNullException>(() => request.Requirements = null);
        }

        [Theory]
        [InlineData(-100)]
        [InlineData(-1)]
        public void Test_Cost(decimal arg)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => request.Cost = arg);
        }

        [Theory]
        [InlineData(-100)]
        [InlineData(-1)]
        public void Test_Reward(decimal arg)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => request.Reward = arg);
        }
    }
}
