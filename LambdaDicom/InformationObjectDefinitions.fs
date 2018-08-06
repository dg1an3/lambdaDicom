namespace LambdaDicom

module InformationObjectDefinitions =

    open Modules

    type ComputedTomographyImage = 
        {  SOPCommon:SOPCommonModule; 
            Patient:PatientModule; 
            GeneralImage:GeneralImageModule }
