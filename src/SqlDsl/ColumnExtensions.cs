using SqlDsl.Core.Expressions;
using SqlDsl.Core.Predicates;

namespace SqlDsl
{
    public static class ColumnExtensions
    {
        public static Predicate Like(this Column<string> column, string pattern) => new LikePredicate(column, pattern);
    }
}