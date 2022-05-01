using System;
using NUnit.Framework;
using Sqlify.Core;
using Sqlify.Core.CodeGen;
using Sqlify.Core.Expressions;

namespace Sqlify.Tests
{
    [TestFixture]
    public class SqlTableTests
    {
        [Test]
        [TestCase("t", "t.Id", "t.user_name")]
        [TestCase(null, "foo.Id", "foo.user_name")]
        public void Should_set_table_name_from_TableAttribute(string alias, string idColumnName, string userNameColumnName)
        {
            IFooTable table = Sql.Table<IFooTable>(alias);

            AssertTable(table, "foo", alias);

            AssertColumn(table.Id, "Id", idColumnName);
            AssertColumn(table.UserName, "user_name", userNameColumnName);

        }

        [Test]
        [TestCase("t", "t.Id", "t.user_name")]
        [TestCase(null, "Bar.Id", "Bar.user_name")]
        public void Should_set_table_name_based_on_type_name(string alias, string idColumnName, string userNameColumnName)
        {
            IBarTable table = Sql.Table<IBarTable>(alias);

            AssertTable(table, "Bar", alias);

            AssertColumn(table.Id, "Id", idColumnName);
            AssertColumn(table.UserName, "user_name", userNameColumnName);
        }

        [Test]
        public void Should_check_table_type()
        {
            AssertThrowException<IPrivateInterfaceTable>();
            AssertThrowException<IPropertyWithSetterInterfaceTable>();
            AssertThrowException<IUnexpectedColumnTypeInterfaceTable>();
            AssertThrowException<IUnexpectedMethodsInterfaceTable>();
            AssertThrowException<UnexpectedTypeTable>();
        }

        private static void AssertTable(object table, string name, string alias)
        {
            var tableBase = (TableBase)table;

            Assert.That(table, Is.Not.Null);
            Assert.That(tableBase.GetName(), Is.EqualTo(name));
            Assert.That(tableBase.GetAlias(), Is.EqualTo(alias));
        }

        private static void AssertThrowException<T>() where T : ITable => Assert.Throws<InvalidOperationException>(() => Sql.Table<T>());

        private static void AssertColumn<T>(Column<T> column, string unqualifiedName, string name)
        {
            Assert.That(column, Is.Not.Null);
            Assert.That(column.UnqualifiedName, Is.EqualTo(unqualifiedName));
            Assert.That(column.Name, Is.EqualTo(name));
        }

        private interface IPrivateInterfaceTable : ITable
        {
        }

        public interface IPropertyWithSetterInterfaceTable : ITable
        {
            Column<int> Id { get; set; }
        }

        public interface IUnexpectedColumnTypeInterfaceTable : ITable
        {
            int Id { get; }
        }

        public interface IUnexpectedMethodsInterfaceTable : ITable
        {
            void Foo();
        }

        public class UnexpectedTypeTable : ITable
        {
            public string GetName() => "table";

            public string GetAlias() => "t";
        }

        [Table("foo")]
        public interface IFooTable : ITable
        {
            Column<int> Id { get; }

            [Column("user_name")]
            Column<string> UserName { get; }
        }

        public interface IBarTable : ITable
        {
            Column<int> Id { get; }

            [Column("user_name")]
            Column<string> UserName { get; }
        }
    }
}