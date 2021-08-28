namespace SqlDsl.Core.Predicates
{
    public abstract class PredicateExpression : ISqlFormattable
    {
        public PredicateExpression And(PredicateExpression expression) => new AndExpression(this, expression);

        public PredicateExpression Or(PredicateExpression expression) => new OrExpression(this, expression);

        public abstract void Format(ISqlWriter sql);
    }
}