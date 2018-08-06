namespace LambdaDicom.UnitTest.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open FsUnit.MsTest
open LambdaDicom
open System.Collections.Generic
open NUnit.Framework

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
    ``when asked to find a tag that is not in the list.`` ()=
      try
        attributes
        |> LambdaDicom.Attribute.find {Group=0x0002u;Element=0x0003u}
        |> ignore
        Assert.Fail()
      with
        | :? KeyNotFoundException -> ()
        | _ -> failwith("The test did not throw the correct exception.")
        

