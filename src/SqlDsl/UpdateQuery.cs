using System.Collections.Generic;
using SqlDsl.Core;
using SqlDsl.Core.Expressions;
using SqlDsl.Core.Predicates;

namespace SqlDsl
{
    public class UpdateQuery : IQuery
    {
        private readonly Table _table;
        private readonly List<AssignExpression> _setExpressions;
        private PredicateExpression _whereClause;

        public UpdateQuery(Table table)
        {
            _table = table;
            _setExpressions = new List<AssignExpression>();
        }

        public void Format(ISqlWriter sql)
        {
            sql.Append("UPDATE ");
            sql.Append(_table.GetName());
            sql.Append(" SET ", ", ", _setExpressions);
            sql.Append(" WHERE ", _whereClause);
        }

        public UpdateQuery Set<T>(ColumnExpression<T> column, T value) => SetInternal(column, new ParamExpression<T>(value));

        public UpdateQuery Set<T>(ColumnExpression<T> column, Expression<T> value) => SetInternal(column, value);

        private UpdateQuery SetInternal<T>(Expression<T> left, Expression<T> right)
        {
            var expression = new AssignExpression(left, right);
            _setExpressions.Add(expression);
            return this;
        }

        public UpdateQuery Where(PredicateExpression condition)
        {
            _whereClause = _whereClause == null
                ? condition
                : _whereClause.And(condition);
            return this;
        }

        public UpdateQuery WhereExists<TSubQuery>(TSubQuery query) where TSubQuery : SelectQueryBase<TSubQuery>, new()
        {
            var condition = new ExistsExpression<TSubQuery>(query);
            return Where(condition);
        }

        public UpdateQuery WhereNotExists<TSubQuery>(TSubQuery query) where TSubQuery : SelectQueryBase<TSubQuery>, new()
        {
            var condition = new NotExistsExpression<TSubQuery>(query);
            return Where(condition);
        }
    }
}