namespace SqlDsl.Core.Expressions
{
    public sealed class ParamExpression<T> : Expression<T>
    {
        private readonly T _value;

        public ParamExpression(T value)
        {
            _value = value;
        }

        public override void Format(ISqlWriter sql)
        {
            var name = sql.AddParam(_value);
            sql.Append(name);
        }
    }
}