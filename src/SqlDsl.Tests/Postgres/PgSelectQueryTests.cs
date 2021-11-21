using NUnit.Framework;
using SqlDsl.Postgres;

namespace SqlDsl.Tests.Postgres
{
    [TestFixture]
    public class PgSelectQueryTests
    {
        [Test]
        public void Select_from_table_with_limit()
        {
            var u = Sql.Table<IUsersTable>();

            PgSelectQuery query = PgSql
                .Select()
                .From(u)
                .OrderBy(u.Name)
                .Limit(10);

            query.ShouldBe("SELECT * FROM users ORDER BY users.name LIMIT @p1");
        }

        [Test]
        public void Select_from_table_with_offset()
        {
            var u = Sql.Table<IUsersTable>();

            PgSelectQuery query = PgSql
                .Select()
                .From(u)
                .OrderBy(u.Name)
                .Offset(10);

            query.ShouldBe("SELECT * FROM users ORDER BY users.name OFFSET @p1");
        }

        [Test]
        public void Select_from_table_with_offset_and_limit()
        {
            var u = Sql.Table<IUsersTable>();

            PgSelectQuery query = PgSql
                .Select()
                .From(u)
                .OrderBy(u.Name)
                .Offset(5)
                .Limit(10);

            query.ShouldBe("SELECT * FROM users ORDER BY users.name OFFSET @p1 LIMIT @p2");
        }

        [Test]
        public void Select_from_table_with_for_update_mode()
        {
            var u = Sql.Table<IUsersTable>();

            PgSelectQuery query = PgSql
                .Select()
                .From(u)
                .Where(u.Id == 7)
                .For(PgLockMode.Update);

            query.ShouldBe("SELECT * FROM users WHERE users.id = @p1 FOR UPDATE");
        }
    }
}