using SqlDsl.Core;

namespace SqlDsl.Postgres.Clauses
{
    public readonly struct OffsetClause : ISqlFormattable
    {
        private readonly int _offset;

        public OffsetClause(int offset)
        {
            _offset = offset;
        }

        public void Format(ISqlWriter sql)
        {
            if (_offset == 0)
            {
                return;
            }

            string name = sql.AddParam(_offset);
            sql.Append(" OFFSET ");
            sql.Append(name);
        }
    }
}