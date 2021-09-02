namespace SqlDsl.Core.Predicates
{
    public sealed class NotExistsExpression<TSelectQuery> : PredicateExpression where TSelectQuery : SelectQueryBase<TSelectQuery>, new()
    {
        private readonly TSelectQuery _query;

        public NotExistsExpression(TSelectQuery query)
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