using SqlDsl.Core;
using SqlDsl.Core.Expressions;
using SqlDsl.Postgres.Clauses;

namespace SqlDsl.Postgres
{
    public class PgDeleteQuery : DeleteQueryBase<PgDeleteQuery>
    {
        private ReturningClause? _returningClause;

        public PgDeleteQuery(Table table) : base(table)
        {
        }

        public PgDeleteQuery Returning()
        {
            _returningClause = new ReturningClause();
            return this;
        }

        public PgDeleteQuery Returning(params Expression[] columns)
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