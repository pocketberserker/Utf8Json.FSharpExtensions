# Utf8Json.FSharpExtensions

[![Build status](https://ci.appveyor.com/api/projects/status/ng1hco02vd52362u/branch/master?svg=true)](https://ci.appveyor.com/project/pocketberserker/utf8json-fsharpextensions/branch/master)
[![Build Status](https://travis-ci.org/pocketberserker/Utf8Json.FSharpExtensions.svg?branch=enable-ci)](https://travis-ci.org/pocketberserker/Utf8Json.FSharpExtensions)

Utf8Json.FSharpExtensions is a [Utf8Json](https://github.com/neuecc/Utf8Json) extension library for F#.

## Usage

```fsharp
open Utf8Json
open Utf8Json.Resolvers
open Utf8Json.FSharp

CompositeResolver.RegisterAndSetAsDefault(
  FSharpResolver.Instance,
  StandardResolver.Instance
)

type Person = {
  Age: int
  FirstName: string
  LastName: string
  MiddleName: string option
}

let p = {
  Age = 99
  FirstName = "foo"
  LastName = "buz"
  MiddleName = Some "bar"
}

let result = JsonSerializer.Serialize(p)
let p2 = JsonSerializer.Deserialize<Person>(result)
```

