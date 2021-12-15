using System.Collections.Generic;
using SqlDsl.Core.Clauses;
using SqlDsl.Core.Predicates;

namespace SqlDsl.Core
{
    public abstract class DeleteQueryBase<TDeleteQuery> : IQuery, IHasWhereClause<TDeleteQuery> where TDeleteQuery : DeleteQueryBase<TDeleteQuery>
    {
        private readonly TableReference _table;
        private List<JoinClause> _joinClauses;
        private Predicate _whereClause;

        protected DeleteQueryBase(ITable table)
        {
            _table = new TableReference(table, false);
        }

        public TDeleteQuery Where(Predicate condition)
        {
            _whereClause = _whereClause == null
                ? condition
                : _whereClause.And(condition);
            return Self();
        }

        public TDeleteQuery WhereExists<TSelectQuery>(TSelectQuery query) where TSelectQuery : SelectQueryBase<TSelectQuery>, new()
        {
            var condition = new ExistsPredicate<TSelectQuery>(query);
            return Where(condition);
        }

        public TDeleteQuery WhereNotExists<TSelectQuery>(TSelectQuery query) where TSelectQuery : SelectQueryBase<TSelectQuery>, new()
        {
            var condition = new NotExistsPredicate<TSelectQuery>(query);
            return Where(condition);
        }

        public TDeleteQuery Join(ITable table, Predicate condition)
        {
            var join = new JoinClause(" JOIN ", table, condition);
            ListUtils.Add(ref _joinClauses, join);
            return Self();
        }

        public virtual void Format(ISqlWriter sql)
        {
            sql.Append("DELETE FROM ", _table);
            sql.Append(_joinClauses);
            sql.Append(" WHERE ", _whereClause);
        }

        protected TDeleteQuery Self() => this as TDeleteQuery;

        public override string ToString() => QueryFormatUtils.ToString(this);
    }
}