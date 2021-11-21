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
            var u = Sql.Table<IUsersTable>();
            SelectQuery query = Sql.Select().From(u);

            var expression = new ExistsExpression<SelectQuery>(query);

            expression.ShouldBe("EXISTS (SELECT * FROM users)");
        }
    }
}