using System;
using System.Linq;
using System.Text;

namespace QueryGeneratorCore
{
    public class QueryGeneratorBuilder
    {
        public QueryGeneratorBuilder(QuerySchema querySchema)
        {
            QuerySchema = querySchema;
        }

        public QuerySchema QuerySchema { get; set; }

        public string Build()
        {
            if (QuerySchema == null) throw new Exception("No Selected Schema!");
            return QueryFrom(QuerySchema);
        }

        public string BuildCondition(QueryCondition condition)
        {
            if (condition.HasSubQuery()) return BuildConditiionWithSubQuery(condition);

            if (condition.HasBetweenOperator()) return BuildBetweenCondition(condition);

            return BuildConditionString(condition);
        }

        public string BuildConditiionWithSubQuery(QueryCondition condition) => $"{BuildLogicalOperatorAndColumn(condition)} {condition.Operator} ({QueryFrom(condition.Query)})";

        private string BuildBetweenCondition(QueryCondition condition) => $"{BuildLogicalOperatorAndColumn(condition)} {condition.Value} BETWEEN {condition.RightValue}";

        public string BuildConditionString(QueryCondition condition) => $"{BuildLogicalOperatorAndColumn(condition)} {condition.LeftOperator} {condition.Operator} {condition.StringValue()}";

        private string BuildLogicalOperatorAndColumn(QueryCondition condition) => $"{condition.LogicalOperator} {condition.Column}";

        public string BuildJoin(QueryJoin join) => $"{join.Clause} JOIN {join.Table} On {join.RelatedTable}.{join.RelatedColumn} = {join.Table}.{join.Column}";

        public string QueryFrom(QuerySchema query)
        {
            string fields = query.Fields?.Count > 0 ? string.Join(", ", query.Fields.Select(field => $"{query.Table}.{field}")) : "*";
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"SELECT {fields}");
            builder.AppendLine($"FROM {query.Table} ");
            if (query.QueryJoins?.Count > 0)
            {
                var joinsQuery = query.QueryJoins.Select(join => BuildJoin(join));
                builder.AppendLine(string.Join("\n", joinsQuery));
            }
            if (query.Conditions?.Count > 0)
            {
                builder.Append("WHERE ");
                var conditionsQuery = query.Conditions.Select(condition => BuildCondition(condition));
                builder.AppendLine(string.Join("\n", conditionsQuery));
            }
            return builder.ToString();
        }
    }
}
