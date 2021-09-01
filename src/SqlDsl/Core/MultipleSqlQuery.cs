using System.Collections.Generic;

namespace SqlDsl.Core
{
    public sealed class MultipleSqlQuery : ISqlQuery
    {
        private readonly List<ISelectSqlQuery> _queries;

        public MultipleSqlQuery(params ISelectSqlQuery[] queries)
        {
            _queries = new List<ISelectSqlQuery>(queries);
        }

        public void Add(ISelectSqlQuery query) => _queries.Add(query);

        public void Format(ISqlWriter sql) => sql.Append(string.Empty, "; ", _queries);
    }
}