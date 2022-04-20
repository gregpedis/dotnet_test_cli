using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Test.Cli.Base
{
    public class Definitions
    {
        public const string SLN_EXTENSION = ".SLN";
        public const string CSPROJ_EXTENSION = ".CSPROJ";
        //public static readonly List<string> VALID_EXTENSIONS = new List<string> {  SLN_EXTENSION, CSPROJ_EXTENSION };
        //public const string FS_REGEX_PATTERN = @".+\.(csproj|sln)";

        public static readonly bool IS_LUNIX = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        //public static string WORKING_DIR = Directory.GetCurrentDirectory();
        public static string WORKING_DIR = IS_LUNIX ? "/mnt/e/repos/dotnet_test_cli/playground" : @"E:/repos/dotnet_test_cli/playground";

        public const ConsoleColor CC_INFO = ConsoleColor.DarkYellow;
        public const ConsoleColor CC_SUCCESS = ConsoleColor.Green;

        public const ConsoleColor CC_WARNING = ConsoleColor.Yellow;
        public const ConsoleColor CC_ERROR = ConsoleColor.Red;

        public static readonly string CMD_DOTNET = IS_LUNIX ? "dotnet" : "dotnet.exe";
        public const string ARGV_LIST_TESTS = "test --list-tests";
        public const string ARGV_EXECUTE_TEST_FILTER = "test --filter";

        public const string LIST_TESTS_OUTPUT_SPLITTER = "ARE AVAILABLE:";
        public const string RUN_TESTS_OUTPUT_SPLITTER = "A TOTAL OF";
    }
}

