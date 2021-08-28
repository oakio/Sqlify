namespace SqlDsl.Core
{
    public class UnionQuery<TSqlQuery> : ISqlFormattable where TSqlQuery : SelectSqlQueryBase<TSqlQuery>, new()
    {
        public UnionQuery(TSqlQuery query, bool all)
        {
            Query = query;
            All = all;
        }

        public TSqlQuery Query { get; }

        public bool All { get; }

        public void Format(ISqlWriter sql)
        {
            Query.Format(sql);
            sql.Append(All
                ? " UNION ALL "
                : " UNION ");
        }
    }
}