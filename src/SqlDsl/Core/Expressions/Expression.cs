using SqlDsl.Core.Predicates;

namespace SqlDsl.Core.Expressions
{
    public abstract class Expression : ISqlFormattable
    {
        public abstract void Format(ISqlWriter sql);
    }

#pragma warning disable 660,661
    public abstract class Expression<T> : Expression
#pragma warning restore 660,661
    {
        public AliasExpression<T> As(string alias) => new AliasExpression<T>(this, alias);

        public static Predicate operator ==(Expression<T> left, T right) =>
            right == null
                ? (Predicate)new IsNullPredicate<T>(left)
                : new EqPredicate<T>(left, AsParam(right));

        public static Predicate operator ==(Expression<T> left, Expression<T> right) => new EqPredicate<T>(left, right);

        public static Predicate operator !=(Expression<T> left, T right) =>
            right == null
                ? (Predicate)new IsNotNullPredicate<T>(left)
                : new NeqPredicate<T>(left, AsParam(right));

        public static Predicate operator !=(Expression<T> left, Expression<T> right) => new NeqPredicate<T>(left, right);

        public static Predicate operator >(Expression<T> left, T right) => new GtPredicate<T>(left, AsParam(right));

        public static Predicate operator >(Expression<T> left, Expression<T> right) => new GtPredicate<T>(left, right);

        public static Predicate operator <(Expression<T> left, T right) => new LtPredicate<T>(left, AsParam(right));

        public static Predicate operator <(Expression<T> left, Expression<T> right) => new LtPredicate<T>(left, right);

        public static Predicate operator >=(Expression<T> left, T right) => new GtePredicate<T>(left, AsParam(right));

        public static Predicate operator >=(Expression<T> left, Expression<T> right) => new GtePredicate<T>(left, right);

        public static Predicate operator <=(Expression<T> left, T right) => new LtePredicate<T>(left, AsParam(right));

        public static Predicate operator <=(Expression<T> left, Expression<T> right) => new LtePredicate<T>(left, right);

        public static BinaryExpression<T> operator +(Expression<T> left, Expression<T> right) => new AddExpression<T>(left, right);

        public static BinaryExpression<T> operator +(Expression<T> left, T right) => new AddExpression<T>(left, AsParam(right));

        public static BinaryExpression<T> operator -(Expression<T> left, Expression<T> right) => new SubExpression<T>(left, right);

        public static BinaryExpression<T> operator -(Expression<T> left, T right) => new SubExpression<T>(left, AsParam(right));

        public static BinaryExpression<T> operator *(Expression<T> left, Expression<T> right) => new MulExpression<T>(left, right);

        public static BinaryExpression<T> operator *(Expression<T> left, T right) => new MulExpression<T>(left, AsParam(right));

        public static BinaryExpression<T> operator /(Expression<T> left, Expression<T> right) => new DivExpression<T>(left, right);

        public static BinaryExpression<T> operator /(Expression<T> left, T right) => new DivExpression<T>(left, AsParam(right));

        private static ParamExpression<T> AsParam(T value) => new ParamExpression<T>(value);
    }
}