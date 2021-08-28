using System.Collections.Generic;
using SqlDsl.Core;
using SqlDsl.Core.Expressions;
using SqlDsl.Core.Predicates;

namespace SqlDsl
{
    public class UpdateSqlQuery : ISqlQuery
    {
        private readonly Table _table;
        private readonly List<AssignExpression> _setExpressions;
        private PredicateExpression _whereClause;

        public UpdateSqlQuery(Table table)
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

        public UpdateSqlQuery Set<T>(ColumnExpression<T> column, T value) => SetInternal(column, new ParamExpression<T>(value));

        public UpdateSqlQuery Set<T>(ColumnExpression<T> column, Expression<T> value) => SetInternal(column, value);

        private UpdateSqlQuery SetInternal<T>(Expression<T> left, Expression<T> right)
        {
            var expression = new AssignExpression(left, right);
            _setExpressions.Add(expression);
            return this;
        }

        public UpdateSqlQuery Where(PredicateExpression condition)
        {
            _whereClause = _whereClause == null
                ? condition
                : _whereClause.And(condition);
            return this;
        }

        public UpdateSqlQuery WhereExists<TSqlSubQuery>(TSqlSubQuery query) where TSqlSubQuery : SelectSqlQueryBase<TSqlSubQuery>, new()
        {
            var condition = new ExistsExpression<TSqlSubQuery>(query);
            return Where(condition);
        }

        public UpdateSqlQuery WhereNotExists<TSqlSubQuery>(TSqlSubQuery query) where TSqlSubQuery : SelectSqlQueryBase<TSqlSubQuery>, new()
        {
            var condition = new NotExistsExpression<TSqlSubQuery>(query);
            return Where(condition);
        }
    }
}