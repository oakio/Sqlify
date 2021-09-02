using SqlDsl.Core;
using SqlDsl.Core.Expressions;
using SqlDsl.Postgres.Clauses;

namespace SqlDsl.Postgres
{
    public class PgInsertQuery : InsertQueryBase<PgInsertQuery>
    {
        private ReturningClause? _returningClause;

        public PgInsertQuery(Table table) : base(table)
        {
        }

        public PgInsertQuery Returning()
        {
            _returningClause = new ReturningClause();
            return this;
        }

        public PgInsertQuery Returning(params Expression[] columns)
        {
            _returningClause = new ReturningClause(columns);
            return this;
        }

        public override void Format(ISqlWriter sql)
        {
            base.Format(sql);
            _returningClause?.Format(sql);
        }
    }
}