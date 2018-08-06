namespace LambdaDicom

module InformationObjectDefinitions =

    open Modules

    type ComputedTomographyInstance = 
        {  SOPCommon:SOPCommonModule; 
            Patient:PatientModule; 
            GeneralImage:GeneralImageModule }

    let computedTomographyInstanceToAttributes ctInstance =
        sopCommonToAttributes ctInstance.SOPCommon
        |> List.append (patientToAttributes ctInstance.Patient)
