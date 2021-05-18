using System;
namespace QueryGeneratorCore
{
    public class QueryCondition
    {
        public QueryCondition()
        {
        }

        public string Operator { get; set; }
        public string LeftOperator { get; set; } = "";
        public string Column { get; set; }
        public string Value { get; set; }
        public string LogicalOperator { get; set; } // AND NOT OR
        public string RightValue { get; set; }
        public QuerySchema Query { get; set; }

        public bool HasSubQuery()
        {
            return Query != null;
        }

        public bool HasSideOperator() => !string.IsNullOrEmpty(LeftOperator);

        public bool HasInOperator() => "IN".Equals(Operator.ToUpper());

        public bool HasBetweenOperator() => "BETWEEN".Equals(Operator.ToUpper());

        public string CombineOperators() => $"{LeftOperator}{Operator}";

        public string StringValue() => int.TryParse(Value, out int value) ? $"{value}" : $"'{Value}'";
    }
}
