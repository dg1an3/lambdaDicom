namespace LambdaDicom

module GeneralImageModule =

    open System

    type Record = 
        { AcquisitionDateTime:DateTime }

    let toAttributes (gi:Record) = 
        [ {Tag={Group=0x0000u; Element=0x0000u}; Values=DT [| gi.AcquisitionDateTime |]}; ]

    let fromAttributes attributes =
        { AcquisitionDateTime =(attributes |> Attribute.findHeadValue<DateTime> {Group=0x0000u; Element=0x0000u}); }