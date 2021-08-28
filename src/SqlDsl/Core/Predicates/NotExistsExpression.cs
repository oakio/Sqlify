namespace SqlDsl.Core.Predicates
{
    public sealed class NotExistsExpression<TSqlQuery> : PredicateExpression where TSqlQuery : SelectSqlQueryBase<TSqlQuery>, new()
    {
        private readonly TSqlQuery _query;

        public NotExistsExpression(TSqlQuery query)
        {
            _query = query;
        }

        public override void Format(ISqlWriter writer)
        {
            writer.Append("NOT EXISTS (");
            _query.Format(writer);
            writer.Append(")");
        }
    }
}