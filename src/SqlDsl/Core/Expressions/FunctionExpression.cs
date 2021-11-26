namespace SqlDsl.Core.Expressions
{
    public class FunctionExpression<T> : Expression<T>
    {
        private readonly string _name;
        private readonly Expression _arg;

        public FunctionExpression(string name, Expression arg)
        {
            _name = name;
            _arg = arg;
        }

        public override void Format(ISqlWriter sql)
        {
            sql.Append(_name);
            sql.Append("(");
            _arg.Format(sql);
            sql.Append(")");
        }
    }
}