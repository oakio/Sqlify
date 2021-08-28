namespace SqlDsl.Core.Expressions
{
    public sealed class DivExpression<T> : BinaryExpression<T>
    {
        public DivExpression(Expression<T> left, Expression<T> right) : base(left, right)
        {
        }

        public override void Format(ISqlWriter sql) => Format(sql, " / ");
    }
}