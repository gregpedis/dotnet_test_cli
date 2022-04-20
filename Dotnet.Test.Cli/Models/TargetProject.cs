using Dotnet.Test.Cli.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Test.Cli.Models
{
    public class TargetProject
    {
        public string FilePath { get; init; }
        public Dictionary<string, TargetClass> TargetClasses { get; init; }

        public TargetProject(string filePath)
        {
            var tests = TestHelper.EnumerateTests(filePath);
            var kvPairs = GroupTestsByClass(tests);

            FilePath = filePath;
            TargetClasses = kvPairs.ToDictionary(kv => kv.Key, kv => new TargetClass(kv.Key, kv.Value));
        }

        private static Dictionary<string, HashSet<string>> GroupTestsByClass(IEnumerable<string> tests)
        {
            var testsByClass = new Dictionary<string, HashSet<string>>();

            foreach (var test in tests)
            {
                var splitted = test.Split('.');
                var testname = splitted[^1];
                var className = splitted[^2];

                if (testsByClass.ContainsKey(className))
                {
                    testsByClass[className].Add(testname);
                }
                else
                {
                    testsByClass.Add(className, new HashSet<string> { testname });
                }
            }

            return testsByClass;
        }
    }
}

