using System;
using System.Collections;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using SqlDsl.Core.CodeGen;

namespace SqlDsl.Tests.Core.CodeGen
{
    [TestFixture]
    public class TableNameResolverTests
    {
        [TestCaseSource(nameof(TestCases))]
        public void Should_resolve_table_name(string lineNo, Type type, string expectedName)
        {
            string name = TableNameResolver.Resolve(type);

            Assert.That(name, Is.EqualTo(expectedName));
        }

        public static IEnumerable TestCases
        {
            get
            {
                yield return Case<IFooTable>("Foo");
                yield return Case<FooTable>("Foo");
                yield return Case<IFoo>("Foo");
                yield return Case<Foo>("Foo");
                yield return Case<IItems>("Items");
                yield return Case<Items>("Items");
                yield return Case<ItemsTable>("Items");
                yield return Case<IBarTable>("bar_attribute");
            }
        }

        private static object[] Case<T>(string name, [CallerLineNumber] int lineNo = 0) => new object[]
        {
            $"line #{lineNo}",
            typeof(T),
            name
        };

        private interface IFooTable
        {
        }

        private interface IFoo
        {
        }

        private class FooTable
        {
        }

        private class Foo
        {
        }

        private interface ITable
        {
        }

        private interface IItems
        {
        }

        private class Items
        {
        }

        private class ItemsTable
        {
        }

        [Table("bar_attribute")]
        private interface IBarTable
        {
        }
    }
}