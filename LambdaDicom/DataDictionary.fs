namespace LambdaDicom

module Dictionary =
    open System

    let tagAndValueRepresentation (name:string) =
        match name with 
        | "SOPClassUID" -> ({Group=0x0001u; Element=0x0001u}, UI [||])
        | "SOPInstanceUID" -> ({Group=0x0001u; Element=0x0002u}, UI [||])
        | "PatientId" -> ({Group=0x0002u; Element=0x0001u}, SH [||])
        | "AcquisitionDateTime" -> ({Group=0x0003u; Element=0x0001u}, DT [||])
        | _ -> raise (Exception("unrecognized tag name"))
