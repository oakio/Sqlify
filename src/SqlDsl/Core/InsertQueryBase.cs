using System.Collections.Generic;
using SqlDsl.Core.Expressions;

namespace SqlDsl.Core
{
    public abstract class InsertQueryBase<TInsertQuery> : IQuery where TInsertQuery : InsertQueryBase<TInsertQuery>
    {
        private readonly Table _table;
        private readonly List<IInsertValue> _values;

        protected InsertQueryBase(Table table)
        {
            _table = table;
            _values = new List<IInsertValue>();
        }

        public TInsertQuery Values<T>(ColumnExpression<T> column, T value)
        {
            var insertValue = new InsertValue<T>(column, value);
            _values.Add(insertValue);
            return Self();
        }

        public virtual void Format(ISqlWriter sql)
        {
            sql.Append("INSERT INTO ");
            sql.Append(_table.GetName());
            sql.Append(" (");
            for (int i = 0; i < _values.Count; i++)
            {
                if (i > 0)
                {
                    sql.Append(", ");
                }
                _values[i].WriteColumn(sql);
            }
            sql.Append(") VALUES (");
            for (int i = 0; i < _values.Count; i++)
            {
                if (i > 0)
                {
                    sql.Append(", ");
                }
                _values[i].WriteValue(sql);
            }
            sql.Append(")");
        }

        protected TInsertQuery Self() => this as TInsertQuery;
    }
}