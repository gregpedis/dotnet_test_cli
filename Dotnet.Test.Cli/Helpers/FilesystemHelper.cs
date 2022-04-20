using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dotnet.Test.Cli.Base;
using Dotnet.Test.Cli.Models;
using Sharprompt;

namespace Dotnet.Test.Cli.Helpers
{
    public static class FilesystemHelper
    {
        public static void InitializeCurrentWorkingDirectory()
        {
            var accept_working_dir = false;

            while (!accept_working_dir)
            {
                Console.WriteLine($"Using {Definitions.WORKING_DIR}.");
                accept_working_dir = Prompt.Confirm("Accept", defaultValue: true);

                if (!accept_working_dir)
                {
                    var working_dir = Prompt.Input<string>("Specify directory to be used for testing");
                    Definitions.WORKING_DIR = working_dir;
                }
            }
        }

        public static TargetContext GetCurrentWorkingDirectoryContext()
        {
            Directory.SetCurrentDirectory(Definitions.WORKING_DIR);
            var here = Directory.GetCurrentDirectory();

            Console.WriteLine($"Working Directory: {here}.");
            Console.WriteLine("Processing files...");

            var files = Directory.GetFiles(here);

            var solutions = GetSolutions(files);
            var projects = GetProjects(files);

            var context = new TargetContext();

            if (solutions.Any())
            {
                foreach (var sln in solutions)
                {
                    var solution = new TargetSolution(sln);
                    context.Solutions.Add(sln, solution);
                }

            }
            else if (projects.Any())
            {
                foreach (var csproj in projects)
                {
                    var project = new TargetProject(csproj);
                    context.Projects.Add(csproj, project);
                }
            }

            context.RemoveGarbage();
            context.PrettyPrint();


            if (!context.IsValid)
            {
                ConsoleHelper.WriteError("Cannot initialize testbed on current working directory.");
                ConsoleHelper.WriteError("No .csproj or .sln files found.");
                ConsoleHelper.WriteError("Exiting...");
                Environment.Exit(-1);
            }

            ConsoleHelper.WriteSuccess("Current Working Directory is valid.");
            return context;
        }

        private static IEnumerable<string> GetSolutions(IEnumerable<string> files)
        {
            return MatchFiles(files, Definitions.SLN_EXTENSION);
        }

        private static IEnumerable<string> GetProjects(IEnumerable<string> files)
        {
            return MatchFiles(files, Definitions.CSPROJ_EXTENSION);
        }

        private static IEnumerable<string> MatchFiles(IEnumerable<string> files, string pattern)
        {
            return files.Where(f => Regex.IsMatch(f.ToUpperInvariant(), pattern));
        }

    }
}

