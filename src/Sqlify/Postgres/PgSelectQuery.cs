using Sqlify.Core.Expressions;

namespace Sqlify.Postgres
{
    public class PgSelectQuery : PgSelectQueryBase<PgSelectQuery>
    {
        public PgSelectQuery Select() => this;

        public PgSelectQuery Select(Expression[] columns)
        {
            SelectColumns(columns);
            return this;
        }
    }

    public class PgSelectQuery<T> : PgSelectQueryBase<PgSelectQuery<T>>
    {
        public PgSelectQuery<T> Select(Expression<T> column)
        {
            SelectColumn(column);
            return this;
        }
    }
}