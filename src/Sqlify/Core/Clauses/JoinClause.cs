using Sqlify.Core.Predicates;

namespace Sqlify.Core.Clauses
{
    public readonly struct JoinClause : ISqlFormattable
    {
        private readonly string _type;
        private readonly TableReference _table;
        private readonly Predicate _condition;

        public JoinClause(string type, ITable table, Predicate condition)
        {
            _type = type;
            _condition = condition;
            _table = new TableReference(table, false);
        }

        public void Format(ISqlWriter sql)
        {
            sql.Append(_type);
            _table.Format(sql);
            sql.Append(" ON ");
            _condition.Format(sql);
        }
    }
}