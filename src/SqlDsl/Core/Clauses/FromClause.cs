namespace SqlDsl.Core.Clauses
{
    public readonly struct FromClause : ISqlFormattable
    {
        private readonly ISelectQuery _nested;
        private readonly Table _alias;
        private readonly Table _table;

        public FromClause(Table table)
        {
            _table = table;
            _nested = null;
            _alias = null;
        }

        public FromClause(ISelectQuery nested, Table alias)
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