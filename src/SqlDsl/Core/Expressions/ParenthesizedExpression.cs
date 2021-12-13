namespace SqlDsl.Core.Expressions
{
    public sealed class ParenthesizedExpression<T> : Expression<T>
    {
        private readonly Expression<T> _inner;

        public ParenthesizedExpression(Expression<T> inner)
        {
            _inner = inner;
        }

        public override void Format(ISqlWriter sql)
        {
            sql.Append("(");
            _inner.Format(sql);
            sql.Append(")");
        }
    }
}