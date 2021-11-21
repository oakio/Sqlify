using NUnit.Framework;
using SqlDsl.Postgres;
using SqlDsl.Postgres.Clauses;

namespace SqlDsl.Tests.Postgres
{
    [TestFixture]
    public class PgInsertQueryTests
    {
        [Test]
        public void Insert_into_table()
        {
            var u = Sql.Table<IUsersTable>();

            PgInsertQuery query = PgSql
                .Insert(u)
                .Values(u.Name, "name")
                .Values(u.Age, 10);

            query.ShouldBe("INSERT INTO users (name, age) VALUES (@p1, @p2)");
        }

        [Test]
        public void Insert_into_table_with_returning()
        {
            var u = Sql.Table<IUsersTable>();

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
            var u = Sql.Table<IUsersTable>();

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
            var u = Sql.Table<IUsersTable>("u");

            PgInsertQuery query = PgSql
                .Insert(u)
                .Values(u.Name, "name")
                .Values(u.Age, 10)
                .Returning(u.Id, u.Name);

            query.ShouldBe("INSERT INTO users AS u (name, age) VALUES (@p1, @p2) RETURNING u.id, u.name");
        }

        [Test]
        public void Insert_into_table_on_conflict_columns_do_nothing()
        {
            var b = Sql.Table<IBooksTable>();

            PgInsertQuery query = PgSql
                .Insert(b)
                .Values(b.Id, 1)
                .Values(b.Name, "foo")
                .OnConflict(
                    PgConflict.Columns(b.Name),
                    PgConflict.DoNothing
                );

            query.ShouldBe("INSERT INTO books (id, name) VALUES (@p1, @p2) ON CONFLICT (books.name) DO NOTHING");
        }

        [Test]
        public void Insert_into_table_alias_on_conflict_columns_do_nothing()
        {
            var b = Sql.Table<IBooksTable>("b");

            PgInsertQuery query = PgSql
                .Insert(b)
                .Values(b.Id, 1)
                .Values(b.Name, "foo")
                .OnConflict(
                    PgConflict.Columns(b.Name),
                    PgConflict.DoNothing);

            query.ShouldBe("INSERT INTO books AS b (id, name) VALUES (@p1, @p2) ON CONFLICT (b.name) DO NOTHING");
        }

        [Test]
        public void Insert_into_table_on_conflict_columns_do_update()
        {
            var b = Sql.Table<IBooksTable>();

            PgInsertQuery query = PgSql
                .Insert(b)
                .Values(b.Id, 1)
                .Values(b.Name, "foo")
                .Values(b.Quantity, 5)
                .OnConflict(
                    PgConflict.Columns(b.Name),
                    PgConflict
                        .DoUpdate()
                        .Set(b.Quantity, b.Quantity + 5)
                );

            query.ShouldBe("INSERT INTO books (id, name, qty) VALUES (@p1, @p2, @p3) ON CONFLICT (books.name) DO UPDATE SET qty = books.qty + @p4");
        }

        [Test]
        public void Insert_into_table_alias_on_conflict_columns_do_update()
        {
            var b = Sql.Table<IBooksTable>("b");

            PgInsertQuery query = PgSql
                .Insert(b)
                .Values(b.Id, 1)
                .Values(b.Name, "foo")
                .Values(b.Quantity, 5)
                .OnConflict(
                    PgConflict.Columns(b.Name),
                    PgConflict
                        .DoUpdate()
                        .Set(b.Quantity, b.Quantity + 5)
                );

            query.ShouldBe("INSERT INTO books AS b (id, name, qty) VALUES (@p1, @p2, @p3) ON CONFLICT (b.name) DO UPDATE SET qty = b.qty + @p4");
        }
    }
}