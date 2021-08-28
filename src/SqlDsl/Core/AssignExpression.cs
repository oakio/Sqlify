using SqlDsl.Core.Expressions;

namespace SqlDsl.Core
{
    public readonly struct AssignExpression : ISqlFormattable
    {
        private readonly Expression _left;
        private readonly Expression _right;

        public AssignExpression(Expression left, Expression right)
        {
            _left = left;
            _right = right;
        }

        public void Format(ISqlWriter sql)
        {
            _left.Format(sql);
            sql.Append(" = ");
            _right.Format(sql);
        }
    }
}