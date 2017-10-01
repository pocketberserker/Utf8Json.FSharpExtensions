using System;
using System.Collections.Generic;
using Utf8Json;
using Utf8Json.Formatters;
using Microsoft.FSharp.Collections;

namespace Utf8Json.FSharp.Formatters
{
    public sealed class FSharpListFormatter<T> : CollectionFormatterBase<T, List<T>, IEnumerator<T>, FSharpList<T>>
    {
        protected override void Add(ref List<T> collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override FSharpList<T> Complete(ref List<T> intermediateCollection)
        {
            return ListModule.OfSeq(intermediateCollection);
        }

        protected override List<T> Create()
        {
            return new List<T>();
        }

        protected override IEnumerator<T> GetSourceEnumerator(FSharpList<T> source)
        {
            return ((IEnumerable<T>)source).GetEnumerator();
        }
    }

    public sealed class FSharpMapFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, List<Tuple<TKey, TValue>>, IEnumerator<KeyValuePair<TKey, TValue>>, FSharpMap<TKey, TValue>>
    {
        protected override void Add(ref List<Tuple<TKey, TValue>> collection, int index, TKey key, TValue value)
        {
            collection.Add(Tuple.Create(key, value));
        }

        protected override FSharpMap<TKey, TValue> Complete(ref List<Tuple<TKey, TValue>> intermediateCollection)
        {
            return new FSharpMap<TKey, TValue>(intermediateCollection);
        }

        protected override List<Tuple<TKey, TValue>> Create()
        {
            return new List<Tuple<TKey, TValue>>();
        }

        protected override IEnumerator<KeyValuePair<TKey, TValue>> GetSourceEnumerator(FSharpMap<TKey, TValue> source)
        {
            return ((IEnumerable<KeyValuePair<TKey, TValue>>)source).GetEnumerator();
        }
    }

    public sealed class FSharpSetFormatter<T> : CollectionFormatterBase<T, List<T>, IEnumerator<T>, FSharpSet<T>>
    {
        protected override void Add(ref List<T> collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override FSharpSet<T> Complete(ref List<T> intermediateCollection)
        {
            return new FSharpSet<T>(intermediateCollection);
        }

        protected override List<T> Create()
        {
            return new List<T>();
        }

        protected override IEnumerator<T> GetSourceEnumerator(FSharpSet<T> source)
        {
            return ((IEnumerable<T>)source).GetEnumerator();
        }
    }
}
