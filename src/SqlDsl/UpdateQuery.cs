using SqlDsl.Core;

namespace SqlDsl
{
    public class UpdateQuery : UpdateQueryBase<UpdateQuery>
    {
        public UpdateQuery(Table table) : base(table)
        {
        }
    }
}