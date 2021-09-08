using SqlDsl.Core;

namespace SqlDsl
{
    public class DeleteQuery : DeleteQueryBase<DeleteQuery>
    {
        public DeleteQuery(Table table) : base(table)
        {
        }
    }
}