using SqlDsl.Core.Expressions;
using SqlDsl.Core.Predicates;

namespace SqlDsl
{
    public static class ColumnExpressionExtensions
    {
        public static Predicate Like(this ColumnExpression<string> column, string pattern) => new LikePredicate(column, pattern);
    }
}