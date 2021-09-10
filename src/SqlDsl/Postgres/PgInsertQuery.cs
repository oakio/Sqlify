using SqlDsl.Core;
using SqlDsl.Core.Expressions;
using SqlDsl.Postgres.Clauses;
using SqlDsl.Postgres.InsertConflict;

namespace SqlDsl.Postgres
{
    public class PgInsertQuery : InsertQueryBase<PgInsertQuery>
    {
        private OnConflictClause? _onConflictClause;
        private ReturningClause? _returningClause;

        public PgInsertQuery(Table table) : base(table)
        {
        }

        public PgInsertQuery OnConflict(IPgConflictTarget target, IPgConflictAction action)
        {
            _onConflictClause = new OnConflictClause(target, action);
            return Self();
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

            _onConflictClause?.Format(sql);
            _returningClause?.Format(sql);
        }
    }
}