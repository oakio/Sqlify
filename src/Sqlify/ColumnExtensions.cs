using Sqlify.Core.Expressions;
using Sqlify.Core.Predicates;

namespace Sqlify
{
    public static class ColumnExtensions
    {
        public static Predicate Like(this Column<string> column, string pattern) => new LikePredicate(column, pattern);
    }
}