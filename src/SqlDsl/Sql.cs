using SqlDsl.Core;
using SqlDsl.Core.Expressions;

namespace SqlDsl
{
    public static class Sql
    {
        public static SelectQuery Select() => new SelectQuery();

        public static SelectQuery Select(params Expression[] columns) => new SelectQuery().Select(columns);

        public static SelectQuery<T> Select<T>(Expression<T> column) => new SelectQuery<T>().Select(column);

        public static InsertQuery Insert(Table table) => new InsertQuery(table);

        public static UpdateQuery Update(Table table) => new UpdateQuery(table);

        public static DeleteQuery Delete(Table table) => new DeleteQuery(table);

        public static MultipleQuery Multiple(params ISelectQuery[] queries) => new MultipleQuery(queries);

        public static FunctionExpression<int> Count() => new FunctionExpression<int>("COUNT", ColumnExpression<int>.Asterisk);

        public static FunctionExpression<T> Max<T>(Expression<T> column) => Create("MAX", column);

        public static FunctionExpression<T> Min<T>(Expression<T> column) => Create("MIN", column);

        public static FunctionExpression<T> Avg<T>(Expression<T> column) => Create("AVG", column);

        public static FunctionExpression<T> Sum<T>(Expression<T> column) => Create("SUM", column);

        private static FunctionExpression<T> Create<T>(string name, Expression<T> arg) => new FunctionExpression<T>(name, arg);
    }
}