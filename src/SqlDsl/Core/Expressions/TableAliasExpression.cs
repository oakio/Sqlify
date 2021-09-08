namespace SqlDsl.Core.Expressions
{
    public readonly struct TableAliasExpression : ISqlFormattable
    {
        private readonly Table _table;
        private readonly bool _useAs;

        public TableAliasExpression(Table table, bool useAs)
        {
            _table = table;
            _useAs = useAs;
        }

        public void Format(ISqlWriter sql)
        {
            string name = _table.GetName();
            sql.Append(name);

            string alias = _table.GetAlias();

            if (!string.IsNullOrEmpty(alias))
            {
                sql.Append(_useAs ? " AS " : " ");
                sql.Append(alias);
            }
        }
    }
}