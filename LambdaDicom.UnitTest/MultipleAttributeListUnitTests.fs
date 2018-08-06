namespace LambdaDicom.UnitTest.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open FsUnit.MsTest
open LambdaDicom
open System.Collections.Generic

[<TestClass>] 
type ``given a list with multiple DICOM attributes`` ()=
   let tag1 = {Group=0x0001u;Element=0x0002u}
   let tag2 = {Group=0x0003u;Element=0x0005u}
   let tag3 = {Group=0x0004u;Element=0x001Au}
   let attributes = 
    [ { Tag=tag1; Values=UI [| "1.2.3.4" |] };
        { Tag=tag2; Values=IS [| 902 |] };
        { Tag=tag3; Values=SH [| "some text" |] }; ]

   [<TestMethod>] member test.
    ``when asked to find one tag in the list.`` ()=
        attributes
        |> LambdaDicom.Attribute.find tag2
        |> should equal { Tag=tag2; Values=IS [| 902 |] }

   [<TestMethod>] member test.
    ``when asked to find another tag in the list.`` ()=
        attributes
        |> LambdaDicom.Attribute.find tag3
        |> should equal { Tag=tag3; Values=SH [| "some text" |] }

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

