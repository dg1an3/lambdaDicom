namespace LambdaDicom.UnitTest.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open FsUnit.MsTest
open LambdaDicom
open System.Collections.Generic


type LightBulb(state) =
   member x.On = state
   override x.ToString() =
       match x.On with
       | true  -> "On"
       | false -> "Off"

[<TestClass>] 
type ``given a list of a single DICOM attributes`` ()=
   let tag = {Group=0x0001u;Element=0x0002u}
   let attributes = [ { Tag=tag; Values=UI [| "1.2.3.4" |] } ]

   [<TestMethod>] member test.
    ``when asked to find then single tag in the list.`` ()=
        attributes
        |> LambdaDicom.Attribute.find tag
        |> should equal { Tag=tag; Values=UI [| "1.2.3.4" |] }

   [<TestMethod>] member test.
    ``when asked to find a different tag in then list.`` ()=
        attributes
        |> LambdaDicom.Attribute.find {Group=0x0002u;Element=0x0003u}
        |> should throw typeof<System.Exception>
        

