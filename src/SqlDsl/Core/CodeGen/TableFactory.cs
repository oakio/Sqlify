using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SqlDsl.Core.CodeGen
{
    public static class TableFactory<TTable>
    {
        private static Func<string, TTable> _factory;

        public static TTable Create(string alias)
        {
            _factory ??= BuildFactory();

            return _factory(alias);
        }

        private static Func<string, TTable> BuildFactory()
        {
            Type type = TableTypeBuilder.Create(typeof(TTable));

            ConstructorInfo ctor = type.GetConstructor(new[] { typeof(string) });

            if (ctor == null)
            {
                throw new InvalidOperationException("Constructor not found");
            }

            ParameterExpression aliasParameter = Expression.Parameter(typeof(string), "alias");
            NewExpression newExpression = Expression.New(ctor, aliasParameter);
            Expression<Func<string, TTable>> lambda = Expression.Lambda<Func<string, TTable>>(newExpression, aliasParameter);

            Func<string, TTable> factory = lambda.Compile();
            return factory;
        }
    }
}