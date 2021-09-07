using SqlDsl.Core.Expressions;

namespace SqlDsl
{
    public class Table
    {
        private readonly string _name;

        private readonly string _alias;

        public Table(string name, string alias)
        {
            _name = string.Intern(name);
            _alias = alias;
        }

        public string GetName() => _name;

        public string GetAlias() => _alias;

        protected ColumnExpression<T> CreateColumn<T>(string name) => new ColumnExpression<T>(name, _alias ?? _name);
    }
}