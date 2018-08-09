namespace LambdaDicom.UnitTest.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open FsUnit.MsTest

open System

open LambdaDicom

[<TestClass>] 
type ``encode and decode an SOPCommon`` () =
    [<TestMethod>]
    member __.``convert sop common to attribute sequence`` () =
        let sopCommon = SOPCommonModule("1.2.3", "4.5.6")
        let attributes = ModuleEncoding.toAttributes sopCommon
        attributes |> should not' (be null)

    [<TestMethod>]
    member __.``encode and decode sop common`` () =
        let sopCommon = SOPCommonModule("1.2.3", "4.5.6")
        let attributes = ModuleEncoding.toAttributes sopCommon
        let reSopCommon = ModuleEncoding.fromAttributes<SOPCommonModule> (List.ofSeq attributes)
        Assert.IsTrue((sopCommon :> IEquatable<SOPCommonModule>).Equals(reSopCommon))

    [<TestMethod>]
    member __.``encode and decode patient module`` () =
        let patient = PatientModule("MR1234")
        let attributes = ModuleEncoding.toAttributes patient
        let rePatient = ModuleEncoding.fromAttributes<PatientModule> (List.ofSeq attributes)
        Assert.IsTrue((patient:>IEquatable<PatientModule>).Equals(rePatient))