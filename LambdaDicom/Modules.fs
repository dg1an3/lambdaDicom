namespace LambdaDicom

open System

type ModuleAttribute() = inherit System.Attribute()

[<Module>]
type SOPCommonModule(SOPClassUID:UniqueIdentifier, 
                     SOPInstanceUID:UniqueIdentifier) =
    member this.SOPClassUID = SOPClassUID
    member this.SOPInstanceUID = SOPInstanceUID
    member this.asAttributes : LambdaDicom.Attribute list =
        [ {Tag={Group=0x0000u; Element=0x0000u}; Values=UI [| this.SOPClassUID |]};
          {Tag={Group=0x0000u; Element=0x0000u}; Values=UI [| this.SOPInstanceUID |]} ]
    static member fromAttributes attributes =
        SOPCommonModule(attributes |> LambdaDicom.Attribute.findHeadValue<UniqueIdentifier> {Group=0x0000u; Element=0x0000u},
                        attributes |> LambdaDicom.Attribute.findHeadValue<UniqueIdentifier> {Group=0x0000u; Element=0x0000u})

[<Module>]
type PatientModule(PatientId:string) =
    member this.PatientId = PatientId
    member this.asAttributes : LambdaDicom.Attribute list =
        [ {Tag={Group=0x0000u; Element=0x0000u}; Values=UI [| this.PatientId |]}; ]
    static member fromAttributes attributes =
        PatientModule(attributes |> LambdaDicom.Attribute.findHeadValue<UniqueIdentifier> {Group=0x0000u; Element=0x0000u})

[<Module>]
type GeneralImageModule(AcquisitionDateTime:DateTime) =
    member this.AcquisitionDateTime = AcquisitionDateTime
    member this.asAttributes : LambdaDicom.Attribute list =
        [ {Tag={Group=0x0000u; Element=0x0000u}; Values=DT [| this.AcquisitionDateTime |]}; ]
    static member fromAttributes attributes =
        GeneralImageModule(attributes |> LambdaDicom.Attribute.findHeadValue<DateTime> {Group=0x0000u; Element=0x0000u})
