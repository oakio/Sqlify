using NUnit.Framework;
using SqlDsl.Core;
using SqlDsl.Core.Expressions;

namespace SqlDsl.Tests.Core.Expressions
{
    [TestFixture]
    public class ColumnExpressionTests
    {
        private static readonly ColumnExpression<int?> Left = new ColumnExpression<int?>("left");
        private static readonly ColumnExpression<int?> Right = new ColumnExpression<int?>("right");

        [TestCaseSource(nameof(Cases))]
        public void Column_expressions_test(ISqlFormattable expression, string expected) => expression.ShouldBe(expected);

        private static readonly object[] Cases =
        {
            // column vs value
            new object[] { Left == 10, "left = @p1" },
            new object[] { Left != 10, "left <> @p1" },
            new object[] { Left == (int?)null, "left IS NULL" },
            new object[] { Left != (int?)null, "left IS NOT NULL" },
            new object[] { Left >= 10, "left >= @p1" },
            new object[] { Left <= 10, "left <= @p1" },
            new object[] { Left > 10, "left > @p1" },
            new object[] { Left < 10, "left < @p1" },
            new object[] { Left + 10, "left + @p1" },
            new object[] { Left - 10, "left - @p1" },
            new object[] { Left * 10, "left * @p1" },
            new object[] { Left / 10, "left / @p1" },

            //column vs column
            new object[] { Left == Right, "left = right" },
            new object[] { Left != Right, "left <> right" },
            new object[] { Left >= Right, "left >= right" },
            new object[] { Left <= Right, "left <= right" },
            new object[] { Left > Right, "left > right" },
            new object[] { Left < Right, "left < right" },
            new object[] { Left + Right, "left + right" },
            new object[] { Left - Right, "left - right" },
            new object[] { Left * Right, "left * right" },
            new object[] { Left / Right, "left / right" },
        };
    }
}