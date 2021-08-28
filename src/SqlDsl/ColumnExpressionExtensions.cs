using SqlDsl.Core.Expressions;
using SqlDsl.Core.Predicates;

namespace SqlDsl
{
    public static class ColumnExpressionExtensions
    {
        public static PredicateExpression Like(this ColumnExpression<string> column, string pattern) => new LikeExpression(column, pattern);
    }
}