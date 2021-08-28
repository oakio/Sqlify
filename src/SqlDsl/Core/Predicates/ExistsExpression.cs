namespace SqlDsl.Core.Predicates
{
    public sealed class ExistsExpression<TSqlQuery> : PredicateExpression where TSqlQuery : SelectSqlQueryBase<TSqlQuery>, new()
    {
        private readonly TSqlQuery _query;

        public ExistsExpression(TSqlQuery query)
        {
            _query = query;
        }

        public override void Format(ISqlWriter writer)
        {
            writer.Append("EXISTS (");
            _query.Format(writer);
            writer.Append(")");
        }
    }
}