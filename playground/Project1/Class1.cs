using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project1
{
    [Collection("Some Collection")]
    [Trait("Category", "Some Category")]
    public class Class1
    {
        [Fact]
        public void TestA()
        {
            Assert.True(true);
        }

        [Fact]
        public void TestB()
        {
            Assert.True(false);
        }
    }
}
