using System.Collections.Generic;
using SqlDsl.Core.Clauses;
using SqlDsl.Core.Expressions;
using SqlDsl.Core.Predicates;

namespace SqlDsl.Core
{
    public class SelectSqlQueryBase<TSqlQuery> : ISelectSqlQuery where TSqlQuery : SelectSqlQueryBase<TSqlQuery>, new()
    {
        private UnionQuery<TSqlQuery> _unionQuery;

        private bool _distinct;
        private List<Expression> _selectClause;
        private FromClause _fromClause;
        private List<JoinClause> _joinClauses;
        private PredicateExpression _whereClause;
        private List<OrderByClause> _orderByClause;
        private List<Expression> _groupByClause;
        private PredicateExpression _havingClause;

        protected TSqlQuery SelectColumn(Expression column)
        {
            ListUtils.Add(ref _selectClause, column);
            return Self();
        }

        protected TSqlQuery SelectColumns(Expression[] columns)
        {
            ListUtils.AddRange(ref _selectClause, columns);
            return Self();
        }

        public TSqlQuery Distinct()
        {
            _distinct = true;
            return Self();
        }

        public TSqlQuery From<T>(T table) where T : Table
        {
            _fromClause = new FromClause(table);
            return Self();
        }

        public TSqlQuery From<T>(ISqlQuery nested, T alias) where T : Table
        {
            _fromClause = new FromClause(nested, alias);
            return Self();
        }

        public TSqlQuery Where(PredicateExpression condition)
        {
            _whereClause = _whereClause == null
                ? condition
                : _whereClause.And(condition);
            return Self();
        }

        public TSqlQuery WhereExists<TSqlSubQuery>(TSqlSubQuery query) where TSqlSubQuery : SelectSqlQueryBase<TSqlSubQuery>, new()
        {
            var condition = new ExistsExpression<TSqlSubQuery>(query);
            return Where(condition);
        }

        public TSqlQuery WhereNotExists<TSqlSubQuery>(TSqlSubQuery query) where TSqlSubQuery : SelectSqlQueryBase<TSqlSubQuery>, new()
        {
            var condition = new NotExistsExpression<TSqlSubQuery>(query);
            return Where(condition);
        }

        public TSqlQuery LeftJoin<T>(T table, PredicateExpression condition) where T : Table => Join(" LEFT JOIN ", table, condition);

        public TSqlQuery RightJoin<T>(T table, PredicateExpression condition) where T : Table => Join(" RIGHT JOIN ", table, condition);

        public TSqlQuery Join<T>(T table, PredicateExpression condition) where T : Table => Join(" JOIN ", table, condition);

        public TSqlQuery FullJoin<T>(T table, PredicateExpression condition) where T : Table => Join(" FULL JOIN ", table, condition);

        private TSqlQuery Join<T>(string type, T table, PredicateExpression condition) where T : Table
        {
            var join = new JoinClause(type, table, condition);
            ListUtils.Add(ref _joinClauses, join);
            return Self();
        }

        public TSqlQuery Having(PredicateExpression condition)
        {
            _havingClause = condition;
            return Self();
        }

        public TSqlQuery GroupBy<T>(ColumnExpression<T> column)
        {
            ListUtils.Add(ref _groupByClause, column);
            return Self();
        }

        public TSqlQuery OrderBy<T>(Expression<T> expression)
        {
            var orderBy = new OrderByClause(expression, false);
            ListUtils.Add(ref _orderByClause, orderBy);
            return Self();
        }

        public TSqlQuery OrderByDesc<T>(Expression<T> expression)
        {
            var orderBy = new OrderByClause(expression, true);
            ListUtils.Add(ref _orderByClause, orderBy);
            return Self();
        }

        public TSqlQuery Union() => Union(false);

        public TSqlQuery UnionAll() => Union(true);

        private TSqlQuery Union(bool all)
        {
            TSqlQuery self = Self();
            var newQuery = new TSqlQuery();

            var newQueryBase = newQuery as SelectSqlQueryBase<TSqlQuery>;
            newQueryBase._unionQuery = new UnionQuery<TSqlQuery>(self, all);

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

        protected TSqlQuery Self() => this as TSqlQuery;
    }
}
