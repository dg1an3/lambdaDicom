namespace FsCityInfo.API

type PointOfInterestDto = {
  ID : int
  Name : string }

// Stores information about product (will be used later)
type CityDto = {
  ID : int
  Name : string
  Description : string option
  PointsOfInterest : PointOfInterestDto list }

type CityDataStore() =
    member this.Cities = 
        [ {ID=1; Name="New York City"; Description=None; PointsOfInterest=[] };
            {ID=2; Name="Antwerp"; Description=None; PointsOfInterest=[] } ]