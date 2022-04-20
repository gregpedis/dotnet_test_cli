using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Test.Cli.Models
{
    public class TargetSolution
    {
        public string FilePath { get; init; } 
        public Dictionary<string,TargetProject> Projects { get; init; }


        public TargetSolution(string path)
        {
            FilePath = path;

            Projects = File.ReadAllLines(path)
                .Where(l => l.StartsWith("Project"))
                .Select(l => l.Split(",")[1].Replace('\\', Path.DirectorySeparatorChar))
                .Select(proj => proj.Trim().Replace("\"", string.Empty))
                .ToDictionary(proj => proj, proj => new TargetProject(proj));
        }
    }
}

