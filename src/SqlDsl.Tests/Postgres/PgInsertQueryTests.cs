using NUnit.Framework;
using SqlDsl.Postgres;

namespace SqlDsl.Tests.Postgres
{
    [TestFixture]
    public class PgInsertQueryTests
    {
        [Test]
        public void Insert_into_table()
        {
            var u = new UsersTable();

            PgInsertQuery query = PgSql
                .Insert(u)
                .Values(u.Name, "name")
                .Values(u.Age, 10);

            query.ShouldBe("INSERT INTO users (name, age) VALUES (@p1, @p2)");
        }

        [Test]
        public void Insert_into_table_with_returning()
        {
            var u = new UsersTable();

            PgInsertQuery query = PgSql
                .Insert(u)
                .Values(u.Name, "name")
                .Values(u.Age, 10)
                .Returning();

            query.ShouldBe("INSERT INTO users (name, age) VALUES (@p1, @p2) RETURNING *");
        }

        [Test]
        public void Insert_into_table_with_returning_columns()
        {
            var u = new UsersTable();

            PgInsertQuery query = PgSql
                .Insert(u)
                .Values(u.Name, "name")
                .Values(u.Age, 10)
                .Returning(u.Id, u.Name);

            query.ShouldBe("INSERT INTO users (name, age) VALUES (@p1, @p2) RETURNING users.id, users.name");
        }

        [Test]
        public void Insert_into_table_alias_with_returning_columns()
        {
            var u = new UsersTable("u");

            PgInsertQuery query = PgSql
                .Insert(u)
                .Values(u.Name, "name")
                .Values(u.Age, 10)
                .Returning(u.Id, u.Name);

            query.ShouldBe("INSERT INTO users AS u (name, age) VALUES (@p1, @p2) RETURNING u.id, u.name");
        }
    }
}