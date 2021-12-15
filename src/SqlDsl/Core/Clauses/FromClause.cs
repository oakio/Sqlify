namespace SqlDsl.Core.Clauses
{
    public readonly struct FromClause : ISqlFormattable
    {
        private readonly ISelectQuery _nested;
        private readonly ITable _alias;
        private readonly TableReference _table;

        public FromClause(ITable table)
        {
            _table = new TableReference(table, false);
            _nested = null;
            _alias = null;
        }

        public FromClause(ISelectQuery nested, ITable alias)
        {
            _nested = nested;
            _alias = alias;
            _table = default;
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
                _table.Format(sql);
            }
        }
    }

    struct AliasedSubQuery : ISqlFormattable
    {
        private readonly ISelectQuery _query;

        public AliasedSubQuery(ISelectQuery query)
        {
            _query = query;
        }

        public void Format(ISqlWriter sql)
        {
            sql.Append("(");
            _query.Format(sql);
            sql.Append(")");
        }
    }
}