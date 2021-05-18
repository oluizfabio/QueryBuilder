using System;
namespace QueryGeneratorCore
{
    public class QueryJoin
    {
        public QueryJoin()
        {
        }

        public string Table { get; set; }
        public string Column { get; set; }
        public string RelatedTable { get; set; }
        public string RelatedColumn { get; set; }
        public string Clause { get; set; }
    }
}
