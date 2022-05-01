namespace Sqlify.Core.Expressions
{
    public sealed class SubExpression<T> : BinaryExpression<T>
    {
        public SubExpression(Expression<T> left, Expression<T> right) : base(left, right)
        {
        }

        public override void Format(ISqlWriter sql) => Format(sql, " - ");
    }
}