using NUnit.Framework;
using SqlDsl.Core;

namespace SqlDsl.Tests
{
    public class SelectSqlQueryTests
    {
        [Test]
        public void Select_from_table()
        {
            var u = new UsersTable();

            SelectSqlQuery query = Sql
                .Select()
                .From(u);

            query.ShouldBe("SELECT * FROM users");
        }

        [Test]
        public void Select_from_table_alias()
        {
            var u = new UsersTable("u");

            SelectSqlQuery query = Sql
                .Select()
                .From(u);

            query.ShouldBe("SELECT * FROM users u");
        }

        [Test]
        public void Select_column_from_table()
        {
            var u = new UsersTable();

            SelectSqlQuery<string> query = Sql
                .Select(u.Name)
                .From(u);

            query.ShouldBe("SELECT name FROM users");
        }

        [Test]
        public void Select_column_from_table_alias()
        {
            var u = new UsersTable("u");

            SelectSqlQuery<string> query = Sql
                .Select(u.Name)
                .From(u);

            query.ShouldBe("SELECT u.name FROM users u");
        }

        [Test]
        public void Select_distinct_column_from_table()
        {
            var u = new UsersTable();

            SelectSqlQuery<string> query = Sql
                .Select(u.Name)
                .Distinct()
                .From(u);

            query.ShouldBe("SELECT DISTINCT name FROM users");
        }

        [Test]
        public void Select_distinct_column_from_table_alias()
        {
            var u = new UsersTable("u");

            SelectSqlQuery<string> query = Sql
                .Select(u.Name)
                .Distinct()
                .From(u);

            query.ShouldBe("SELECT DISTINCT u.name FROM users u");
        }

        [Test]
        public void Select_MAX_from_table()
        {
            var u = new UsersTable();

            SelectSqlQuery<int> query = Sql
                .Select(Sql.Max(u.Age))
                .From(u);

            query.ShouldBe("SELECT MAX(age) FROM users");
        }

        [Test]
        public void Select_MAX_from_table_alias()
        {
            var u = new UsersTable("u");

            SelectSqlQuery<int> query = Sql
                .Select(Sql.Max(u.Age))
                .From(u);

            query.ShouldBe("SELECT MAX(u.age) FROM users u");
        }

        [Test]
        public void Select_MIN_from_table()
        {
            var u = new UsersTable();

            SelectSqlQuery<int> query = Sql
                .Select(Sql.Min(u.Age))
                .From(u);

            query.ShouldBe("SELECT MIN(age) FROM users");
        }

        [Test]
        public void Select_MIN_from_table_alias()
        {
            var u = new UsersTable("u");

            SelectSqlQuery<int> query = Sql
                .Select(Sql.Min(u.Age))
                .From(u);

            query.ShouldBe("SELECT MIN(u.age) FROM users u");
        }

        [Test]
        public void Select_AVG_from_table()
        {
            var u = new UsersTable();

            SelectSqlQuery<int> query = Sql
                .Select(Sql.Avg(u.Age))
                .From(u);

            query.ShouldBe("SELECT AVG(age) FROM users");
        }

        [Test]
        public void Select_AVG_from_table_alias()
        {
            var u = new UsersTable("u");

            SelectSqlQuery<int> query = Sql
                .Select(Sql.Avg(u.Age))
                .From(u);

            query.ShouldBe("SELECT AVG(u.age) FROM users u");
        }

        [Test]
        public void Select_SUM_from_table()
        {
            var u = new UsersTable();

            SelectSqlQuery<int> query = Sql
                .Select(Sql.Sum(u.Age))
                .From(u);

            query.ShouldBe("SELECT SUM(age) FROM users");
        }

        [Test]
        public void Select_SUM_from_table_alias()
        {
            var u = new UsersTable("u");

            SelectSqlQuery<int> query = Sql
                .Select(Sql.Sum(u.Age))
                .From(u);

            query.ShouldBe("SELECT SUM(u.age) FROM users u");
        }

        [Test]
        public void Select_from_table_order_by()
        {
            var u = new UsersTable();

            SelectSqlQuery query = Sql
                .Select()
                .From(u)
                .OrderBy(u.Name)
                .OrderByDesc(u.Age);

            query.ShouldBe("SELECT * FROM users ORDER BY name, age DESC");
        }

