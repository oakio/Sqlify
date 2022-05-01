namespace Sqlify.Core.Predicates
{
    public sealed class NotExistsPredicate<TSelectQuery> : Predicate where TSelectQuery : SelectQueryBase<TSelectQuery>, new()
    {
        private readonly TSelectQuery _query;

        public NotExistsPredicate(TSelectQuery query)
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