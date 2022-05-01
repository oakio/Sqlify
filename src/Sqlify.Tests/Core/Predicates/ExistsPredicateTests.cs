using NUnit.Framework;
using Sqlify.Core.Predicates;

namespace Sqlify.Tests.Core.Predicates
{
    [TestFixture]
    public class ExistsPredicateTests
    {
        [Test]
        public void Test()
        {
            var u = Sql.Table<IUsersTable>();
            SelectQuery query = Sql.Select().From(u);

            var expression = new ExistsPredicate<SelectQuery>(query);

            expression.ShouldBe("EXISTS (SELECT * FROM users)");
        }
    }
}