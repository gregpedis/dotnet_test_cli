using Dotnet.Test.Cli.Base;
using Dotnet.Test.Cli.Models;
using Sharprompt;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Test.Cli.Helpers
{
    public class TestHelper
    {
        private static string _lastFilterClause = string.Empty;
        //private static bool _keepGoing = true;
        //private static bool _alwaysKeepGoing = false;

        public static IEnumerable<string> EnumerateTests(string target)
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = Definitions.CMD_DOTNET,
                Arguments = Definitions.ARGV_LIST_TESTS + " " + target,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };

            var process = Process.Start(processInfo);

            var output = process!.StandardOutput.ReadToEnd();
            var err = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (!string.IsNullOrWhiteSpace(err))
            {
                ConsoleHelper.WriteError(err);
                Environment.Exit(-1);
            }
            if (output.Contains(" error "))
            {
                ConsoleHelper.WriteError("Build failed.");
                ConsoleHelper.WriteError("What the hell my dude?");
                var question = "How do you expect me to run tests on something that doesn't even build?";
                ConsoleHelper.WriteError(question);
                ConsoleHelper.WriteError(new string('-', question.Length));

                ConsoleHelper.WriteError(Environment.NewLine);
                ConsoleHelper.WriteError(output);
                Environment.Exit(-1);
            }

            var tests = ParseListTestOutput(output);

            return tests;
        }

        public static void RunTests(TargetContext context)
        {
            while (true)
            {

                if (context.Solutions.Count > 1)
                {
                    var sln = ConsoleHelper.PromptForSolutionKey(context.Solutions.Keys);
                    RunTests(context.Solutions[sln]);
                }
                else if (context.Solutions.Count == 1)
                {
                    RunTests(context.Solutions.First().Value);
                }
                else if (context.Projects.Count > 1)
                {
                    var csproj = ConsoleHelper.PromptForProjectKey(context.Projects.Keys);
                    RunTests(context.Projects[csproj]);
                }
                else
                {
                    RunTests(context.Projects.First().Value);
                }

                ConsoleHelper.WriteWarning(new string('>', 20));
            }
        }

        private static void RunTests(TargetSolution solution)
        {
            if (solution.Projects.Count > 1)
            {
                var csproj = ConsoleHelper.PromptForProjectKey(solution.Projects.Keys);
                RunTests(solution.Projects[csproj]);
            }
            else
            {
                RunTests(solution.Projects.First().Value);
            }
        }

        private static void RunTests(TargetProject project)
        {
            var testsByClass = new Dictionary<string, IEnumerable<string>>();

            if (project.TargetClasses.Count > 1)
            {
                var classes = ConsoleHelper.PromptForClassKeys(project.TargetClasses.Keys);
                foreach (var className in classes)
                {
                    var tests = ConsoleHelper.PromptForTestKeys(project.TargetClasses[className].TargetTests, className);
                    testsByClass.Add(className, tests);
                }
            }
            else
            {
                var cls = project.TargetClasses.First().Value;
                if (cls.TargetTests.Count > 1)
                {
                    var tests = ConsoleHelper.PromptForTestKeys(cls.TargetTests);
                    testsByClass.Add(cls.Name, tests);
                }
                else
                {
                    testsByClass.Add(cls.Name, cls.TargetTests);
                }
            }

            RunTests(project.FilePath, testsByClass);
        }

        private static void RunTests(
            string csproj,
            Dictionary<string, IEnumerable<string>> testsByClass
            )
        {
            _lastFilterClause = FilterClauseHelper.BuildFilterClause(testsByClass);

            var processInfo = new ProcessStartInfo
            {
                FileName = Definitions.CMD_DOTNET,
                Arguments = Definitions.ARGV_EXECUTE_TEST_FILTER + " " + _lastFilterClause + " " + csproj,
                UseShellExecute = true,
            };

            var process = Process.Start(processInfo);
            process?.WaitForExit();
        }

        private static IEnumerable<string> ParseListTestOutput(string output)
        {
            var testcases = KeepLinesAfter(output, Definitions.LIST_TESTS_OUTPUT_SPLITTER);

            if (!testcases.Any())
            {
                return new List<string>();
            }

            var validTestCases = testcases
                .Select(testcase => testcase.Trim())
                .Where(testcase => !string.IsNullOrWhiteSpace(testcase))
                .Select(testcase => testcase.Contains('(') ? testcase.Substring(0, testcase.IndexOf('(')) : testcase);

            return validTestCases;
        }

        private static IEnumerable<string> KeepLinesAfter(string output, string searchPattern)
        {
            var splitted = output.Split(Environment.NewLine).ToList();

            var splitPoint = splitted
                .FindLastIndex(line => line.ToUpperInvariant().Contains(searchPattern));

            if (splitPoint < 0 || splitPoint == splitted.Count - 1)
            {
                return new List<string>();
            }
            else
            {
                return splitted.Skip(splitPoint + 1);
            }
        }
    }
}

