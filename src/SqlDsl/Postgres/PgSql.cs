using SqlDsl.Core.Expressions;

namespace SqlDsl.Postgres
{
    public static class PgSql
    {
        public static PgSelectSqlQuery Select() => new PgSelectSqlQuery();

        public static PgSelectSqlQuery Select(params Expression[] columns) => new PgSelectSqlQuery().Select(columns);

        public static PgSelectSqlQuery<T> Select<T>(Expression<T> column) => new PgSelectSqlQuery<T>().Select(column);
    }
}