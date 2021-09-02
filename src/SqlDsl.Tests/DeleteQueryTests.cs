using NUnit.Framework;

namespace SqlDsl.Tests
{
    [TestFixture]
    public class DeleteQueryTests
    {
        [Test]
        public void Delete_from_table()
        {
            var u = new UsersTable();

            DeleteQuery query = Sql
                .Delete(u);

            query.ShouldBe("DELETE FROM users");
        }

        [Test]
        public void Delete_from_table_alias()
        {
            var u = new UsersTable("u");

            DeleteQuery query = Sql
                .Delete(u);

            query.ShouldBe("DELETE u FROM users u");
        }

        [Test]
        public void Delete_from_table_where()
        {
            var u = new UsersTable();

            DeleteQuery query = Sql
                .Delete(u)
                .Where(u.Id == 0);

            query.ShouldBe("DELETE FROM users WHERE id = @p1");
        }

        [Test]
        public void Delete_from_table_alias_where()
        {
            var u = new UsersTable("u");

            DeleteQuery query = Sql
                .Delete(u)
                .Where(u.Id == 0);

            query.ShouldBe("DELETE u FROM users u WHERE u.id = @p1");
        }
        [Test]
        public void Delete_from_table_alias_join()
        {
            var a = new AuthorsTable("a");
            var b = new BooksTable("b");

            DeleteQuery query = Sql
                .Delete(b)
                .Join(a, a.Id == b.AuthorId);

            query.ShouldBe("DELETE b FROM books b JOIN authors a ON a.id = b.author_id");
        }

        [Test]
        public void Delete_from_table_alias_where_exists()
        {
            var a = new AuthorsTable("a");
            var b = new BooksTable("b");

            DeleteQuery query = Sql
                .Delete(b)
                .WhereExists(Sql.Select().From(a).Where(a.Id == b.AuthorId));

            query.ShouldBe("DELETE b FROM books b WHERE EXISTS (SELECT * FROM authors a WHERE a.id = b.author_id)");
        }

        [Test]
        public void Delete_from_table_alias_where_not_exists()
        {
            var a = new AuthorsTable("a");
            var b = new BooksTable("b");

            DeleteQuery query = Sql
                .Delete(b)
                .WhereNotExists(Sql.Select().From(a).Where(a.Id == b.AuthorId));

            query.ShouldBe("DELETE b FROM books b WHERE NOT EXISTS (SELECT * FROM authors a WHERE a.id = b.author_id)");
        }

        [Test]
        public void Delete_from_table_alias_left_join_where()
        {
            var a = new AuthorsTable("a");
            var b = new BooksTable("b");

            DeleteQuery query = Sql
                .Delete(b)
                .Join(a, a.Id == b.AuthorId)
                .Where(a.Name == "");

            query.ShouldBe("DELETE b FROM books b JOIN authors a ON a.id = b.author_id WHERE a.name = @p1");
        }
    }
}