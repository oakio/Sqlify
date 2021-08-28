using SqlDsl.Core.Predicates;

namespace SqlDsl.Core.Expressions
{
    public abstract class Expression : ISqlFormattable
    {
        public abstract void Format(ISqlWriter sql);
    }

    public abstract class Expression<T> : Expression
    {
        public AliasExpression<T> As(string alias) => new AliasExpression<T>(this, alias);

        public static PredicateExpression operator ==(Expression<T> left, T right) =>
            right == null
                ? (PredicateExpression)new IsNullExpression<T>(left)
                : new EqExpression<T>(left, AsParam(right));

        public static PredicateExpression operator ==(Expression<T> left, Expression<T> right) => new EqExpression<T>(left, right);

        public static PredicateExpression operator !=(Expression<T> left, T right) =>
            right == null
                ? (PredicateExpression)new IsNotNullExpression<T>(left)
                : new NeqExpression<T>(left, AsParam(right));

        public static PredicateExpression operator !=(Expression<T> left, Expression<T> right) => new NeqExpression<T>(left, right);

        public static PredicateExpression operator >(Expression<T> left, T right) => new GtExpression<T>(left, AsParam(right));

        public static PredicateExpression operator >(Expression<T> left, Expression<T> right) => new GtExpression<T>(left, right);

        public static PredicateExpression operator <(Expression<T> left, T right) => new LtExpression<T>(left, AsParam(right));

        public static PredicateExpression operator <(Expression<T> left, Expression<T> right) => new LtExpression<T>(left, right);

        public static PredicateExpression operator >=(Expression<T> left, T right) => new GteExpression<T>(left, AsParam(right));

        public static PredicateExpression operator >=(Expression<T> left, Expression<T> right) => new GteExpression<T>(left, right);

        public static PredicateExpression operator <=(Expression<T> left, T right) => new LteExpression<T>(left, AsParam(right));

        public static PredicateExpression operator <=(Expression<T> left, Expression<T> right) => new LteExpression<T>(left, right);

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