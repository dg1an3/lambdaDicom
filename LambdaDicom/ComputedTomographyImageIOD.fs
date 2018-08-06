namespace LambdaDicom

module ComputedTomographyImageIOD =

    type ComputedTomographyImageIODRecord = 
        {  SOPCommon:SOPCommonModule.Record; 
            Patient:PatientModule.Record; 
            GeneralImage:GeneralImageModule.Record }

    let computedTomographyInstanceToAttributes ctInstance =
        SOPCommonModule.toAttributes ctInstance.SOPCommon
        |> List.append (PatientModule.toAttributes ctInstance.Patient)
        |> List.append (GeneralImageModule.toAttributes ctInstance.GeneralImage)
