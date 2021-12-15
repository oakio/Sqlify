using NUnit.Framework;
using SqlDsl.Core.Expressions;
using SqlDsl.Core.Predicates;

namespace SqlDsl.Tests.Core.Predicates
{
    [TestFixture]
    public class BetweenPredicateTests
    {
        [Test]
        public void FormatTests()
        {
            var column = new ColumnExpression<int>("age");
            var expression = new BetweenPredicate<int>(column, 10, 15);

            expression.ShouldBe("age BETWEEN @p1 AND @p2");
        }
    }
}