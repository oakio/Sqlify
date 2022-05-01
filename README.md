# Sqlify
[![Build status](https://ci.appveyor.com/api/projects/status/8viaaqblmh5t1gwv?svg=true)](https://ci.appveyor.com/project/oakio/sqlify)
[![Nuget Package](https://badgen.net/nuget/v/sqlify)](https://www.nuget.org/packages/Sqlify)

Fluent SQL builder library.
* Just start build `SQL` query from `Sql` or `PgSql` classes.
* Use [`Sqlify.Dapper`](https://www.nuget.org/packages/Sqlify.Dapper/) library allowing `Sqlify` and `Dapper` to be used together.

## Features:
* `SELECT`, `DELETE`, `INSERT`, `UPDATE` queries
* `WHERE`, `JOIN`, `ORDER BY`, `GROUP BY`, `HAVING BY` clauses
* `LIKE`, `EXISTS`, `IN`, `BETWEEN` predicates
* `COUNT`, `SUM`, `MAX`, `MIN`, `AVG`, `CAST` functions
* `UNION` queries
* Multiple queries
* Table and Column aliases
* SQL injections free
* Partial `PostgreSQL` dialect support
* Strongly typed (checked at compile time)
* GC friendly

# Getting started
```csharp
// Create model for table Users with columns: Id, Name
public interface IUsersTable : ITable
{
    Column<int> Id { get; }
    Column<string> Name { get; }
}

var u = Sql.Table<IUsersTable>();

// INSERT INTO Users (Id, Name) VALUES (@p1, @p2)
var insertQuery = Sql
    .Insert(u)
    .Values(u.Id, 1)
    .Values(u.Name, "Alex");

// SELECT * FRO Users
var selectQuery = Sql
    .Select()
    .From(b);

// UPDATE Users SET Name = @p1 WHERE Users.Id = @p2
var updateQuery = Sql
    .Update(u)
    .Set(u.Name, "John")
    .Where(u.Id == 1);

// DELETE FROM Users WHERE Users.Id = @p1
var deleteQuery = Sql
    .Delete(u)
    .Where(u.Id == 1);
```

# Examples
* [Schema definition](#schema-definition)
* `SELECT` query
    * [Aliases](#aliases)
    * [Functions](#functions)
    * [`DISTINCT`](#distinct)
    * `WHERE` clause
        * [Predicates](#predicates) (`AND`, `OR`, `IS NULL` and others)
        * [`LIKE`](#like-predicate) predicate
        * [`IN`](#in-predicate) predicate
        * [`EXISTS`](#exists-predicate) predicate
        * [`BETWEEN`](#between-predicate) predicate
    * [`JOIN ON`](#join-on-clause) clause
    * [`ORDER BY`](#order-by-clause) clause
    * [`GROUP BY`](#group-by-clause) clause
    * [`HAVING`](#having-clause) clause
    * [Multiple](#multiple-queries) queries
* [`DELETE`](#delete-query) query
* [`INSERT`](#insert-query) query
* [`UPDATE`](#update-query) query

* `PostreSQL` dialect
    * [`OFFSET` and `LIMIT`](#postgresql-offset-and-limit-clauses) clauses
    * [`UPDATE RETURNING`](#postgresql-update-returning-clause) clause
    * [`INSERT RETURNING`](#postgresql-insert-returning-clause) clause
    * [`DELETE RETURNING`](#postgresql-delete-returning-clause) clause
    * [`INSERT ON CONFLICT DO`](#postgresql-insert-on-conflict-do-clause) clause
    * [`SELECT FOR`](#postgresql-select-for-clause) clause

## Schema definition
As an example, consider the following database schema (`authors` and `books` tables with one-to-many relationship):
```sql
CREATE TABLE authors (
    id integer PRIMARY KEY,
    name varchar(64)
)

CREATE TABLE books (
    id integer PRIMARY KEY,
    name varchar(512),
    author_id integer REFERENCES authors (id), -- one-to-many relationship
    rating real,
    qty integer
)
```

For these tables create corresponding interfaces:
```csharp
[Table("authors")]
public interface IAuthorsTable : ITable
{
    [Column("id")]
    Column<int> Id { get; }

    [Column("name")]
    Column<string> Name { get; }
}

[Table("books")]
public interface IBooksTable : ITable
{
    [Column("id")]
    Column<int> Id { get; }

    [Column("name")]
    Column<string> Name { get; }

    [Column("author_id")]
    Column<int> AuthorId { get; }

    [Column("rating")]
    Column<double> Rating { get; }

    [Column("qty")]
    Column<int> Quantity { get; }
}
```
If the names of the columns in the database are the same as the names of the properties in models, then using `TableAttribute` and `ColumnAttrubute` are optional. 

For example, for schema:
```sql
CREATE TABLE Authors (
    Id integer PRIMARY KEY,
    Name varchar(64),
    BooksCount integer
)
```
you can define table like:
```csharp
public interface IAuthorsTable : ITable
{
    Column<int> Id { get; }

    Column<int> Name { get; }

    Column<int> BooksCount { get; }
}
```
[up &#8593;](#examples)
## Aliases
```csharp
var b = Sql.Table<IBooksTable>();
var query = Sql
    .Select(b.Id, b.Name)
    .From(b);

// SELECT books.id, books.name FROM books
```
```csharp
var b = Sql.Table<IBooksTable>("t"); // table alias
var query = Sql
    .Select(b.Id, b.Name)
    .From(b);

// SELECT t.id, t.name FROM books t
```
```csharp
var b = Sql.Table<IBooksTable>("t");
var query = Sql
    .Select(b.Id, b.Name.As("author_name")) // column alias
    .From(b);

// SELECT t.id, t.name AS author_name FROM books t
```
[up &#8593;](#examples)
## Functions
```csharp
var b = Sql.Table<IBooksTable>();
var query = Sql
    .Select(Sql.Count())
    .From(b);

// SELECT COUNT(*) FROM books
```
```csharp
var b = Sql.Table<IBooksTable>("b");
var query = Sql
    .Select(Sql.Avg(b.Rating))
    .From(b);

// SELECT AVG(b.rating) FROM books b
```
```csharp
var b = Sql.Table<IBooksTable>("b");
var query = Sql
    .Select(b.Rating.Cast<int>("INTEGER"))
    .From(b);

// SELECT CAST(b.rating AS INTEGER) FROM books b
```
[up &#8593;](#examples)
## DISTINCT
```csharp
var a = Sql.Table<IAuthorsTable>("a");
var query = Sql
    .Select(a.Name)
    .Distinct()
    .From(a);

// SELECT DISTINCT a.name FROM authors a
```
[up &#8593;](#examples)
## Predicates
```csharp
var b = Sql.Table<IBooksTable>("b");
var query = Sql
    .Select()
    .From(b)
    .Where(b.Name.IsNull.And(b.Rating <= 0));

// SELECT * FROM books b WHERE b.name IS NULL AND b.rating <= @p1
```
```csharp
var b = Sql.Table<IBooksTable>("b");
var query = Sql
    .Select()
    .From(b)
    .Where(
        b.Name.IsNull, 
        b.Rating <= 0
    );

// SELECT * FROM books b WHERE b.name IS NULL AND b.rating <= @p1
```
[up &#8593;](#examples)
## LIKE predicate
```csharp
var a = Sql.Table<IAuthorsTable>("a");
var query = Sql
    .Select()
    .From(a)
    .Where(a.Name.Like("A%")); // started with 'A'

// SELECT * FROM authors a WHERE a.name LIKE @p1
```
[up &#8593;](#examples)
## IN predicate
```csharp
var a = Sql.Table<IAuthorsTable>("a");
var query = Sql
    .Select()
    .From(a)
    .Where(a.Id.In(new[] {1, 2})); // where id==1 OR id==2

// SELECT * FROM authors a WHERE a.id IN @p1
```
```csharp
var a = Sql.Table<IAuthorsTable>("a");
var b = Sql.Table<IBooksTable>("b");

var subQuery = Sql
    .Select(b.AuthorId)
    .From(b)
    .Where(b.Rating > 3);

var query = Sql
    .Select()
    .From(a)
    .Where(a.Id.In(subQuery)); // IN sub-query
                   
// SELECT * FROM authors a WHERE a.id IN (SELECT b.author_id FROM books b WHERE b.rating > @p1)");
```
[up &#8593;](#examples)
## EXISTS predicate
```csharp
var a = Sql.Table<IAuthorsTable>("a");
var b = Sql.Table<IBooksTable>("b");

var subQuery = Sql
    .Select()
    .From(b)
    .Where((a.Id == b.AuthorId).And(b.Rating > 3));

var query = Sql
    .Select()
    .From(a)
    .WhereExists(subQuery);

// SELECT * FROM authors a WHERE EXISTS (SELECT * FROM books b WHERE a.id = b.author_id AND b.rating > @p1
```
[up &#8593;](#examples)
## BETWEEN predicate
```csharp
var b = Sql.Table<IBooksTable>("b");
var query = Sql
    .Select()
    .From(b)
    .Where(b.Rating.Between(2, 4));

// SELECT * FROM books b WHERE b.rating BETWEEN @p1 AND @p2
```
[up &#8593;](#examples)
## JOIN ON clause
```csharp
var a = Sql.Table<IAuthorsTable>("a");
var b = Sql.Table<IBooksTable>("b");
var query = Sql
    .Select()
    .Join(b, a.Id == b.AuthorId) // also LEFT, RIGHT, FULL JOIN
    .From(a);

// SELECT * FROM authors a JOIN books b ON a.id = b.author_id
```
[up &#8593;](#examples)
## ORDER BY clause
```csharp
var b = Sql.Table<IBooksTable>("b");
var query = Sql
    .Select()
    .OrderByDesc(b.Rating)
    .From(b);

// SELECT * FROM books b ORDER BY b.rating DESC
```
[up &#8593;](#examples)
## GROUP BY clause
```csharp
var b = Sql.Table<IBooksTable>("b");
var query = Sql
    .Select(b.AuthorId, Sql.Count())
    .GroupBy(b.AuthorId)
    .From(b);

// SELECT b.author_id, COUNT(*) FROM books b GROUP BY b.author_id
```
[up &#8593;](#examples)
## Multiple queries
```csharp
var a = Sql.Table<IAuthorsTable>();
var b = Sql.Table<IBooksTable>();
MultipleQuery query = Sql
    .Multiple(
        Sql.Select().From(a),
        Sql.Select().From(b)
    );
    
// SELECT * FROM authors; SELECT * FROM books        
```
[up &#8593;](#examples)
## HAVING clause
```csharp
var b = Sql.Table<IBooksTable>("b");
var query = Sql
    .Select(b.AuthorId, Sql.Count())
    .GroupBy(b.AuthorId)
    .Having(Sql.Count() > 3)
    .From(b);

// SELECT b.author_id, COUNT(*) FROM books b GROUP BY b.author_id HAVING COUNT(*) > @p1
```
[up &#8593;](#examples)
## DELETE query
```csharp
var b = Sql.Table<IBooksTable>();
var query = Sql
    .Delete(b)
    .Where(b.Id == 1);

// DELETE FROM books WHERE books.id = @p1
```
[up &#8593;](#examples)
## INSERT query
```csharp
var a = Sql.Table<IAuthorsTable>();
var query = Sql
    .Insert(a)
    .Values(a.Id, 1)
    .Values(a.Name, "Adam");
                
// INSERT INTO authors (id, name) VALUES (@p1, @p2)
```
[up &#8593;](#examples)
## UPDATE query
```csharp
var b = Sql.Table<IBooksTable>();
var query = Sql
    .Update(b)
    .Set(b.Rating, b.Rating + 1)
    .Where(b.AuthorId == 1);

// UPDATE books SET rating = books.rating + @p1 WHERE books.author_id = @p2
```
[up &#8593;](#examples)
## PostgreSQL OFFSET and LIMIT clauses
```csharp
var a = Sql.Table<IAuthorsTable>("a");
PgSelectQuery query = PgSql
    .Select()
    .From(a)
    .OrderBy(a.Name)
    .Offset(5)
    .Limit(10)

// SELECT * FROM authors a ORDER BY a.name OFFSET @p1 LIMIT @p2
```
[up &#8593;](#examples)
## PostgreSQL UPDATE RETURNING clause
```csharp
var b = Sql.Table<IBooksTable>();
PgUpdateQuery query = PgSql
    .Update(b)
    .Set(b.Rating, b.Rating + 1)
    .Returning(b.Id, b.Rating);

// UPDATE books SET rating = books.rating + @p1 RETURNING books.id, books.rating
```
[up &#8593;](#examples)
## PostgreSQL INSERT RETURNING clause
```csharp
var b = Sql.Table<IBooksTable>();
PgInsertQuery query = PgSql
    .Insert(b)
    .Values(b.Name, "name")
    .Returning();

// INSERT INTO books (name) VALUES (@p1) RETURNING *
```
[up &#8593;](#examples)
## PostgreSQL DELETE RETURNING clause
```csharp
var b = Sql.Table<IBooksTable>();
PgInsertQuery query = PgSql
    .Delete(b)
    .Returning();

// DELETE FROM books RETURNING *
```
[up &#8593;](#examples)
## PostgreSQL INSERT ON CONFLICT DO clause
```csharp
var b = Sql.Table<IBooksTable>("b");
PgInsertQuery query = PgSql
    .Insert(b)
    .Values(b.Id, 1)
    .Values(b.Name, "foo bar")
    .Values(b.Quantity, 5)
    .OnConflict(
        PgConflict.Columns(b.Name),
        PgConflict
            .DoUpdate()
            .Set(b.Quantity, b.Quantity + 5)
    );

// INSERT INTO books AS b (id, name, qty) VALUES (@p1, @p2, @p3) 
// ON CONFLICT (b.name) 
// DO UPDATE SET qty = b.qty + @p4"
```
[up &#8593;](#examples)
## PostgreSQL SELECT FOR clause
```csharp
var b = Sql.Table<IBooksTable>("b");
PgSelectQuery query = PgSql
    .Select()
    .From(b)
    .Where(b.Id == 3)
    .For(PgLockMode.Update); // mode: UPDATE, NO KEY UPDATE, SHARE, KEY SHARE

// SELECT * FROM books b WHERE b.id = @p1 FOR UPDATE
```
[up &#8593;](#examples)

# How to build
```bash
# build
dotnet build ./src

# running tests
dotnet test ./src

# pack
dotnet pack ./src -c=release
```