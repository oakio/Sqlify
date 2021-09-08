using SqlDsl.Core.Expressions;

namespace SqlDsl.Postgres
{
    public static class PgSql
    {
        public static PgSelectQuery Select() => new PgSelectQuery();

        public static PgSelectQuery Select(params Expression[] columns) => new PgSelectQuery().Select(columns);

        public static PgSelectQuery<T> Select<T>(Expression<T> column) => new PgSelectQuery<T>().Select(column);

        public static PgInsertQuery Insert(Table table) => new PgInsertQuery(table);

        public static PgUpdateQuery Update(Table table) => new PgUpdateQuery(table);

        public static PgDeleteQuery Delete(Table table) => new PgDeleteQuery(table);
    }
}