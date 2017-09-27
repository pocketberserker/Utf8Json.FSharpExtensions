using System;
using Utf8Json;
using Microsoft.FSharp.Core;

namespace Utf8Json.FSharp.Formatters
{
    public sealed class FSharpOptionFormatter<T> : IJsonFormatter<FSharpOption<T>>
    {
        public void Serialize(ref JsonWriter writer, FSharpOption<T> value, IJsonFormatterResolver formatterResolver)
        {
            if (FSharpOption<T>.get_IsNone(value))
            {
                writer.WriteNull();
            }
            else
            {
                formatterResolver.GetFormatterWithVerify<T>().Serialize(ref writer, value.Value, formatterResolver);
            }
        }

        public FSharpOption<T> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                return FSharpOption<T>.Some(formatterResolver.GetFormatterWithVerify<T>().Deserialize(ref reader, formatterResolver));
            }
        }
    }

    public sealed class StaticFSharpOptionFormatter<T> : IJsonFormatter<FSharpOption<T>>
    {
        readonly IJsonFormatter<T> underlyingFormatter;

        public StaticFSharpOptionFormatter(IJsonFormatter<T> underlyingFormatter)
        {
            this.underlyingFormatter = underlyingFormatter;
        }

        public StaticFSharpOptionFormatter(Type formatterType, object[] formatterArguments)
        {
            try
            {
                underlyingFormatter = (IJsonFormatter<T>)Activator.CreateInstance(formatterType, formatterArguments);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Can not create formatter from JsonFormatterAttribute, check the target formatter is public and has constructor with right argument. FormatterType:" + formatterType.Name, ex);
            }
        }

        public void Serialize(ref JsonWriter writer, FSharpOption<T> value, IJsonFormatterResolver formatterResolver)
        {
            if (FSharpOption<T>.get_IsNone(value))
            {
                writer.WriteNull();
            }
            else
            {
                underlyingFormatter.Serialize(ref writer, value.Value, formatterResolver);
            }
        }

        public FSharpOption<T> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                return FSharpOption<T>.Some(underlyingFormatter.Deserialize(ref reader, formatterResolver));
            }
        }
    }
}
