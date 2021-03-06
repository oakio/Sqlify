using System;
using NUnit.Framework;

namespace Sqlify.Tests
{
    [TestFixture]
    public class UpdateQueryTests
    {
        [Test]
        public void Update_table_set_column_value()
        {
            var u = Sql.Table<IUsersTable>();

            UpdateQuery query = Sql
                .Update(u)
                .Set(u.Age, 20);

            query.ShouldBe("UPDATE users SET age = @p1");
        }

        [Test]
        public void Update_table_set_column_expression1()
        {
            var u = Sql.Table<IUsersTable>();

            UpdateQuery query = Sql
                .Update(u)
                .Set(u.Age, u.Age + 1);

            query.ShouldBe("UPDATE users SET age = users.age + @p1");
        }

        [Test]
        public void Update_table_set_column_expression2()
        {
            var u = Sql.Table<IUsersTable>();

            UpdateQuery query = Sql
                .Update(u)
                .Set(u.Age, u.Age);

            query.ShouldBe("UPDATE users SET age = users.age");
        }

        [Test]
        public void Update_table_set_column_expression3()
        {
            var u = Sql.Table<IUsersTable>();

            UpdateQuery query = Sql
                .Update(u)
                .Set(u.Age, u.Age + u.Age);

            query.ShouldBe("UPDATE users SET age = users.age + users.age");
        }

        [Test]
        public void Update_table_where()
        {
            var u = Sql.Table<IUsersTable>();

            UpdateQuery query = Sql
                .Update(u)
                .Set(u.Age, 10)
                .Where(u.Age == 10);

            query.ShouldBe("UPDATE users SET age = @p1 WHERE users.age = @p2");
        }

        [Test]
        public void Update_table_alias_is_not_supported()
        {
            var u = Sql.Table<IUsersTable>("u");

            Assert.Throws<NotSupportedException>(() => Sql.Update(u));
        }
    }
}