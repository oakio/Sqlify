using System;
using NUnit.Framework;

namespace Sqlify.Tests
{
    [TestFixture]
    public class DeleteQueryTests
    {
        [Test]
        public void Delete_from_table()
        {
            var u = Sql.Table<IUsersTable>();

            DeleteQuery query = Sql
                .Delete(u);

            query.ShouldBe("DELETE FROM users");
        }

        [Test]
        public void Delete_from_table_alias_is_not_supported()
        {
            var u = Sql.Table<IUsersTable>("u");

            Assert.Throws<NotSupportedException>(() => Sql.Delete(u));
        }

        [Test]
        public void Delete_from_table_where()
        {
            var u = Sql.Table<IUsersTable>();

            DeleteQuery query = Sql
                .Delete(u)
                .Where(u.Id == 0);

            query.ShouldBe("DELETE FROM users WHERE users.id = @p1");
        }

        [Test]
        public void Delete_from_table_join()
        {
            var a = Sql.Table<IAuthorsTable>("a");
            var b = Sql.Table<IBooksTable>();

            DeleteQuery query = Sql
                .Delete(b)
                .Join(a, a.Id == b.AuthorId);

            query.ShouldBe("DELETE FROM books JOIN authors a ON a.id = books.author_id");
        }

        [Test]
        public void Delete_from_table_where_exists()
        {
            var a = Sql.Table<IAuthorsTable>("a");
            var b = Sql.Table<IBooksTable>();

            DeleteQuery query = Sql
                .Delete(b)
                .WhereExists(Sql.Select().From(a).Where(a.Id == b.AuthorId));

            query.ShouldBe("DELETE FROM books WHERE EXISTS (SELECT * FROM authors a WHERE a.id = books.author_id)");
        }

        [Test]
        public void Delete_from_table_where_not_exists()
        {
            var a = Sql.Table<IAuthorsTable>("a");
            var b = Sql.Table<IBooksTable>();

            DeleteQuery query = Sql
                .Delete(b)
                .WhereNotExists(Sql.Select().From(a).Where(a.Id == b.AuthorId));

            query.ShouldBe("DELETE FROM books WHERE NOT EXISTS (SELECT * FROM authors a WHERE a.id = books.author_id)");
        }

        [Test]
        public void Delete_from_table_join_where()
        {
            var a = Sql.Table<IAuthorsTable>("a");
            var b = Sql.Table<IBooksTable>();

            DeleteQuery query = Sql
                .Delete(b)
                .Join(a, a.Id == b.AuthorId)
                .Where(a.Name == "");

            query.ShouldBe("DELETE FROM books JOIN authors a ON a.id = books.author_id WHERE a.name = @p1");
        }
    }
}