using SqlDsl.Core.Predicates;

namespace SqlDsl.Core.Clauses
{
    public readonly struct JoinClause : ISqlFormattable
    {
        private readonly string _type;
        private readonly ITable _table;
        private readonly PredicateExpression _condition;

        public JoinClause(string type, ITable table, PredicateExpression condition)
        {
            _type = type;
            _condition = condition;
            _table = table;
        }

        public void Format(ISqlWriter sql)
        {
            var tableName = _table.GetName();
            var tableAlias = _table.GetAlias();

            sql.Append(_type);
            
            sql.Append(tableName);
            if (!string.IsNullOrEmpty(tableAlias))
            {
                sql.Append(" ");
                sql.Append(tableAlias);
            }
            sql.Append(" ON ");
            _condition.Format(sql);
        }
    }
}