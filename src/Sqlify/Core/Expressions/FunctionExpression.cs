namespace Sqlify.Core.Expressions
{
    public class FunctionExpression<T> : Expression<T>
    {
        private readonly string _name;
        private readonly Expression _arg;
        private readonly Expression[] _args;

        public FunctionExpression(string name, Expression arg)
        {
            _name = name;
            _arg = arg;
        }

        public FunctionExpression(string name, Expression[] args)
        {
            _name = name;
            _args = args;
        }

        public override void Format(ISqlWriter sql)
        {
            sql.Append(_name);

            if (_arg != null)
            {
                sql.Append("(");
                _arg.Format(sql);
                sql.Append(")");
            }
            else
            {
                sql.Append("(", ", ", _args, ")");
            }
        }
    }
}