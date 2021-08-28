using NUnit.Framework;
using SqlDsl.Core.Predicates;

namespace SqlDsl.Tests.Core.Predicates
{
    [TestFixture]
    public class ExistsExpressionTests
    {
        [Test]
        public void Test()
        {
            var u = new UsersTable();
            SelectSqlQuery query = Sql.Select().From(u);

            var expression = new ExistsExpression<SelectSqlQuery>(query);

            expression.ShouldBe("EXISTS (SELECT * FROM users)");
        }
    }
}