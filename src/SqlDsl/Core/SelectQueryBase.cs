using System.Collections.Generic;
using SqlDsl.Core.Clauses;
using SqlDsl.Core.Expressions;
using SqlDsl.Core.Predicates;

namespace SqlDsl.Core
{
    public class SelectQueryBase<TSelectQuery> : ISelectQuery, IHasWhereClause<TSelectQuery> where TSelectQuery : SelectQueryBase<TSelectQuery>, new()
    {
        private UnionQuery<TSelectQuery> _unionQuery;

        private bool _distinct;
        private List<Expression> _selectClause;
        private FromClause _fromClause;
        private List<JoinClause> _joinClauses;
        private PredicateExpression _whereClause;
        private List<OrderByClause> _orderByClause;
        private List<Expression> _groupByClause;
        private PredicateExpression _havingClause;

        protected TSelectQuery SelectColumn(Expression column)
        {
            ListUtils.Add(ref _selectClause, column);
            return Self();
        }

        protected TSelectQuery SelectColumns(Expression[] columns)
        {
            ListUtils.AddRange(ref _selectClause, columns);
            return Self();
        }

        public TSelectQuery Distinct()
        {
            _distinct = true;
            return Self();
        }

        public TSelectQuery From<T>(T table) where T : Table
        {
            _fromClause = new FromClause(table);
            return Self();
        }

        public TSelectQuery From<T>(ISelectQuery nested, T alias) where T : Table
        {
            _fromClause = new FromClause(nested, alias);
            return Self();
        }

        public TSelectQuery Where(PredicateExpression condition)
        {
            _whereClause = _whereClause == null
                ? condition
                : _whereClause.And(condition);
            return Self();
        }

        public TSelectQuery WhereExists<TSubQuery>(TSubQuery query) where TSubQuery : SelectQueryBase<TSubQuery>, new()
        {
            var condition = new ExistsExpression<TSubQuery>(query);
            return Where(condition);
        }

        public TSelectQuery WhereNotExists<TSubQuery>(TSubQuery query) where TSubQuery : SelectQueryBase<TSubQuery>, new()
        {
            var condition = new NotExistsExpression<TSubQuery>(query);
            return Where(condition);
        }

        public TSelectQuery LeftJoin<T>(T table, PredicateExpression condition) where T : Table => Join(" LEFT JOIN ", table, condition);

        public TSelectQuery RightJoin<T>(T table, PredicateExpression condition) where T : Table => Join(" RIGHT JOIN ", table, condition);

        public TSelectQuery Join<T>(T table, PredicateExpression condition) where T : Table => Join(" JOIN ", table, condition);

        public TSelectQuery FullJoin<T>(T table, PredicateExpression condition) where T : Table => Join(" FULL JOIN ", table, condition);

        private TSelectQuery Join<T>(string type, T table, PredicateExpression condition) where T : Table
        {
            var join = new JoinClause(type, table, condition);
            ListUtils.Add(ref _joinClauses, join);
            return Self();
        }

        public TSelectQuery Having(PredicateExpression condition)
        {
            _havingClause = condition;
            return Self();
        }

        public TSelectQuery GroupBy<T>(ColumnExpression<T> column)
        {
            ListUtils.Add(ref _groupByClause, column);
            return Self();
        }

        public TSelectQuery OrderBy<T>(Expression<T> expression)
        {
            var orderBy = new OrderByClause(expression, false);
            ListUtils.Add(ref _orderByClause, orderBy);
            return Self();
        }

        public TSelectQuery OrderByDesc<T>(Expression<T> expression)
        {
            var orderBy = new OrderByClause(expression, true);
            ListUtils.Add(ref _orderByClause, orderBy);
            return Self();
        }

        public TSelectQuery Union() => Union(false);

        public TSelectQuery UnionAll() => Union(true);

        private TSelectQuery Union(bool all)
        {
            TSelectQuery self = Self();
            var newQuery = new TSelectQuery();

            var newQueryBase = newQuery as SelectQueryBase<TSelectQuery>;
            newQueryBase._unionQuery = new UnionQuery<TSelectQuery>(self, all);

            return newQuery;
        }

        public virtual void Format(ISqlWriter sql)
        {
            _unionQuery?.Format(sql);

            AppendSelectFrom(sql);
            sql.Append(_joinClauses);
            sql.Append(" WHERE ", _whereClause);
            sql.Append(" GROUP BY ", ", ", _groupByClause);
            sql.Append(" ORDER BY ", ", ", _orderByClause);
            sql.Append(" HAVING ", _havingClause);
        }

        private void AppendSelectFrom(ISqlWriter sql)
        {
            if (_selectClause == null)
            {
                sql.Append(_distinct ? "SELECT DISTINCT *" : "SELECT *");
            }
            else
            {
                sql.Append(_distinct ? "SELECT DISTINCT " : "SELECT ");
                sql.Append("", ", ", _selectClause);
            }

            _fromClause.Format(sql);
        }

        protected TSelectQuery Self() => this as TSelectQuery;

        public override string ToString() => QueryFormatUtils.ToString(this);
    }
}
