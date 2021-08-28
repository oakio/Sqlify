using SqlDsl.Core.Expressions;

namespace SqlDsl.Core.Clauses
{
    public readonly struct OrderByClause : ISqlFormattable
    {
        private readonly Expression _expression;

        private readonly bool _desc;

        public OrderByClause(Expression expression, bool desc)
        {
            _expression = expression;
            _desc = desc;
        }

        public void Format(ISqlWriter sql)
        {
            _expression.Format(sql);

            if (_desc)
            {
                sql.Append(" DESC");
            }
        }
    }
}