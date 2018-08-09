namespace LambdaDicom

type InformationObjectDefinitionAttribute() = inherit System.Attribute()

[<InformationObjectDefinition>]
type ComputedTomographyImage(SOPCommon:SOPCommonModule, 
                             Patient:PatientModule, 
                             GeneralImage:GeneralImageModule) =
    member this.SOPCommon = SOPCommon
    member this.Patient = Patient
    member this.GeneralImage = GeneralImage
    member this.asAttributes = 
        SOPCommon.asAttributes
        |> List.append Patient.asAttributes
        |> List.append GeneralImage.asAttributes
    static member fromAttributes attributes =
        ComputedTomographyImage(SOPCommonModule.fromAttributes attributes,
                                PatientModule.fromAttributes attributes,
                                GeneralImageModule.fromAttributes attributes)
