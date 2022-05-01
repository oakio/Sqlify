using Sqlify.Core;
using Sqlify.Core.Expressions;
using Sqlify.Postgres.Clauses;

namespace Sqlify.Postgres
{
    public class PgDeleteQuery : DeleteQueryBase<PgDeleteQuery>
    {
        private ReturningClause? _returningClause;

        public PgDeleteQuery(ITable table) : base(table)
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