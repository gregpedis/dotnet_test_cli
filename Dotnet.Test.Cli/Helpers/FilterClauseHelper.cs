using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Test.Cli.Helpers
{
    public static class FilterClauseHelper
    {
        public static string BuildFilterClause(
                    Dictionary<string, IEnumerable<string>> testsByClass)
        {
            //dotnet test --filter "(FullyQualifiedName~UnitTest1&TestCategory=CategoryA)|Priority=1"
            // Expression is in the format <Property><Operator><Value>[|&<Expression>].
            var propertySelector = "FullyQualifiedName";
            var comparisonOperator = "~";
            var orOperator = '|';
            var endOperator = '&';
            var openBracket = "(";
            var closeBracket = ")";

            var partialFilterClauses = new List<string>();

            foreach (var (className, tests) in testsByClass)
            {
                var classExpression = propertySelector + comparisonOperator + className;

                var testsExpression = string.Join(
                    orOperator,
                    tests.Select(t => propertySelector + comparisonOperator + t)
                    );

                var partialFilterClause = classExpression + endOperator + openBracket + testsExpression + closeBracket;
                partialFilterClauses.Add(openBracket + partialFilterClause + closeBracket);
            }

            var filterClause = string.Join(orOperator, partialFilterClauses);
            return filterClause;
        }
    }
}
