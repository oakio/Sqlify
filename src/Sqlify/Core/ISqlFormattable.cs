namespace Sqlify.Core
{
    public interface ISqlFormattable
    {
        void Format(ISqlWriter sql);
    }
}