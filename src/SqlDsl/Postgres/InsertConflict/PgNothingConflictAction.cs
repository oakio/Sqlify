using SqlDsl.Core;

namespace SqlDsl.Postgres.InsertConflict
{
    public sealed class PgNothingConflictAction : IPgConflictAction
    {
        public static PgNothingConflictAction Instance = new PgNothingConflictAction();

        public void Format(ISqlWriter sql) => sql.Append("NOTHING");
    }
}