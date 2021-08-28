namespace SqlDsl.Core.Expressions
{
    public abstract class BinaryExpression<T> : Expression<T>
    {
        private readonly Expression<T> _left;
        private readonly Expression<T> _right;

        protected BinaryExpression(Expression<T> left, Expression<T> right)
        {
            _left = left;
            _right = right;
        }

        protected void Format(ISqlWriter sql, string type)
        {
            _left.Format(sql);
            sql.Append(type);
            _right.Format(sql);
        }
    }
}