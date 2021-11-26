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

        public PredicateExpression In(IReadOnlyCollection<T> items) => new InExpression<T>(this, items);

        public PredicateExpression NotIn(IReadOnlyCollection<T> items) => new NotInExpression<T>(this, items);

        public PredicateExpression In(SelectQuery<T> query) => new InSubQueryExpression<T>(this, query);

        public PredicateExpression NotIn(SelectQuery<T> query) => new NotInSubQueryExpression<T>(this, query);

        public PredicateExpression Between(T from, T to) => new BetweenExpression<T>(this, from, to);

        public PredicateExpression IsNull => new IsNullExpression<T>(this);

        public PredicateExpression IsNotNull => new IsNotNullExpression<T>(this);

        public override void Format(ISqlWriter sql) => sql.Append(Name);
    }
}