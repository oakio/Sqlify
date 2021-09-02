namespace SqlDsl.Core.Predicates
{
    public sealed class ExistsExpression<TSelectQuery> : PredicateExpression where TSelectQuery : SelectQueryBase<TSelectQuery>, new()
    {
        private readonly TSelectQuery _query;

        public ExistsExpression(TSelectQuery query)
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