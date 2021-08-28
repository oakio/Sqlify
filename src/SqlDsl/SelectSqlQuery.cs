using SqlDsl.Core;
using SqlDsl.Core.Expressions;

namespace SqlDsl
{
    public class SelectSqlQuery : SelectSqlQueryBase<SelectSqlQuery>
    {
        public SelectSqlQuery Select() => this;

        public SelectSqlQuery Select(Expression[] columns)
        {
            SelectColumns(columns);
            return this;
        }
    }

    public class SelectSqlQuery<T> : SelectSqlQueryBase<SelectSqlQuery<T>>
    {
        public SelectSqlQuery<T> Select(Expression<T> column)
        {
            SelectColumn(column);
            return this;
        }
    }
}
