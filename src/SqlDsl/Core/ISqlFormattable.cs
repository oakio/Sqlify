namespace SqlDsl.Core
{
    public interface ISqlFormattable
    {
        void Format(ISqlWriter sql);
    }
}