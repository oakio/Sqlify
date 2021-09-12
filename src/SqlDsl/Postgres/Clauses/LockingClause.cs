using System;
using SqlDsl.Core;

namespace SqlDsl.Postgres.Clauses
{
    public readonly struct LockingClause : ISqlFormattable
    {
        private readonly PgLockMode _mode;

        public LockingClause(PgLockMode mode)
        {
            _mode = mode;
        }

        public void Format(ISqlWriter sql)
        {
            switch (_mode)
            {
                case PgLockMode.Update:
                    sql.Append(" FOR UPDATE");
                    return;
                case PgLockMode.NoKeyUpdate:
                    sql.Append(" FOR NO KEY UPDATE");
                    return;
                case PgLockMode.Share:
                    sql.Append(" FOR SHARE");
                    return;
                case PgLockMode.KeyShare:
                    sql.Append(" FOR KEY SHARE");
                    return;
                default:
                    throw new ArgumentOutOfRangeException(_mode.ToString());
            }
        }
    }
}