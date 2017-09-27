[<AutoOpen>]
module Utf8Json.Tests.Helper

open Utf8Json

let convert<'T> (value: 'T) =
  JsonSerializer.Deserialize<'T>(JsonSerializer.Serialize(value))

