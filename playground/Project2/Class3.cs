using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project2
{
    public class Class3
    {
        [Fact]
        public void TestA()
        {
            System.Diagnostics.Debugger.Launch();
            Assert.True(true);
        }
    }
}
