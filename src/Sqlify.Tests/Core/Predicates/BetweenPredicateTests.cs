using NUnit.Framework;
using Sqlify.Core.Expressions;
using Sqlify.Core.Predicates;

namespace Sqlify.Tests.Core.Predicates
{
    [TestFixture]
    public class BetweenPredicateTests
    {
        [Test]
        public void FormatTests()
        {
            var column = new Column<int>("age");
            var expression = new BetweenPredicate<int>(column, 10, 15);

            expression.ShouldBe("age BETWEEN @p1 AND @p2");
        }
    }
}