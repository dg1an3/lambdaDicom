namespace LambdaDicom

module SOPCommonModule =

    type Record = 
        { SOPClassUID:UniqueIdentifier; 
            SOPInstanceUID:UniqueIdentifier; }

    let toAttributes (scm:Record) =
        [ {Tag={Group=0x0000u; Element=0x0000u}; Values=UI [| scm.SOPClassUID |]};
            {Tag={Group=0x0000u; Element=0x0000u}; Values=UI [| scm.SOPInstanceUID |]} ]

    let fromAttributes attributes =
        { SOPClassUID=(attributes |> Attribute.findHeadValue<UniqueIdentifier> {Group=0x0000u; Element=0x0000u});
            SOPInstanceUID=(attributes |> Attribute.findHeadValue<UniqueIdentifier> {Group=0x0000u; Element=0x0000u}); }


