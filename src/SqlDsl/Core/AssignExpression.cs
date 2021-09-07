using SqlDsl.Core.Expressions;

namespace SqlDsl.Core
{
    public readonly struct AssignExpression : ISqlFormattable
    {
        private readonly string _column;
        private readonly Expression _value;

        public AssignExpression(string column, Expression value)
        {
            _column = column;
            _value = value;
        }

        public void Format(ISqlWriter sql)
        {
            sql.Append(_column);
            sql.Append(" = ");
            _value.Format(sql);
        }
    }
}