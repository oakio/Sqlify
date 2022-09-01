using NUnit.Framework;
using Sqlify.Core;
using Sqlify.Core.Expressions;

namespace Sqlify.Tests
{
    [TestFixture]
    public class ComputedColumnTests
    {
#if !NET472
        [Test]
        public void Select_computed_column()
        {
            var o = Sql.Table<IOrder>();

            SelectQuery query = Sql.Select(o.Id, o.Total)
                .From(o);

            query.ShouldBe("SELECT orders.id, orders.qty * orders.price AS total FROM orders");
        }

        [Test]
        public void Select_computed_column_alias()
        {
            var o = Sql.Table<IOrder>("o");

            SelectQuery query = Sql.Select(o.Id, o.Total).From(o);

            query.ShouldBe("SELECT o.id, o.qty * o.price AS total FROM orders o");
        }

        [Table("orders")]
        public interface IOrder : ITable
        {
            [Column("id")]
            Column<int> Id { get; }

            [Column("qty")]
            Column<int> Qty { get; }

            [Column("price")]
            Column<int> Price { get; }

            [Column("total")]
            Expression<int> Total => Qty * Price; // <- computed column
        }
#endif
    }
}