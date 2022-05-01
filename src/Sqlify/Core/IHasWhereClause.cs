using Sqlify.Core.Predicates;

namespace Sqlify.Core
{
    public interface IHasWhereClause<out TQuery>
    {
        TQuery Where(Predicate condition);

        TQuery WhereExists<TSubQuery>(TSubQuery query) where TSubQuery : SelectQueryBase<TSubQuery>, new();

        TQuery WhereNotExists<TSubQuery>(TSubQuery query) where TSubQuery : SelectQueryBase<TSubQuery>, new();
    }
}