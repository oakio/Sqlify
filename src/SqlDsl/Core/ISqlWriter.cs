namespace SqlDsl.Core
{
    public interface ISqlWriter
    {
        void Append(string text);

        string AddParam<T>(T value);
    }
}