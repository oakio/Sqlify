namespace Sqlify.Core
{
    public class UnionQuery<TSelectQuery> : ISqlFormattable where TSelectQuery : SelectQueryBase<TSelectQuery>, new()
    {
        public UnionQuery(TSelectQuery query, bool all)
        {
            Query = query;
            All = all;
        }

        public TSelectQuery Query { get; }

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