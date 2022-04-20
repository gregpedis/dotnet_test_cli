using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Test.Cli.Models
{
    public class TargetClass
    {
        public string Name { get; init; }
        public List<string> TargetTests { get; init; }

        public TargetClass(string name, IEnumerable<string> tests)
        {
            Name =  name;
            TargetTests = tests.ToList();
        }
    }
}

