using System;
using NUnit.Framework;
using Sqlify.Core;

namespace Sqlify.Tests
{
    public static class TestUtils
    {
        public static void ShouldBe(this ISqlFormattable self, string expectedSql)
        {
            var writer = new SqlWriter();
            self.Format(writer);

            string sql = writer.GetCommand();

            Assert.That(sql, Is.EqualTo(expectedSql));
            Console.WriteLine(sql);
        }
    }
}