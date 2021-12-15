using System.Collections.Generic;
using SqlDsl.Core;
using SqlDsl.Core.Expressions;
using SqlDsl.Core.Predicates;

namespace SqlDsl.Postgres.InsertConflict
{
    public class PgUpdateConflictAction : IPgConflictAction
    {
        private readonly List<AssignExpression> _setExpressions;
        private Predicate _whereClause;

        public PgUpdateConflictAction()
        {
            _setExpressions = new List<AssignExpression>();
        }

        public PgUpdateConflictAction Set<T>(ColumnExpression<T> column, T value) => SetInternal(column, new ParamExpression<T>(value));

        public PgUpdateConflictAction Set<T>(ColumnExpression<T> column, Expression<T> value) => SetInternal(column, value);

        private PgUpdateConflictAction SetInternal<T>(ColumnExpression<T> left, Expression<T> right)
        {
            var expression = new AssignExpression(left.UnqualifiedName, right);
            _setExpressions.Add(expression);
            return this;
        }

        public PgUpdateConflictAction Where(Predicate condition)
        {
            _whereClause = _whereClause == null
                ? condition
                : _whereClause.And(condition);
            return this;
        }

        public void Format(ISqlWriter sql)
        {
            sql.Append("UPDATE SET ", ", ", _setExpressions);
            sql.Append(" WHERE ", _whereClause);
        }
    }
}