using NUnit.Framework;

namespace SqlDsl.Tests
{
    [TestFixture]
    public class InsertQueryTests
    {
        [Test]
        public void Insert_into_table()
        {
            var u = new UsersTable();

            InsertQuery query = Sql
                .Insert(u)
                .Values(u.Name, "name")
                .Values(u.Age, 10);

            query.ShouldBe("INSERT INTO users (name, age) VALUES (@p1, @p2)");
        }
    }
}