using SqlDsl.Core;

namespace SqlDsl
{
    public class InsertQuery : InsertQueryBase<InsertQuery>
    {
        public InsertQuery(Table table) : base(table)
        {
        }
    }
}