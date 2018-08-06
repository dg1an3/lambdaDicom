namespace LambdaDicom

open System

module Modules =
    type SOPCommonModule = 
        { SOPClassUID:UniqueIdentifier; 
            SOPInstanceUID:UniqueIdentifier; }

    let sopCommonToAttributes (scm:SOPCommonModule) =
        [ {Tag={Group=0x0000u; Element=0x0000u}; Values=UI [| scm.SOPClassUID |]};
            {Tag={Group=0x0000u; Element=0x0000u}; Values=UI [| scm.SOPInstanceUID |]} ]

    let attributesToSopCommon attributes =
        { SOPClassUID=(attributes |> Attribute.findHeadValue<UniqueIdentifier> {Group=0x0000u; Element=0x0000u});
            SOPInstanceUID=(attributes |> Attribute.findHeadValue<UniqueIdentifier> {Group=0x0000u; Element=0x0000u}); }

    type PatientModule = 
        { PatientId:string }

    type GeneralImageModule = 
        { AcquisitionDateTime:DateTime }