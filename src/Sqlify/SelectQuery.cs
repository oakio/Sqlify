using Sqlify.Core;
using Sqlify.Core.Expressions;

namespace Sqlify
{
    public class SelectQuery : SelectQueryBase<SelectQuery>
    {
        public SelectQuery Select() => this;

        public SelectQuery Select(Expression[] columns)
        {
            SelectColumns(columns);
            return this;
        }
    }

    public class SelectQuery<T> : SelectQueryBase<SelectQuery<T>>
    {
        public SelectQuery<T> Select(Expression<T> column)
        {
            SelectColumn(column);
            return this;
        }
    }
}
