using Sqlify.Core;
using Sqlify.Core.Expressions;

namespace Sqlify.Postgres
{
    public static class PgSql
    {
        public static PgSelectQuery Select() => new PgSelectQuery();

        public static PgSelectQuery Select(params Expression[] columns) => new PgSelectQuery().Select(columns);

        public static PgSelectQuery<T> Select<T>(Expression<T> column) => new PgSelectQuery<T>().Select(column);

        public static PgInsertQuery Insert(ITable table) => new PgInsertQuery(table);

        public static PgUpdateQuery Update(ITable table) => new PgUpdateQuery(table);

        public static PgDeleteQuery Delete(ITable table) => new PgDeleteQuery(table);
    }
}