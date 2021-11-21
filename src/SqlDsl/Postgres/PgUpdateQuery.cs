using SqlDsl.Core;
using SqlDsl.Core.Expressions;
using SqlDsl.Postgres.Clauses;

namespace SqlDsl.Postgres
{
    public class PgUpdateQuery : UpdateQueryBase<PgUpdateQuery>
    {
        private ReturningClause? _returningClause;

        public PgUpdateQuery(ITable table) : base(table)
        {
        }

        public PgUpdateQuery Returning()
        {
            _returningClause = new ReturningClause();
            return this;
        }

        public PgUpdateQuery Returning(params Expression[] columns)
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