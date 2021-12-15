using System.Collections.Generic;
using SqlDsl.Core.Predicates;

namespace SqlDsl.Core.Expressions
{
    public sealed class ColumnExpression<T> : Expression<T>
    {
        public readonly string UnqualifiedName;

        public readonly string Name;

        public ColumnExpression(string unqualifiedName, string tableAlias = null)
        {
            UnqualifiedName = unqualifiedName;

            Name = string.IsNullOrEmpty(tableAlias)
                ? unqualifiedName
                : string.Concat(tableAlias, ".", unqualifiedName);
        }

        public Predicate In(IReadOnlyCollection<T> items) => new InPredicate<T>(this, items);

        public Predicate NotIn(IReadOnlyCollection<T> items) => new NotInPredicate<T>(this, items);

        public Predicate In(SelectQuery<T> query) => new InSubQueryPredicate<T>(this, query);

        public Predicate NotIn(SelectQuery<T> query) => new NotInSubQueryPredicate<T>(this, query);

        public Predicate Between(T from, T to) => new BetweenPredicate<T>(this, from, to);

        public Predicate IsNull => new IsNullPredicate<T>(this);

        public Predicate IsNotNull => new IsNotNullPredicate<T>(this);

        public CastExpression<TTarget> Cast<TTarget>(string dataType) => new CastExpression<TTarget>(this, dataType);

        public override void Format(ISqlWriter sql) => sql.Append(Name);
    }
}