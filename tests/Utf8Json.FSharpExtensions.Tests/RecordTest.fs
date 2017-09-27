module Utf8Json.Tests.RecordTest

open Xunit
open Utf8Json

type SimpleRecord = {
  Property1: int
  Property2: int64
  Property3: float32
}

[<Fact>]
let ``simple record`` () =

  let input = { Property1 = 100; Property2 = 99999999L; Property3 = -123.43f }
  let actual = convert input
  Assert.Equal(input, actual)

[<Struct>]
type StructRecord = {
  X: int
  Y: int
}

[<Fact>]
let ``struct record`` () =

  let input = { X = 1; Y = 2 }
  let actual = convert input
  Assert.Equal(input, actual)

