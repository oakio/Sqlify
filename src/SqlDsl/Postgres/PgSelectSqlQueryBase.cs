using SqlDsl.Core;
using SqlDsl.Postgres.Clauses;

namespace SqlDsl.Postgres
{
    public abstract class PgSelectSqlQueryBase<TSqlQuery> : SelectSqlQueryBase<TSqlQuery> where TSqlQuery : SelectSqlQueryBase<TSqlQuery>, new()
    {
        private OffsetClause? _offsetClause;
        private LimitClause? _limitClause;

        public TSqlQuery Offset(int offset)
        {
            _offsetClause = new OffsetClause(offset);
            return Self();
        }

        public TSqlQuery Limit(int limit)
        {
            _limitClause = new LimitClause(limit);
            return Self();
        }

        public override void Format(ISqlWriter sql)
        {
            base.Format(sql);
            
            _offsetClause?.Format(sql);
            _limitClause?.Format(sql);
        }
    }
}