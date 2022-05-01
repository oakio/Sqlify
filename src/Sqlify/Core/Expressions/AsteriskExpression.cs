namespace Sqlify.Core.Expressions
{
    public class AsteriskExpression : Expression
    {
        public static readonly AsteriskExpression Instance = new AsteriskExpression();

        public override void Format(ISqlWriter sql) => sql.Append("*");
    }
}