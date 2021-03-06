using Sqlify.Core.Expressions;

namespace Sqlify.Core.Predicates
{
    public class LikePredicate : Predicate
    {
        private readonly Column<string> _column;
        private readonly string _pattern;

        public LikePredicate(Column<string> column, string pattern)
        {
            _column = column;
            _pattern = pattern;
        }

        public override void Format(ISqlWriter sql)
        {
            _column.Format(sql);
            sql.Append(" LIKE ");

            var paramName = sql.AddParam(_pattern);
            sql.Append(paramName);
        }
    }
}