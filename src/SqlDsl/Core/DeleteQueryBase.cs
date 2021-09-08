using System.Collections.Generic;
using SqlDsl.Core.Clauses;
using SqlDsl.Core.Predicates;

namespace SqlDsl.Core
{
    public abstract class DeleteQueryBase<TDeleteQuery> : IQuery where TDeleteQuery : DeleteQueryBase<TDeleteQuery>
    {
        private readonly Table _table;
        private List<JoinClause> _joinClauses;
        private PredicateExpression _whereClause;

        protected DeleteQueryBase(Table table)
        {
            _table = table;
        }

        public TDeleteQuery Where(PredicateExpression condition)
        {
            _whereClause = _whereClause == null
                ? condition
                : _whereClause.And(condition);
            return Self();
        }

        public TDeleteQuery WhereExists<TSelectQuery>(TSelectQuery query) where TSelectQuery : SelectQueryBase<TSelectQuery>, new()
        {
            var condition = new ExistsExpression<TSelectQuery>(query);
            return Where(condition);
        }

        public TDeleteQuery WhereNotExists<TSelectQuery>(TSelectQuery query) where TSelectQuery : SelectQueryBase<TSelectQuery>, new()
        {
            var condition = new NotExistsExpression<TSelectQuery>(query);
            return Where(condition);
        }

        public TDeleteQuery Join<T>(T table, PredicateExpression condition) where T : Table
        {
            var join = new JoinClause(" JOIN ", table, condition);
            ListUtils.Add(ref _joinClauses, join);
            return Self();
        }

        public void Format(ISqlWriter sql)
        {
            string alias = _table.GetAlias();
            string name = _table.GetName();

            if (string.IsNullOrEmpty(alias))
            {
                sql.Append("DELETE FROM ");
                sql.Append(name);
            }
            else
            {
                sql.Append("DELETE ");
                sql.Append(alias);
                sql.Append(" FROM ");
                sql.Append(name);
                sql.Append(" ");
                sql.Append(alias);
            }

            sql.Append(_joinClauses);
            sql.Append(" WHERE ", _whereClause);
        }

        protected TDeleteQuery Self() => this as TDeleteQuery;
    }
}