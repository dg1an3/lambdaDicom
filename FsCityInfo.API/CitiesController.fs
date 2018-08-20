namespace FsCityInfo.API

open Microsoft.AspNetCore.Mvc

[<Route("api/cities")>]
type CitiesController() =
  inherit Controller()

  member this.checkReturn result =
    match result with
    | Some value -> this.Ok(value) :> IActionResult
    | None -> this.NotFound() :> IActionResult

  [<HttpGet()>]
  member this.GetCities() =
    CityDataStore().Cities
    |> this.Ok

  [<HttpGet("{id}")>]
  member this.GetCity(id:int) =
    CityDataStore().Cities
    |> List.where (fun cit -> cit.ID = id)
    |> List.tryHead
    |> this.checkReturn

  [<HttpGet("{cityId}/pointsofinterest>")>]
  member this.GetPointsOfInterest(cityId:int) =
     CityDataStore().Cities
     |> List.where (fun cit -> cit.ID = cityId)
     |> List.tryHead
     |> this.checkReturn

  [<HttpGet("{cityId}/pointsofinterest/{poiId}")>]
  member this.GetPointOfInterest(cityId:int) (poiId:int) =
     let selectedCityOption = 
       CityDataStore().Cities
       |> List.where (fun cit -> cit.ID = cityId)
       |> List.tryHead
     match selectedCityOption with
     | Some selectedCity -> 
        this.Ok(selectedCity.PointsOfInterest) :> IActionResult
     | None -> 
        this.NotFound() :> IActionResult