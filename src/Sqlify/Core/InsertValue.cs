using Sqlify.Core.Expressions;

namespace Sqlify.Core
{
    public readonly struct InsertValue<T> : IInsertValue
    {
        private readonly Column<T> _column;
        private readonly T _value;

        public InsertValue(Column<T> column, T value)
        {
            _column = column;
            _value = value;
        }

        public void WriteColumn(ISqlWriter sql) => sql.Append(_column.UnqualifiedName);

        public void WriteValue(ISqlWriter sql)
        {
            string name = sql.AddParam(_value);
            sql.Append(name);
        }
    }
}