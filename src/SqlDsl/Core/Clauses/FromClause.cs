namespace SqlDsl.Core.Clauses
{
    public readonly struct FromClause : ISqlFormattable
    {
        private readonly ISelectQuery _nested;
        private readonly ITable _alias;
        private readonly ITable _table;

        public FromClause(ITable table)
        {
            _table = table;
            _nested = null;
            _alias = null;
        }

        public FromClause(ISelectQuery nested, ITable alias)
        {
            _nested = nested;
            _alias = alias;
            _table = null;
        }

        public void Format(ISqlWriter sql)
        {
            if (_nested != null)
            {
                sql.Append(" FROM (");
                _nested.Format(sql);
                sql.Append(") ");
                sql.Append(_alias.GetAlias());
            }
            else
            {
                sql.Append(" FROM ");
                sql.Append(_table.GetName());

                string alias = _table.GetAlias();
                if (!string.IsNullOrEmpty(alias))
                {
                    sql.Append(" ");
                    sql.Append(alias);
                }
            }
        }
    }
}