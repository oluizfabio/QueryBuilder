using System;
using System.Collections.Generic;

namespace QueryGeneratorCore
{
    public class QuerySchema
    {

        public QuerySchema()
        {
        }

        public string Table { get; set; }
        public ICollection<string> Fields { get; set; }
        public ICollection<QueryCondition> Conditions { get; set; }
        public ICollection<QueryJoin> QueryJoins { get; set; }
    }
}
