namespace SqlDsl.Core.Predicates
{
    public class AndPredicate : Predicate
    {
        private readonly Predicate _left;
        private readonly Predicate _right;

        public AndPredicate(Predicate left, Predicate right)
        {
            _left = left;
            _right = right;
        }

        public override void Format(ISqlWriter sql)
        {
            if (_left is OrPredicate)
            {
                if (_right is OrPredicate)
                {
                    sql.Append("(");
                    _left.Format(sql);
                    sql.Append(") AND (");
                    _right.Format(sql);
                    sql.Append(")");
                    return;
                }

                sql.Append("(");
                _left.Format(sql);
                sql.Append(") AND ");
                _right.Format(sql);
                return;
            }

            if (_right is OrPredicate)
            {
                _left.Format(sql);
                sql.Append(" AND (");
                _right.Format(sql);
                sql.Append(")");
                return;
            }

            _left.Format(sql);
            sql.Append(" AND ");
            _right.Format(sql);
        }
    }
}