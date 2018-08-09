namespace LambdaDicom

type InformationObjectDefinitionAttribute() = inherit System.Attribute()

[<InformationObjectDefinition>]
type ComputedTomographyImage(SOPCommon:SOPCommonModule, 
                             Patient:PatientModule, 
                             GeneralImage:GeneralImageModule) =
    member this.SOPCommon = SOPCommon
    member this.Patient = Patient
    member this.GeneralImage = GeneralImage
