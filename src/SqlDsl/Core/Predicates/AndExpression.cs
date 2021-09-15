namespace SqlDsl.Core.Predicates
{
    public class AndExpression : PredicateExpression
    {
        private readonly PredicateExpression _left;
        private readonly PredicateExpression _right;

        public AndExpression(PredicateExpression left, PredicateExpression right)
        {
            _left = left;
            _right = right;
        }

        public override void Format(ISqlWriter sql)
        {
            if (_left is OrExpression)
            {
                if (_right is OrExpression)
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

            if (_right is OrExpression)
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