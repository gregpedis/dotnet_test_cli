using Dotnet.Test.Cli.Helpers;
using System;
using Sharprompt;

namespace Dotnet.Test.Cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FilesystemHelper.InitializeCurrentWorkingDirectory();
            var context = FilesystemHelper.GetCurrentWorkingDirectoryContext();
            TestHelper.RunTests(context);
        }
    }
}

