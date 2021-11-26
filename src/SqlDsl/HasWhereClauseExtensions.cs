using SqlDsl.Core;
using SqlDsl.Core.Predicates;

namespace SqlDsl
{
    public static class HasWhereClauseExtensions
    {
        public static TQuery Where<TQuery>(
            this TQuery query,
            PredicateExpression condition1,
            PredicateExpression condition2)
            where TQuery : IHasWhereClause<TQuery>
        {
            PredicateExpression condition = condition1.And(condition2);
            return query.Where(condition);
        }

        public static TQuery Where<TQuery>(
            this TQuery query,
            PredicateExpression condition1,
            PredicateExpression condition2,
            PredicateExpression condition3)
            where TQuery : IHasWhereClause<TQuery>
        {
            PredicateExpression condition = condition1.And(condition2).And(condition3);
            return query.Where(condition);
        }

        public static TQuery Where<TQuery>(
            this TQuery query,
            PredicateExpression condition1,
            PredicateExpression condition2,
            PredicateExpression condition3,
            PredicateExpression condition4)
            where TQuery : IHasWhereClause<TQuery>
        {
            PredicateExpression condition = condition1.And(condition2).And(condition3).And(condition4);
            return query.Where(condition);
        }

        public static TQuery Where<TQuery>(
            this TQuery query,
            PredicateExpression condition1,
            PredicateExpression condition2,
            PredicateExpression condition3,
            PredicateExpression condition4,
            PredicateExpression condition5)
            where TQuery : IHasWhereClause<TQuery>
        {
            PredicateExpression condition = condition1.And(condition2).And(condition3).And(condition4).And(condition5);
            return query.Where(condition);
        }
    }
}