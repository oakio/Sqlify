using System.Collections.Generic;

namespace Sqlify.Core
{
    public sealed class MultipleQuery : IQuery
    {
        private readonly List<IQuery> _queries;

        public MultipleQuery(params IQuery[] queries)
        {
            _queries = new List<IQuery>(queries);
        }

        public void Add(IQuery query) => _queries.Add(query);

        public void Format(ISqlWriter sql) => sql.Append("; ", _queries);
    }
}