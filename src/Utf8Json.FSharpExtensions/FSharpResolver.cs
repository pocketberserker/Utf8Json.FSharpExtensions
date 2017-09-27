using System;
using System.Collections.Generic;
using System.Reflection;
using Utf8Json.Formatters;
using Microsoft.FSharp.Core;
using Utf8Json.FSharp.Formatters;

namespace Utf8Json.FSharp
{
    public sealed class FSharpResolver : IJsonFormatterResolver
    {
        public static readonly IJsonFormatterResolver Instance = new FSharpResolver();

        FSharpResolver() { }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                formatter = (IJsonFormatter<T>)FSharpGetFormatterHelper.GetFormatter(typeof(T));
            }
        }
    }

    internal static class FSharpGetFormatterHelper
    {
        internal static object GetFormatter(Type t)
        {
            var ti = t.GetTypeInfo();

            if (ti.IsGenericType)
            {
                var genericType = ti.GetGenericTypeDefinition();

                if (genericType.GetTypeInfo().IsFSharpOption())
                {
                    return CreateInstance(typeof(FSharpOptionFormatter<>), new[] { ti.GenericTypeArguments[0] });
                }
            }

            return null;
        }

        static object CreateInstance(Type genericType, Type[] genericTypeArguments, params object[] arguments)
        {
            return Activator.CreateInstance(genericType.MakeGenericType(genericTypeArguments), arguments);
        }
    }

    internal static class ReflectionExtensions
    {
        public static bool IsFSharpOption(this TypeInfo type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(FSharpOption<>);
        }
    }
}
