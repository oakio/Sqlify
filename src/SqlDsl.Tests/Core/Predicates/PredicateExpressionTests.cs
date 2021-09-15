using System.Runtime.CompilerServices;
using NUnit.Framework;
using SqlDsl.Core.Expressions;
using SqlDsl.Core.Predicates;

namespace SqlDsl.Tests.Core.Predicates
{
    [TestFixture]
    public class PredicateExpressionTests
    {
        private static readonly PredicateExpression P1 = Create("c1", 1);
        private static readonly PredicateExpression P2 = Create("c2", 2);
        private static readonly PredicateExpression P3 = Create("c3", 3);
        private static readonly PredicateExpression P4 = Create("c4", 4);

        [TestCaseSource(nameof(Cases))]
        public void Test(string lineNo, PredicateExpression expression, string expected) => expression.ShouldBe(expected);

        private static readonly object[] Cases =
        {
            // AND
            Case(Sql.And(P1, P2), "c1 = @p1 AND c2 = @p2"),
            Case(P1.And(P2), "c1 = @p1 AND c2 = @p2"),
            Case(P1.And(P2).And(P3), "c1 = @p1 AND c2 = @p2 AND c3 = @p3"),
            Case(P1.And(P2.And(P3)), "c1 = @p1 AND c2 = @p2 AND c3 = @p3"),

            // OR
            Case(Sql.Or(P1, P2), "c1 = @p1 OR c2 = @p2"),
            Case(P1.Or(P2), "c1 = @p1 OR c2 = @p2"),
            Case(P1.Or(P2).Or(P3), "c1 = @p1 OR c2 = @p2 OR c3 = @p3"),
            Case(P1.Or(P2.Or(P3)), "c1 = @p1 OR c2 = @p2 OR c3 = @p3"),

            // AND OR
            Case(P1.And(P2).Or(P3), "c1 = @p1 AND c2 = @p2 OR c3 = @p3"),

            // AND (OR)
            Case(P1.And(P2.Or(P3)), "c1 = @p1 AND (c2 = @p2 OR c3 = @p3)"),

            // (OR) AND
            Case(Sql.Or(P1, P2).And(P3), "(c1 = @p1 OR c2 = @p2) AND c3 = @p3"),
            Case(P1.Or(P2).And(P3), "(c1 = @p1 OR c2 = @p2) AND c3 = @p3"),
            
            // OR (AND)
            Case(P1.Or(P2.And(P3)), "c1 = @p1 OR c2 = @p2 AND c3 = @p3"),

            // (AND) OR
            Case(Sql.And(P1, P2).Or(P3), "c1 = @p1 AND c2 = @p2 OR c3 = @p3"),

            // (OR) AND (OR)
            Case(Sql.Or(P1, P2).And(P3.Or(P4)), "(c1 = @p1 OR c2 = @p2) AND (c3 = @p3 OR c4 = @p4)"),
            Case(Sql.And(P1.Or(P2), P3.Or(P4)), "(c1 = @p1 OR c2 = @p2) AND (c3 = @p3 OR c4 = @p4)"),

            // (AND) OR (AND)
            Case(Sql.And(P1, P2).Or(P3.And(P4)), "c1 = @p1 AND c2 = @p2 OR c3 = @p3 AND c4 = @p4"),
            Case(Sql.Or(P1.And(P2), P3.And(P4)), "c1 = @p1 AND c2 = @p2 OR c3 = @p3 AND c4 = @p4")
        };

        private static object[] Case(PredicateExpression expression, string expected, [CallerLineNumber] int lineNo = 0) => new object[]
        {
            $"line #{lineNo}", expression, expected
        };

        private static PredicateExpression Create(string name, int value)
        {
            var column = new ColumnExpression<int>(name);
            return column == value;
        }
    }
}