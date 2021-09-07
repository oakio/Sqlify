using NUnit.Framework;
using SqlDsl.Postgres;

namespace SqlDsl.Tests.Postgres
{
    [TestFixture]
    public class PgUpdateQueryTests
    {
        [Test]
        public void Update_table_set_column_value()
        {
            var u = new UsersTable();

            PgUpdateQuery query = PgSql
                .Update(u)
                .Set(u.Age, 20);

            query.ShouldBe("UPDATE users SET age = @p1");
        }

        [Test]
        public void Update_table_set_column_value_with_returning()
        {
            var u = new UsersTable();

            PgUpdateQuery query = PgSql
                .Update(u)
                .Set(u.Age, 20)
                .Returning();

            query.ShouldBe("UPDATE users SET age = @p1 RETURNING *");
        }

        [Test]
        public void Update_table_set_column_value_where_with_returning_columns()
        {
            var u = new UsersTable();

            PgUpdateQuery query = PgSql
                .Update(u)
                .Set(u.Age, u.Age + 1)
                .Where(u.Id == 1)
                .Returning(u.Id, u.Age);

            query.ShouldBe("UPDATE users SET age = users.age + @p1 WHERE users.id = @p2 RETURNING users.id, users.age");
        }

        [Test]
        public void Update_table_alias_set_column_value_where_with_returning_columns()
        {
            var u = new UsersTable("u");

            PgUpdateQuery query = PgSql
                .Update(u)
                .Set(u.Age, u.Age + 1)
                .Where(u.Id == 1)
                .Returning(u.Id, u.Age);

            query.ShouldBe("UPDATE users u SET age = u.age + @p1 WHERE u.id = @p2 RETURNING u.id, u.age");
        }
    }
}