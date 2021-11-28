namespace SqlDsl.Core.Expressions
{
    public class CastExpression<T> : Expression<T>
    {
        private readonly Expression _expression;
        private readonly string _dataType;

        public CastExpression(Expression expression, string dataType)
        {
            _expression = expression;
            _dataType = dataType;
        }

        public override void Format(ISqlWriter sql)
        {
            sql.Append("CAST(");
            _expression.Format(sql);
            sql.Append(" AS ");
            sql.Append(_dataType);
            sql.Append(")");
        }
    }
}