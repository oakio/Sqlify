namespace SqlDsl.Core.Expressions
{
    public class AliasExpression<T> : Expression<T>
    {
        private readonly Expression<T> _column;
        private readonly string _alias;

        public AliasExpression(Expression<T> column, string alias)
        {
            _column = column;
            _alias = alias;
        }

        public override void Format(ISqlWriter sql)
        {
            _column.Format(sql);
            sql.Append(" AS ");
            sql.Append(_alias);
        }
    }
}