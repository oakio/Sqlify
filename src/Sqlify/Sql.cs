using Sqlify.Core;
using Sqlify.Core.CodeGen;
using Sqlify.Core.Expressions;
using Sqlify.Core.Predicates;

namespace Sqlify
{
    public static class Sql
    {
        public static TTable Table<TTable>(string alias = null) where TTable : ITable =>  TableFactory<TTable>.Create(alias);

        public static SelectQuery Select() => new SelectQuery();

        public static SelectQuery Select(params Expression[] columns) => new SelectQuery().Select(columns);

        public static SelectQuery<T> Select<T>(Expression<T> column) => new SelectQuery<T>().Select(column);

        public static InsertQuery Insert(ITable table) => new InsertQuery(table);

        public static UpdateQuery Update(ITable table) => new UpdateQuery(table);

        public static DeleteQuery Delete(ITable table) => new DeleteQuery(table);

        public static MultipleQuery Multiple(params IQuery[] queries) => new MultipleQuery(queries);

        public static Predicate Or(Predicate left, Predicate right) => new OrPredicate(left, right);

        public static Predicate And(Predicate left, Predicate right) => new AndPredicate(left, right);

        public static FunctionExpression<int> Count() => new FunctionExpression<int>("COUNT", AsteriskExpression.Instance);

        public static FunctionExpression<T> Max<T>(Expression<T> column) => Create("MAX", column);

        public static FunctionExpression<T> Min<T>(Expression<T> column) => Create("MIN", column);

        public static FunctionExpression<T> Avg<T>(Expression<T> column) => Create("AVG", column);

        public static FunctionExpression<T> Sum<T>(Expression<T> column) => Create("SUM", column);

        public static FunctionExpression<T> Coalesce<T>(params Expression<T>[] columns) => Create("COALESCE", columns);

        public static FunctionExpression<T> NullIf<T>(Expression<T> left, Expression<T> right) => Create("NULLIF", new[]
        {
            left,
            right
        });

        private static FunctionExpression<T> Create<T>(string name, Expression<T> arg) => new FunctionExpression<T>(name, arg);

        private static FunctionExpression<T> Create<T>(string name, Expression<T>[] args) => new FunctionExpression<T>(name, args);
    }
}