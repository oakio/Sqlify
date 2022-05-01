namespace Sqlify.Core.Predicates
{
    public abstract class Predicate : ISqlFormattable
    {
        public Predicate And(Predicate expression) => new AndPredicate(this, expression);

        public Predicate Or(Predicate expression) => new OrPredicate(this, expression);

        public abstract void Format(ISqlWriter sql);
    }
}