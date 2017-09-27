[<AutoOpen>]
module Utf8Json.Tests.Helper

open Utf8Json
open Utf8Json.Resolvers
open Utf8Json.FSharp

type WithFSharpDefaultResolver() =
  interface IJsonFormatterResolver with
    member __.GetFormatter<'T>() =
      match FSharpResolver.Instance.GetFormatter<'T>() with
      | null -> StandardResolver.Default.GetFormatter<'T>()
      | x -> x

let convert<'T> (value: 'T) =
  let resolver = WithFSharpDefaultResolver()
  JsonSerializer.Deserialize<'T>(JsonSerializer.Serialize(value, resolver), resolver)

