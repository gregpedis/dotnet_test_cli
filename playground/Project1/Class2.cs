using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project1
{
    public class Class2
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TestC(bool condition)
        {
            Assert.True(condition);
        }

        [Fact]
        public void TestD()
        {
            Assert.True(1 == 2);
        }
    }
}
