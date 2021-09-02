using System.Collections.Generic;
using SqlDsl.Core.Expressions;
using SqlDsl.Core.Predicates;

namespace SqlDsl.Core
{
    public abstract class UpdateQueryBase<TUpdateQuery> : IQuery where TUpdateQuery : UpdateQueryBase<TUpdateQuery>
    {
        private readonly Table _table;
        private readonly List<AssignExpression> _setExpressions;
        private PredicateExpression _whereClause;

        protected UpdateQueryBase(Table table)
        {
            _table = table;
            _setExpressions = new List<AssignExpression>();
        }

        public virtual void Format(ISqlWriter sql)
        {
            sql.Append("UPDATE ");
            sql.Append(_table.GetName());
            sql.Append(" SET ", ", ", _setExpressions);
            sql.Append(" WHERE ", _whereClause);
        }

        public TUpdateQuery Set<T>(ColumnExpression<T> column, T value) => SetInternal(column, new ParamExpression<T>(value));

        public TUpdateQuery Set<T>(ColumnExpression<T> column, Expression<T> value) => SetInternal(column, value);

        private TUpdateQuery SetInternal<T>(Expression<T> left, Expression<T> right)
        {
            var expression = new AssignExpression(left, right);
            _setExpressions.Add(expression);
            return Self();
        }

        public TUpdateQuery Where(PredicateExpression condition)
        {
            _whereClause = _whereClause == null
                ? condition
                : _whereClause.And(condition);
            return Self();
        }

        public TUpdateQuery WhereExists<TSelectQuery>(TSelectQuery query) where TSelectQuery : SelectQueryBase<TSelectQuery>, new()
        {
            var condition = new ExistsExpression<TSelectQuery>(query);
            return Where(condition);
        }

        public TUpdateQuery WhereNotExists<TSelectQuery>(TSelectQuery query) where TSelectQuery : SelectQueryBase<TSelectQuery>, new()
        {
            var condition = new NotExistsExpression<TSelectQuery>(query);
            return Where(condition);
        }

        protected TUpdateQuery Self() => this as TUpdateQuery;
    }
}