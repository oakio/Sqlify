using System;
using NUnit.Framework;

namespace Sqlify.Tests
{
    [TestFixture]
    public class InsertQueryTests
    {
        [Test]
        public void Insert_into_table()
        {
            var u = Sql.Table<IUsersTable>();

            InsertQuery query = Sql
                .Insert(u)
                .Values(u.Name, "name")
                .Values(u.Age, 10);

            query.ShouldBe("INSERT INTO users (name, age) VALUES (@p1, @p2)");
        }

        [Test]
        public void Insert_into_table_alias_is_not_supported()
        {
            var u = Sql.Table<IUsersTable>("u");

            Assert.Throws<NotSupportedException>(() => Sql.Insert(u));
        }
    }
}