        [Test]
        public void Select_from_table_alias_order_by()
        {
            var u = new UsersTable("u");

            SelectSqlQuery query = Sql
                .Select()
                .From(u)
                .OrderBy(u.Name)
                .OrderByDesc(u.Age);

            query.ShouldBe("SELECT * FROM users u ORDER BY u.name, u.age DESC");
        }

        [Test]
        public void Select_from_table_group_by()
        {
            var u = new UsersTable();

            SelectSqlQuery query = Sql
                .Select()
                .From(u)
                .GroupBy(u.Name);

            query.ShouldBe("SELECT * FROM users GROUP BY name");
        }

        [Test]
        public void Select_from_table_alias_group_by()
        {
            var u = new UsersTable("u");

            SelectSqlQuery query = Sql
                .Select()
                .From(u)
                .GroupBy(u.Name);

            query.ShouldBe("SELECT * FROM users u GROUP BY u.name");
        }

        [Test]
        public void Select_column_COUNT_group_by_order_by_COUNT()
        {
            var u = new UsersTable();

            SelectSqlQuery query = Sql
                .Select(u.Name, Sql.Count())
                .From(u)
                .GroupBy(u.Name)
                .OrderByDesc(Sql.Count());

            query.ShouldBe("SELECT name, COUNT(*) FROM users GROUP BY name ORDER BY COUNT(*) DESC");
        }

        [Test]
        public void Select_from_table_where()
        {
            var u = new UsersTable();

            SelectSqlQuery query = Sql
                .Select()
                .From(u)
                .Where((u.Age > 30).And(u.Name == "John"));

            query.ShouldBe("SELECT * FROM users WHERE age > @p1 AND name = @p2");
        }

        [Test]
        public void Select_from_table_alias_where()
        {
            var u = new UsersTable("u");

            SelectSqlQuery query = Sql
                .Select()
                .From(u)
                .Where((u.Age > 30).And(u.Name == "John"));

            query.ShouldBe("SELECT * FROM users u WHERE u.age > @p1 AND u.name = @p2");
        }

        [Test]
        public void Select_from_table_where_column_in()
        {
            var u = new UsersTable();

            SelectSqlQuery query = Sql
                .Select()
                .From(u)
                .Where(u.Name.In(new[] {"John", "Adam"}));

            query.ShouldBe("SELECT * FROM users WHERE name IN @p1");
        }

        [Test]
        public void Select_from_table_where_column_not_in()
        {
            var u = new UsersTable();

            SelectSqlQuery query = Sql
                .Select()
                .From(u)
                .Where(u.Name.NotIn(new[] { "John", "Adam" }));

            query.ShouldBe("SELECT * FROM users WHERE name NOT IN @p1");
        }

        [Test]
        public void Select_from_table_where_column_between()
        {
            var u = new UsersTable();

            SelectSqlQuery query = Sql
                .Select()
                .From(u)
                .Where(u.Age.Between(30, 50));

            query.ShouldBe("SELECT * FROM users WHERE age BETWEEN @p1 AND @p2");
        }

        [Test]
        public void Select_from_table_where_column_like()
        {
            var u = new UsersTable();

            SelectSqlQuery query = Sql
                .Select()
                .From(u)
                .Where(u.Name.Like("A%"));

            query.ShouldBe("SELECT * FROM users WHERE name LIKE @p1");
        }

        [Test]
        public void Select_from_table_left_join_table_on()
        {
            var a = new AuthorsTable("a");
            var b = new BooksTable("b");

            SelectSqlQuery query = Sql
                .Select()
                .From(a)
                .LeftJoin(b, b.AuthorId == a.Id);

            query.ShouldBe("SELECT * FROM authors a LEFT JOIN books b ON b.author_id = a.id");
        }

        [Test]
        public void Select_from_table_right_join_table_on()
        {
            var a = new AuthorsTable("a");
            var b = new BooksTable("b");

            SelectSqlQuery query = Sql
                .Select()
                .From(a)
                .RightJoin(b, b.AuthorId == a.Id);

            query.ShouldBe("SELECT * FROM authors a RIGHT JOIN books b ON b.author_id = a.id");
        }

        [Test]
        public void Select_from_table_join_table_on()
        {
            var a = new AuthorsTable("a");
            var b = new BooksTable("b");

            SelectSqlQuery query = Sql
                .Select()
                .From(a)
                .Join(b, b.AuthorId == a.Id);

            query.ShouldBe("SELECT * FROM authors a JOIN books b ON b.author_id = a.id");
        }

