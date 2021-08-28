using System;
using NUnit.Framework;
using SqlDsl.Core;

namespace SqlDsl.Tests
{
    public static class TestUtils
    {
        public static void ShouldBe(this ISqlFormattable self, string expectedSql)
        {
            var writer = new TestSqlWriter();
            self.Format(writer);

            string sql = writer.ToString();

            Assert.That(sql, Is.EqualTo(expectedSql));
            Console.WriteLine(sql);
        }
    }
}