namespace Sqlify.Core.Expressions
{
    public sealed class AddExpression<T> : BinaryExpression<T>
    {
        public AddExpression(Expression<T> left, Expression<T> right) : base(left, right)
        {
        }

        public override void Format(ISqlWriter sql) => Format(sql, " + ");
    }
}