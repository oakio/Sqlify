using System.Collections.Generic;

namespace Sqlify.Core
{
    public sealed class MultipleQuery : IQuery
    {
        private readonly List<ISelectQuery> _queries;

        public MultipleQuery(params ISelectQuery[] queries)
        {
            _queries = new List<ISelectQuery>(queries);
        }

        public void Add(ISelectQuery query) => _queries.Add(query);

        public void Format(ISqlWriter sql) => sql.Append("; ", _queries);
    }
}