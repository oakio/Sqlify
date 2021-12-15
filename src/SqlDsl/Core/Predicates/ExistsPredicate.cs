namespace SqlDsl.Core.Predicates
{
    public sealed class ExistsPredicate<TSelectQuery> : Predicate where TSelectQuery : SelectQueryBase<TSelectQuery>, new()
    {
        private readonly TSelectQuery _query;

        public ExistsPredicate(TSelectQuery query)
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