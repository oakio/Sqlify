using System.Runtime.CompilerServices;
using NUnit.Framework;
using SqlDsl.Core.Expressions;

namespace SqlDsl.Tests.Core.Expressions
{
    [TestFixture]
    public class MathParenthesesTests
    {
        private static readonly ColumnExpression<int> C1 = Create("c1");
        private static readonly ColumnExpression<int> C2 = Create("c2");
        private static readonly ColumnExpression<int> C3 = Create("c3");
        private static readonly ColumnExpression<int> C4 = Create("c4");

        [TestCaseSource(nameof(Cases))]
        public void Test(string lineNo, BinaryExpression<int> expression, string expected) => expression.ShouldBe(expected);

        private static readonly object[] Cases =
        {
            Case(C1 + C2, "c1 + c2"),
            Case(C1 - C2, "c1 - c2"),
            Case(C1 * C2, "c1 * c2"),
            Case(C1 / C2, "c1 / c2"),

            Case(C1 + C2 + C3, "c1 + c2 + c3"),
            Case(C1 - C2 - C3, "c1 - c2 - c3"),
            Case(C1 * C2 * C3, "c1 * c2 * c3"),
            Case(C1 / C2 / C3, "c1 / c2 / c3"),

            Case(C1 * (C2 + C3), "c1 * (c2 + c3)"),
            Case(C1 / (C2 + C3), "c1 / (c2 + c3)"),
            Case(C1 * (C2 - C3), "c1 * (c2 - c3)"),
            Case(C1 / (C2 - C3), "c1 / (c2 - c3)"),
            Case((C2 + C3) * C4, "(c2 + c3) * c4"),
            Case((C2 + C3) / C4, "(c2 + c3) / c4"),
            Case((C2 - C3) * C4, "(c2 - c3) * c4"),
            Case((C2 - C3) / C4, "(c2 - c3) / c4"),

            Case(C1 + C2 * C3, "c1 + c2 * c3"),
            Case(C1 + C2 / C3, "c1 + c2 / c3"),
            Case(C1 - C2 * C3, "c1 - c2 * c3"),
            Case(C1 - C2 / C3, "c1 - c2 / c3"),
            Case(C2 * C3 + C4, "c2 * c3 + c4"),
            Case(C2 / C3 + C4, "c2 / c3 + c4"),
            Case(C2 * C3 - C4, "c2 * c3 - c4"),
            Case(C2 / C3 - C4, "c2 / c3 - c4"),

            Case((C1 + C2) * (C3 + C4), "(c1 + c2) * (c3 + c4)"),
            Case((C1 + C2) / (C3 + C4), "(c1 + c2) / (c3 + c4)"),
            Case((C1 - C2) * (C3 - C4), "(c1 - c2) * (c3 - c4)"),
            Case((C1 - C2) / (C3 - C4), "(c1 - c2) / (c3 - c4)"),

            Case(C1 * C2 + C3 * C4, "c1 * c2 + c3 * c4"),
            Case(C1 / C2 + C3 / C4, "c1 / c2 + c3 / c4"),
            Case(C1 * C2 - C3 * C4, "c1 * c2 - c3 * c4"),
            Case(C1 / C2 - C3 / C4, "c1 / c2 - c3 / c4"),
        };

        private static object[] Case(BinaryExpression<int> expression, string expected, [CallerLineNumber] int lineNo = 0) => new object[]
        {
            $"line #{lineNo}", expression, expected
        };

        private static ColumnExpression<int> Create(string name) => new ColumnExpression<int>(name);
    }
}