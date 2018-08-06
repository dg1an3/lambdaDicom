namespace LambdaDicom

module PatientModule =

    type Record = 
        { PatientId:string }

    let toAttributes (pm:Record) =
        [ {Tag={Group=0x0000u; Element=0x0000u}; Values=UI [| pm.PatientId |]}; ]

    let fromAttributes attributes =
        { PatientId=(attributes |> Attribute.findHeadValue<UniqueIdentifier> {Group=0x0000u; Element=0x0000u}); }
