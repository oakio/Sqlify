using Sqlify.Core.Expressions;

namespace Sqlify.Core.CodeGen
{
    public abstract class TableBase : ITable
    {
        private readonly string _name;

        private readonly string _alias;

        protected TableBase(string name, string alias)
        {
            _name = string.Intern(name);
            _alias = alias;
        }

        public string GetName() => _name;

        public string GetAlias() => _alias;

        protected Column<T> CreateColumn<T>(string name) => new Column<T>(name, _alias ?? _name);

        protected Expression<T> CreateComputedColumn<T>(string name, Expression<T> expression) => expression.As(name);
    }
}