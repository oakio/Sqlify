using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Sqlify.Core.Expressions;

namespace Sqlify.Core.CodeGen
{
    public class TableTypeBuilder
    {
        private static readonly ModuleBuilder Builder;

        private const string RootName = "Sqlify.CodeGen";

        static TableTypeBuilder()
        {
            var name = new AssemblyName(RootName);
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);

            Builder = assemblyBuilder.DefineDynamicModule(RootName);
        }

        public static Type Create(Type interfaceType)
        {
            if (!interfaceType.IsInterface)
            {
                throw new InvalidOperationException($"Type {interfaceType} is not interface");
            }

            if (!interfaceType.IsPublic &&
                !interfaceType.IsNestedPublic)
            {
                throw new InvalidOperationException($"Interface {interfaceType} must be public");
            }

            if (interfaceType.GetMembers().Any(m => m is MethodInfo { IsSpecialName: false }))
            {
                throw new InvalidOperationException($"Interface {interfaceType} must contains properties only");
            }

            string name = $"{RootName}.Table_{interfaceType.Name}";
            Type baseType = typeof(TableBase);

            TypeBuilder typeBuilder = Builder.DefineType(
                name,
                TypeAttributes.Public |
                TypeAttributes.Class |
                TypeAttributes.AutoClass |
                TypeAttributes.AnsiClass |
                TypeAttributes.BeforeFieldInit |
                TypeAttributes.AutoLayout,
                baseType,
                new[] { interfaceType });

            ConstructorBuilder ctorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                new[] { typeof(string) } // (alias)
            );

            ILGenerator ctorCode = ctorBuilder.GetILGenerator();

            string tableName = TableNameResolver.Resolve(interfaceType);

            EmitBaseCtorCall(ctorCode, baseType, tableName);

            MethodInfo createColumnMethod = baseType.GetMethod(
                "CreateColumn",
                BindingFlags.Instance |
                BindingFlags.NonPublic);

            Type columnGenericType = typeof(Column<>);

            PropertyInfo[] properties = interfaceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                Type propertyType = property.PropertyType;

                if (property.CanWrite)
                {
                    throw new InvalidOperationException($"Unexpected setter for property {interfaceType}.{propertyName}");
                }

                if (!propertyType.IsGenericType ||
                    propertyType.GetGenericTypeDefinition() != columnGenericType)
                {
                    throw new InvalidOperationException($"Unsupported property {interfaceType}.{propertyName} type. Expected {columnGenericType}");
                }

                Type columnType = propertyType.GetGenericArguments()[0];
                
                FieldBuilder fieldBuilder = typeBuilder.DefineField(
                    $"_{propertyName}",
                    propertyType,
                    FieldAttributes.Private);

                PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(
                    propertyName,
                    PropertyAttributes.HasDefault,
                    propertyType,
                    null);

                MethodBuilder getMethod = typeBuilder.DefineMethod(
                    $"get_{propertyName}",
                    MethodAttributes.Public |
                    MethodAttributes.Final |
                    MethodAttributes.SpecialName |
                    MethodAttributes.HideBySig |
                    MethodAttributes.NewSlot |
                    MethodAttributes.Virtual,
                    propertyType,
                    Type.EmptyTypes);

                var getMethodCode = getMethod.GetILGenerator();
                getMethodCode.Emit(OpCodes.Ldarg_0);
                getMethodCode.Emit(OpCodes.Ldfld, fieldBuilder);
                getMethodCode.Emit(OpCodes.Ret);

                propertyBuilder.SetGetMethod(getMethod);

                string columnName = GetColumnName(property);

                EmitInitColumnField(ctorCode, createColumnMethod, columnType, columnName,  fieldBuilder);
            }

            ctorCode.Emit(OpCodes.Ret);

            return typeBuilder.CreateTypeInfo();
        }

        private static void EmitBaseCtorCall(ILGenerator ctorCode, Type baseType, string tableName)
        {
            ConstructorInfo baseCtor = baseType.GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new[]
                {
                    typeof(string),
                    typeof(string)
                },
                null);

            ctorCode.Emit(OpCodes.Ldarg_0);
            ctorCode.Emit(OpCodes.Ldstr, tableName);
            ctorCode.Emit(OpCodes.Ldarg_1); // alias param
            ctorCode.Emit(OpCodes.Call, baseCtor);
        }

        private static string GetColumnName(PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute<ColumnAttribute>();
            return attribute != null
                ? attribute.Name
                : property.Name;
        }

        private static void EmitInitColumnField(ILGenerator code, MethodInfo createColumnMethod, Type columnType, string arg, FieldInfo field)
        {
            MethodInfo method = createColumnMethod.MakeGenericMethod(columnType);

            code.Emit(OpCodes.Ldarg_0);
            code.Emit(OpCodes.Ldarg_0);
            code.Emit(OpCodes.Ldstr, arg);
            code.EmitCall(OpCodes.Call, method, new[] { typeof(string) });
            code.Emit(OpCodes.Stfld, field);
        }
    }
}