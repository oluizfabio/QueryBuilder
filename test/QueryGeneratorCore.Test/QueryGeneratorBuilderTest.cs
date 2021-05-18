using System;
using Moq;
using Xunit;

namespace QueryGeneratorCore.Test
{
    public class QueryGeneratorBuilderTest
    {
        private readonly QueryGeneratorBuilder mockGeneratorBuild;
        public QueryGeneratorBuilderTest()
        {
            mockGeneratorBuild = new QueryGeneratorBuilder(null);
        }

        [Theory]
        [InlineData("Name", "=", "Peter")]
        public void SimpleCondition(string column, string oprt, string value)
        {
            QueryCondition condition = new QueryCondition() { Column = column, Operator = oprt, Value = value };
            string conditionBuilt = mockGeneratorBuild.BuildConditionString(condition);
            Assert.Equal($" {column}  {oprt} '{value}'", conditionBuilt);
        }

        [Theory]
        [InlineData("Name", "<", "Peter", ">")]
        public void ConditinWithLeftOperator(string column, string oprt, string value, string leftOperator)
        {
            QueryCondition condition = new QueryCondition() { Column = column, Operator = oprt, Value = value, LeftOperator = leftOperator };
            string conditionBuilt = mockGeneratorBuild.BuildConditionString(condition);
            Assert.Equal($" {column} {leftOperator} {oprt} '{value}'", conditionBuilt);
        }
    }
}
