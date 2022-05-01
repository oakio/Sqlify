namespace Sqlify.Core.Predicates
{
    public class OrPredicate : Predicate
    {
        private readonly Predicate _left;
        private readonly Predicate _right;

        public OrPredicate(Predicate left, Predicate right)
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