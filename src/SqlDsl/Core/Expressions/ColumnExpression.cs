using System.Collections.Generic;
using SqlDsl.Core.Predicates;

namespace SqlDsl.Core.Expressions
{
    public sealed class ColumnExpression<T> : Expression<T>
    {
        private readonly string _name;

        public ColumnExpression(string name)
        {
            _name = name;
        }

        public PredicateExpression In(IReadOnlyCollection<T> items) => new InExpression<T>(this, items);

        public PredicateExpression NotIn(IReadOnlyCollection<T> items) => new NotInExpression<T>(this, items);

        public PredicateExpression In(SelectSqlQuery<T> query) => new InSubQueryExpression<T>(this, query);

        public PredicateExpression NotIn(SelectSqlQuery<T> query) => new NotInSubQueryExpression<T>(this, query);

        public PredicateExpression Between(T from, T to) => new BetweenExpression<T>(this, from, to);

        public PredicateExpression IsNull => new IsNullExpression<T>(this);

        public PredicateExpression IsNotNull => new IsNotNullExpression<T>(this);

        public static ColumnExpression<T> Asterisk = new ColumnExpression<T>("*");

        public override void Format(ISqlWriter sql) => sql.Append(_name);
    }
}