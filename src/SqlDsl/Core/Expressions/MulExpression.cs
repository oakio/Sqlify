namespace SqlDsl.Core.Expressions
{
    public sealed class MulExpression<T> : BinaryExpression<T>
    {
        public MulExpression(Expression<T> left, Expression<T> right) : base(left, right)
        {
        }

        public override void Format(ISqlWriter sql) => Format(sql, " * ");
    }
}