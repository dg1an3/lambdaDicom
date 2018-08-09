namespace LambdaDicom

open System

type ModuleAttribute() = inherit System.Attribute()

[<Module>]
type SOPCommonModule(SOPClassUID:UniqueIdentifier, 
                     SOPInstanceUID:UniqueIdentifier) =
    member this.SOPClassUID = SOPClassUID
    member this.SOPInstanceUID = SOPInstanceUID
    interface IEquatable<SOPCommonModule> with
        member this.Equals (other:SOPCommonModule) =
            [ this.SOPClassUID.CompareTo(other.SOPClassUID) = 0;
              this.SOPInstanceUID.CompareTo(other.SOPInstanceUID) = 0]
            |> List.fold (&&) true

[<Module>]
type PatientModule(PatientId:string) =
    member this.PatientId = PatientId
    interface IEquatable<PatientModule> with
        member this.Equals (other:PatientModule) =            
            [ this.PatientId.CompareTo(other.PatientId) = 0 ]
            |> List.fold (&&) true

[<Module>]
type GeneralImageModule(AcquisitionDateTime:DateTime) =
    member this.AcquisitionDateTime = AcquisitionDateTime
    interface IEquatable<GeneralImageModule> with
        member this.Equals (other:GeneralImageModule) =
            [ this.AcquisitionDateTime.CompareTo(other.AcquisitionDateTime) = 0 ]
            |> List.fold (&&) true

module ModuleEncoding =

    open System.Reflection

    // converts a defined module to a dicom attribute list
    let toAttributes<'ModuleType> (forModule:'ModuleType) =
        let attributeForProperty (prop:PropertyInfo) =
            let (tag, vr) = Dictionary.tagAndValueRepresentation prop.Name
            let value = prop.GetValue(forModule)
            {Tag=tag; Values=Attribute.singleValue vr value}

        typeof<'ModuleType>.GetProperties(BindingFlags.Instance ||| BindingFlags.Public)
        |> Array.map attributeForProperty
        |> Seq.ofArray

    // creates a new module from list of attributes
    let fromAttributes<'ModuleType when 'ModuleType :> obj> attributes =
        // should only be one constructor
        let constructor = typeof<'ModuleType>.GetConstructors().[0]
        let valueForParameter (param:ParameterInfo) =
            let (tag, _) = Dictionary.tagAndValueRepresentation param.Name            
            Attribute.findHeadValue tag attributes
        let parameterValues = constructor.GetParameters() |> Array.map valueForParameter
        constructor.Invoke(parameterValues) :?> 'ModuleType
