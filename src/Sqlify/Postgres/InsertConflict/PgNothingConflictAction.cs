using Sqlify.Core;

namespace Sqlify.Postgres.InsertConflict
{
    public sealed class PgNothingConflictAction : IPgConflictAction
    {
        public static PgNothingConflictAction Instance = new PgNothingConflictAction();

        public void Format(ISqlWriter sql) => sql.Append("NOTHING");
    }
}