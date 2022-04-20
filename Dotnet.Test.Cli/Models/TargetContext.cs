using Dotnet.Test.Cli.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Test.Cli.Models
{
    public class TargetContext
    {
        public Dictionary<string, TargetSolution> Solutions { get; set; } = new Dictionary<string, TargetSolution>();
        public Dictionary<string, TargetProject> Projects { get; set; } = new Dictionary<string, TargetProject>();

        public void RemoveGarbage()
        {
            foreach (var (k, v) in Solutions)
            {
                if (!v.Projects.Any())
                {
                    Solutions.Remove(k);
                }
            }

            foreach (var (k, v) in Projects)
            {
                if (!v.TargetClasses.Any())
                {
                    Projects.Remove(k);
                }
            }
        }

        public void PrettyPrint()
        {
            if (Solutions.Any())
            {
                ConsoleHelper.WriteSuccess("Found the following solutions:");

                foreach (var sln in Solutions)
                {
                    ConsoleHelper.WriteInfo($"-{sln.Key}");
                    foreach (var csproj in sln.Value.Projects)
                    {
                        ConsoleHelper.WriteInfo($"  -{csproj.Key}");
                    }
                }
            }
            else if (Projects.Any())
            {
                ConsoleHelper.WriteSuccess("Found the following projects:");
                foreach (var csproj in Projects)
                {
                    ConsoleHelper.WriteInfo($"-{csproj.Key}");
                }
            }
        }

        public bool IsValid => Projects.Any() || (Solutions.Any() && Solutions.Values.Any(s => s.Projects.Any()));
    }
}


