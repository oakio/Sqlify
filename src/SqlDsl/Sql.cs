using SqlDsl.Core.Expressions;

namespace SqlDsl
{
    public static class Sql
    {
        public static SelectSqlQuery Select() => new SelectSqlQuery();

        public static SelectSqlQuery Select(params Expression[] columns) => new SelectSqlQuery().Select(columns);

        public static SelectSqlQuery<T> Select<T>(Expression<T> column) => new SelectSqlQuery<T>().Select(column);

        public static InsertSqlQuery Insert(Table table) => new InsertSqlQuery(table);

        public static UpdateSqlQuery Update(Table table) => new UpdateSqlQuery(table);

        public static DeleteSqlQuery Delete(Table table) => new DeleteSqlQuery(table);

        public static FunctionExpression<int> Count() => new FunctionExpression<int>("COUNT", ColumnExpression<int>.Asterisk);

        public static FunctionExpression<T> Max<T>(Expression<T> column) => Create("MAX", column);

        public static FunctionExpression<T> Min<T>(Expression<T> column) => Create("MIN", column);

        public static FunctionExpression<T> Avg<T>(Expression<T> column) => Create("AVG", column);

        public static FunctionExpression<T> Sum<T>(Expression<T> column) => Create("SUM", column);

        private static FunctionExpression<T> Create<T>(string name, Expression<T> arg) => new FunctionExpression<T>(name, arg);
    }
}