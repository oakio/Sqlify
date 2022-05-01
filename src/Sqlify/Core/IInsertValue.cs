namespace Sqlify.Core
{
    internal interface IInsertValue
    {
        void WriteColumn(ISqlWriter sql);

        void WriteValue(ISqlWriter sql);
    }
}