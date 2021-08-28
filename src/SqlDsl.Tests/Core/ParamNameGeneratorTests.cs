using NUnit.Framework;
using SqlDsl.Core;

namespace SqlDsl.Tests.Core
{
    [TestFixture]
    public class ParamNameGeneratorTests
    {
        [Test]
        public void NextTest()
        {
            var generator = new ParamNameGenerator();

            Assert.That(generator.Next(), Is.EqualTo("@p1"));
            Assert.That(generator.Next(), Is.EqualTo("@p2"));
            Assert.That(generator.Next(), Is.EqualTo("@p3"));
        }

        [Test]
        public void Next1000Test()
        {
            var generator = new ParamNameGenerator();
            for (int i = 1; i <= 1000; i++)
            {
                string name = generator.Next();
                string expected = "@p" + i;
                Assert.That(name, Is.EqualTo(expected));
            }
        }
    }
}