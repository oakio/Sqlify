using SqlDsl.Core.Expressions;
using SqlDsl.Postgres.InsertConflict;

namespace SqlDsl.Postgres
{
    public static class PgConflict
    {
        public static PgIndexColumnsConflictTarget Columns(params Expression[] columns) => new PgIndexColumnsConflictTarget(columns);

        public static PgNothingConflictAction DoNothing = PgNothingConflictAction.Instance;

        public static PgUpdateConflictAction DoUpdate() => new PgUpdateConflictAction();
    }
}