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
| IS of uint32[]
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
        
