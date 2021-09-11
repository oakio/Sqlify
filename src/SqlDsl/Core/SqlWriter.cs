using System.Collections.Generic;
using System.Text;

namespace SqlDsl.Core
{
    public class SqlWriter : ISqlWriter
    {
        private readonly StringBuilder _builder;
        private ParamNameGenerator _generator;
        private Dictionary<string, object> _params;

        public SqlWriter()
        {
            _builder = new StringBuilder();
            _generator = new ParamNameGenerator();
        }

        public void Append(string text) => _builder.Append(text);

        public string AddParam<T>(T value)
        {
            if (_params == null)
            {
                _params = new Dictionary<string, object>();
            }

            string name = _generator.Next();
            _params.Add(name, value);
            return name;
        }

        public string GetCommand() => _builder.ToString();

        public object GetParam(string name) => _params[name];
    }
}