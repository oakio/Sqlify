using SqlDsl.Core;

namespace SqlDsl.Postgres.Clauses
{
    public readonly struct LimitClause : ISqlFormattable
    {
        private readonly int _limit;

        public LimitClause(int limit)
        {
            _limit = limit;
        }

        public void Format(ISqlWriter sql)
        {
            string name = sql.AddParam(_limit);
            sql.Append(" LIMIT ");
            sql.Append(name);
        }
    }
}