using SqlDsl.Core.Expressions;

namespace SqlDsl.Postgres
{
    public class PgSelectSqlQuery : PgSelectSqlQueryBase<PgSelectSqlQuery>
    {
        public PgSelectSqlQuery Select() => this;

        public PgSelectSqlQuery Select(Expression[] columns)
        {
            SelectColumns(columns);
            return this;
        }
    }

    public class PgSelectSqlQuery<T> : PgSelectSqlQueryBase<PgSelectSqlQuery<T>>
    {
        public PgSelectSqlQuery<T> Select(Expression<T> column)
        {
            SelectColumn(column);
            return this;
        }
    }
}