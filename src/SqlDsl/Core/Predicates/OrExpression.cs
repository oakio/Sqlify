namespace SqlDsl.Core.Predicates
{
    public class OrExpression : PredicateExpression
    {
        private readonly PredicateExpression _left;
        private readonly PredicateExpression _right;

        public OrExpression(PredicateExpression left, PredicateExpression right)
        {
            _left = left;
            _right = right;
        }

        public override void Format(ISqlWriter sql)
        {
            _left.Format(sql);
            sql.Append(" OR ");
            _right.Format(sql);
        }
    }
}