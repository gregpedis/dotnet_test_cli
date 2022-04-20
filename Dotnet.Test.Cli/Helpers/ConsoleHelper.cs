using Dotnet.Test.Cli.Base;
using Sharprompt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Test.Cli.Helpers
{
    public static class ConsoleHelper 
    {
        public static void WriteInfo(string message) => Write(message, Definitions.CC_INFO);
        public static void WriteWarning(string message) => Write(message, Definitions.CC_WARNING);
        public static void WriteError(string message) => Write(message, Definitions.CC_ERROR);
        public static void WriteSuccess(string message) => Write(message, Definitions.CC_SUCCESS);

        private static void Write(string message, ConsoleColor color)
        {
            var before = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = before;
        }

        public static string PromptForSolutionKey(IEnumerable<string> keys)
        {
            var sln = Prompt.Select("Which solutions would you like to run?", keys);
            return sln;
        }

        public static string PromptForProjectKey(IEnumerable<string> keys)
        {
            var csproj = Prompt.Select("Which project would you like to run?", keys);
            return csproj;
        }

        public static IEnumerable<string> PromptForClassKeys(IEnumerable<string> keys)
        {
            var classes = Prompt.MultiSelect("Which classes would you like to run?", keys);
            return classes;
        }

        public static IEnumerable<string> PromptForTestKeys(IEnumerable<string> keys, string className = "")
        {
            var specifier = string.IsNullOrWhiteSpace(className)
                ? string.Empty :
                $" ({className})";

            var tests = Prompt.MultiSelect("Which tests would you like to run?" + specifier, keys);
            return tests;
        }
    }
}

