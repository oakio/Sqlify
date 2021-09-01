using NUnit.Framework;
using SqlDsl.Postgres;

namespace SqlDsl.Tests.Postgres
{
    [TestFixture]
    public class PgSelectSqlQueryTests
    {
        [Test]
        public void Select_from_table_with_limit()
        {
            var u = new UsersTable();

            PgSelectSqlQuery query = PgSql
                .Select()
                .From(u)
                .OrderBy(u.Name)
                .Limit(10);

            query.ShouldBe("SELECT * FROM users ORDER BY name LIMIT @p1");
        }

        [Test]
        public void Select_from_table_with_offset()
        {
            var u = new UsersTable();

            PgSelectSqlQuery query = PgSql
                .Select()
                .From(u)
                .OrderBy(u.Name)
                .Offset(10);

            query.ShouldBe("SELECT * FROM users ORDER BY name OFFSET @p1");
        }

        [Test]
        public void Select_from_table_with_offset_and_limit()
        {
            var u = new UsersTable();

            PgSelectSqlQuery query = PgSql
                .Select()
                .From(u)
                .OrderBy(u.Name)
                .Offset(5)
                .Limit(10);

            query.ShouldBe("SELECT * FROM users ORDER BY name OFFSET @p1 LIMIT @p2");
        }
    }
}