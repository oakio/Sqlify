namespace SqlDsl.Core
{
    public static class QueryFormatUtils
    {
        public static string ToString(IQuery query)
        {
            var writer = new SqlWriter();
            query.Format(writer);
            string sql = writer.GetCommand();
            return sql;
        }
    }
}