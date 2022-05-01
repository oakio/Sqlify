namespace Sqlify.Core
{
    public readonly struct TableReference : ISqlFormattable
    {
        private readonly ITable _table;
        private readonly bool _useAs;

        public TableReference(ITable table, bool useAs)
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