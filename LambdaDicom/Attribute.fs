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

// reference http://dicom.nema.org/medical/dicom/current/output/chtml/part05/sect_6.2.html
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

    open System.IO

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

    let readFromStream (stream:Stream) =
        stream.Seek(int64 0, SeekOrigin.Begin) |> ignore
        use reader = new BinaryReader(stream)
        let eof = 
            reader.BaseStream.Position >= reader.BaseStream.Length
        let readAttribute = 
            let tag = 
                {Group=reader.ReadUInt32(); 
                    Element=reader.ReadUInt32()}
            let valueRepresentation = reader.ReadBytes(2)
            let valueLength = int (reader.ReadUInt32())
            let values = 
                match valueRepresentation with
                | "UI"B -> UI (reader.ReadChars(valueLength).ToString().Split('/'))
                | "IS"B -> IS (Array.init (valueLength/sizeof<int32>) (fun n -> reader.ReadInt32()))
                | "US"B -> US (Array.init (valueLength/sizeof<uint16>) (fun n -> reader.ReadUInt16()))
                | "SH"B -> SH (reader.ReadChars(valueLength).ToString().Split('/'))
                | "DT"B -> DT (Array.init valueLength (fun n -> DateTime.Parse(reader.ReadString())))
                | _ -> raise (Exception("invalid VR"))
            {Tag=tag; Values=values}
        seq { while not(eof) do yield readAttribute }
