using NUnit.Framework;
using Sqlify.Core.Expressions;

namespace Sqlify.Tests.Core.Expressions
{
    [TestFixture]
    public class ParamExpressionTests
    {
        [Test]
        public void Test()
        {
            var expression = new ParamExpression<int>(10);

            expression.ShouldBe("@p1");
        }
    }
}