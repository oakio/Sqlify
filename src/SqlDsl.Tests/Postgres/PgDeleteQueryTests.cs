using NUnit.Framework;
using SqlDsl.Postgres;

namespace SqlDsl.Tests.Postgres
{
    [TestFixture]
    public class PgDeleteQueryTests
    {
        [Test]
        public void Delete_from_table_where()
        {
            var b = Sql.Table<IBooksTable>();

            PgDeleteQuery query = PgSql
                .Delete(b)
                .Where(b.Id == 1);

            query.ShouldBe("DELETE FROM books WHERE books.id = @p1");
        }

        [Test]
        public void Delete_from_table_alias_where()
        {
            var b = Sql.Table<IBooksTable>("b");

            PgDeleteQuery query = PgSql
                .Delete(b)
                .Where(b.Id == 1);

            query.ShouldBe("DELETE FROM books b WHERE b.id = @p1");
        }

        [Test]
        public void Delete_from_table_with_returning()
        {
            var u = Sql.Table<IUsersTable>();

            PgDeleteQuery query = PgSql
                .Delete(u)
                .Returning();

            query.ShouldBe("DELETE FROM users RETURNING *");
        }

        [Test]
        public void Delete_from_table_with_returning_columns()
        {
            var u = Sql.Table<IUsersTable>();

            PgDeleteQuery query = PgSql
                .Delete(u)
                .Returning(u.Id, u.Name);

            query.ShouldBe("DELETE FROM users RETURNING users.id, users.name");
        }

        [Test]
        public void Delete_from_table_alias_with_returning_columns()
        {
            var u = Sql.Table<IUsersTable>("u");

            PgDeleteQuery query = PgSql
                .Delete(u)
                .Returning(u.Id, u.Name);

            query.ShouldBe("DELETE FROM users u RETURNING u.id, u.name");
        }
    }
}