        [Test]
        public void Select_from_table_full_join_table_on()
        {
            var a = new AuthorsTable("a");
            var b = new BooksTable("b");

            SelectSqlQuery query = Sql
                .Select()
                .From(a)
                .FullJoin(b, b.AuthorId == a.Id);

            query.ShouldBe("SELECT * FROM authors a FULL JOIN books b ON b.author_id = a.id");
        }

        [Test]
        public void Select_from_select()
        {
            var u = new UsersTable("u");

            SelectSqlQuery<int> query = Sql
                .Select(u.Id)
                .From(Sql
                        .Select()
                        .From(new UsersTable()),
                    u
                );

            query.ShouldBe("SELECT u.id FROM (SELECT * FROM users) u");
        }

        [Test]
        public void Select_from_table_where_column_in_select()
        {
            var a = new AuthorsTable("a");
            var b = new BooksTable("b");

            SelectSqlQuery query = Sql
                .Select()
                .From(a)
                .Where(a.Id.In(Sql.Select(b.AuthorId).From(b).Where(b.Rating > 3)));

            query.ShouldBe("SELECT * FROM authors a WHERE a.id IN (SELECT b.author_id FROM books b WHERE b.rating > @p1)");
        }

        [Test]
        public void Select_from_table_where_column_not_in_select()
        {
            var a = new AuthorsTable("a");
            var b = new BooksTable("b");

            SelectSqlQuery query = Sql
                .Select()
                .From(a)
                .Where(a.Id.NotIn(Sql.Select(b.AuthorId).From(b).Where(b.Rating > 3)));

            query.ShouldBe("SELECT * FROM authors a WHERE a.id NOT IN (SELECT b.author_id FROM books b WHERE b.rating > @p1)");
        }

        [Test]
        public void Select_from_table_where_exists()
        {
            var a = new AuthorsTable("a");
            var b = new BooksTable("b");

            SelectSqlQuery query = Sql
                .Select()
                .From(a)
                .WhereExists(Sql.Select().From(b).Where((a.Id == b.AuthorId).And(b.Rating > 3)));

            query.ShouldBe("SELECT * FROM authors a WHERE EXISTS (SELECT * FROM books b WHERE a.id = b.author_id AND b.rating > @p1)");
        }

        [Test]
        public void Select_from_table_where_not_exists()
        {
            var a = new AuthorsTable("a");
            var b = new BooksTable("b");

            SelectSqlQuery query = Sql
                .Select()
                .From(a)
                .WhereNotExists(Sql.Select().From(b).Where((a.Id == b.AuthorId).And(b.Rating > 3)));

            query.ShouldBe("SELECT * FROM authors a WHERE NOT EXISTS (SELECT * FROM books b WHERE a.id = b.author_id AND b.rating > @p1)");
        }

        [Test]
        public void Select_from_table_where_is_null()
        {
            var u = new UsersTable();

            SelectSqlQuery query = Sql
                .Select()
                .From(u)
                .Where(u.Name.IsNull);

            query.ShouldBe("SELECT * FROM users WHERE name IS NULL");
        }

        [Test]
        public void Select_from_table_where_is_not_null()
        {
            var u = new UsersTable();

            SelectSqlQuery query = Sql
                .Select()
                .From(u)
                .Where(u.Name.IsNotNull);

            query.ShouldBe("SELECT * FROM users WHERE name IS NOT NULL");
        }

        [Test]
        public void Select_column_alias_from_table()
        {
            var u = new UsersTable();

            SelectSqlQuery<int> query = Sql
                .Select(u.Id.As("UserId"))
                .From(u);

            query.ShouldBe("SELECT id AS UserId FROM users");
        }

        [Test]
        public void Select_column_alias_from_table_alias()
        {
            var u = new UsersTable("u");

            SelectSqlQuery<int> query = Sql
                .Select(u.Id.As("UserId"))
                .From(u);

            query.ShouldBe("SELECT u.id AS UserId FROM users u");
        }

        [Test]
        public void Select_function_alias_from_table()
        {
            var u = new UsersTable();

            SelectSqlQuery<int> query = Sql
                .Select(Sql.Max(u.Age).As("max_age"))
                .From(u);

            query.ShouldBe("SELECT MAX(age) AS max_age FROM users");
        }

