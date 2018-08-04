module Utf8Json.Tests.CollectionTest

open Xunit

[<Fact>]
let ``fsharp list`` () =

  let input: int list = []
  let actual = convert input
  Assert.Equal(box input, box actual)

  let input = [1]
  let actual = convert input
  Assert.Equal(box input, box actual)

[<Fact>]
let ``fsharp map`` () =

  let input= Map.empty<int, bool>
  let actual = convert input
  Assert.Equal(box input, box actual)

  let input = Map.empty |> Map.add 0 true
  let actual = convert input
  Assert.Equal(box input, box actual)

[<Fact>]
let ``fsharp set`` () =

  let input = Set.empty<int>
  let actual = convert input
  Assert.Equal(box input, box actual)

  let input = Set.singleton 1
  let actual = convert input
  Assert.Equal(box input, box actual)
