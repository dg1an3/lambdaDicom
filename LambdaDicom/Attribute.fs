namespace LambdaDicom

open System

type UniqueIdentifier = 
    string

type Tag = 
    { Group:uint32; 
        Element:uint32 }

type Attribute = 
    { Tag:Tag; 
        Values:Values }

and Values =
| SQ of SequenceItem list
| UI of UniqueIdentifier[]
| IS of int32[]
| US of uint16[]
| SH of string[]
| DT of DateTime[]

and SequenceItem = 
    { ItemNumber:uint16; 
        Attributes: Attribute list }

module Attribute =

    let find tag attributes =
        let matchTag attribute = 
            attribute.Tag = tag
        attributes
        |> List.find matchTag

    let findValues<'v> tag attributes =
        match (attributes |> find tag).Values with
        | UI valArray -> valArray |> Seq.cast<'v>
        | IS valArray -> valArray |> Seq.cast<'v>
        | US valArray -> valArray |> Seq.cast<'v>
        | SH valArray -> valArray |> Seq.cast<'v>
        | DT valArray -> valArray |> Seq.cast<'v>
        | SQ _ -> raise (Exception(""))

    let findHeadValue<'v> tag =
        findValues<'v> tag >> Seq.head
        
    let arrayValues<'ValueType> (vr:Values) (value:'ValueType[]) =
        match (vr) with
        | UI _ -> UI (value |> Array.map (fun v -> Convert.ToString(v)))
        | IS _ -> IS (value |> Array.map (fun v -> Convert.ToInt32(v)))
        | US _ -> US (value |> Array.map (fun v -> Convert.ToUInt16(v)))
        | SH _ -> SH (value |> Array.map (fun v -> Convert.ToString(v)))
        | DT _ -> DT (value |> Array.map (fun v -> Convert.ToDateTime(v)))
        | SQ _ -> raise (Exception(""))

    let singleValue<'ValueType> (vr:Values) (value:'ValueType) =
        arrayValues vr [| value |]

