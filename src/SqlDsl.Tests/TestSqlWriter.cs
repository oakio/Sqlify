using System.Text;
using SqlDsl.Core;

namespace SqlDsl.Tests
{
    public class TestSqlWriter : ISqlWriter
    {
        private readonly StringBuilder _command;
        private ParamNameGenerator _generator;

        public TestSqlWriter()
        {
            _command = new StringBuilder();
        }

        public void Append(string text) => _command.Append(text);

        public string AddParam<T>(T value) => _generator.Next();

        public override string ToString() => _command.ToString();
    }
}