        [Test]
        public void Select_function_alias_from_table_alias()
        {
            var u = new UsersTable("u");

            SelectSqlQuery<int> query = Sql
                .Select(Sql.Max(u.Age).As("max_age"))
                .From(u);

            query.ShouldBe("SELECT MAX(u.age) AS max_age FROM users u");
        }

        [Test]
        public void Select_union_select()
        {
            var u = new UsersTable();

            SelectSqlQuery query = Sql
                .Select()
                .From(u)
                .Where(u.Age < 20)
                .Union()
                .Select()
                .From(u)
                .Where(u.Age > 40);

            query.ShouldBe("SELECT * FROM users WHERE age < @p1 UNION SELECT * FROM users WHERE age > @p2");
        }

        [Test]
        public void Select_union_all_select()
        {
            var u = new UsersTable();

            SelectSqlQuery<int> query = Sql
                .Select(u.Id)
                .From(u)
                .Where(u.Age < 20)
                .UnionAll()
                .Select(u.Id)
                .From(u)
                .Where(u.Age > 40);

            query.ShouldBe("SELECT id FROM users WHERE age < @p1 UNION ALL SELECT id FROM users WHERE age > @p2");
        }

        [Test]
        public void Select_column_COUNT_from_table_group_by_having_COUNT()
        {
            var u = new UsersTable();

            SelectSqlQuery query = Sql
                .Select(u.Name, Sql.Count())
                .From(u)
                .GroupBy(u.Name)
                .Having(Sql.Count() > 5);

            query.ShouldBe("SELECT name, COUNT(*) FROM users GROUP BY name HAVING COUNT(*) > @p1");
        }

        [Test]
        public void Select_column_COUNT_from_table_alias_group_by_having_COUNT()
        {
            var u = new UsersTable("u");

            SelectSqlQuery query = Sql
                .Select(u.Name, Sql.Count())
                .From(u)
                .GroupBy(u.Name)
                .Having(Sql.Count() > 5);

            query.ShouldBe("SELECT u.name, COUNT(*) FROM users u GROUP BY u.name HAVING COUNT(*) > @p1");
        }

        [Test]
        public void Select_column_expression_from_table()
        {
            var u = new UsersTable();

            SelectSqlQuery<int> query = Sql
                .Select(u.Age + 10)
                .From(u);

            query.ShouldBe("SELECT age + @p1 FROM users");
        }

        [Test]
        public void Select_column_expression_from_table_alias()
        {
            var u = new UsersTable("u");

            SelectSqlQuery<int> query = Sql
                .Select(u.Age + 10)
                .From(u);

            query.ShouldBe("SELECT u.age + @p1 FROM users u");
        }

        [Test]
        public void Select_function_arg_expression_from_table()
        {
            var u = new UsersTable();

            SelectSqlQuery<int> query = Sql
                .Select(Sql.Sum(u.Age + 1))
                .From(u);

            query.ShouldBe("SELECT SUM(age + @p1) FROM users");
        }

        [Test]
        public void Select_function_arg_expression_from_table_alias()
        {
            var u = new UsersTable("u");

            SelectSqlQuery<int> query = Sql
                .Select(Sql.Sum(u.Age + 1))
                .From(u);

            query.ShouldBe("SELECT SUM(u.age + @p1) FROM users u");
        }

        [Test]
        public void Select_function_expression_from_table()
        {
            var u = new UsersTable();

            SelectSqlQuery<int> query = Sql
                .Select(Sql.Sum(u.Age) + 1)
                .From(u);

            query.ShouldBe("SELECT SUM(age) + @p1 FROM users");
        }

        [Test]
        public void Select_function_expression_from_table_alias()
        {
            var u = new UsersTable("u");

            SelectSqlQuery<int> query = Sql
                .Select(Sql.Sum(u.Age) + 1)
                .From(u);

            query.ShouldBe("SELECT SUM(u.age) + @p1 FROM users u");
        }

        [Test]
        public void Multiple_Select_from_table()
        {
            var a = new AuthorsTable();
            var b = new BooksTable();

            MultipleSqlQuery query = Sql
                .Multiple(
                    Sql.Select().From(a),
                    Sql.Select().From(b)
                );

            query.ShouldBe("SELECT * FROM authors; SELECT * FROM books");
        }
    }
}