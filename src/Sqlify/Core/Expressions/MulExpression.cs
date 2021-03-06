namespace Sqlify.Core.Expressions
{
    public sealed class MulExpression<T> : BinaryExpression<T>
    {
        public MulExpression(Expression<T> left, Expression<T> right)
            : base(Parentheses(left), Parentheses(right))
        {
        }

        private static Expression<T> Parentheses(Expression<T> expression)
        {
            if (expression is AddExpression<T> ||
                expression is SubExpression<T>)
            {
                return new ParenthesizedExpression<T>(expression);
            }

            return expression;
        }

        public override void Format(ISqlWriter sql) => Format(sql, " * ");
    }
}