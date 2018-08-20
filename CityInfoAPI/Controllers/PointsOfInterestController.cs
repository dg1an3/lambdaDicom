using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfoAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfoAPI.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var city = CityDataStore.Instance.Cities.FirstOrDefault(cit => cit.Id == cityId);
            if (city == null)
                return NotFound();

            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{cityId}/pointsofinterest/{poiId}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int poiId)
        {
            var city = CityDataStore.Instance.Cities.FirstOrDefault(cit => cit.Id == cityId);
            if (city == null)
                return NotFound();

            var poi = city.PointsOfInterest.FirstOrDefault(poi4 => poi4.Id == poiId);
            if (poi == null)
                return NotFound();

            return Ok(poi);
        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId,
            [FromBody] PointOfInterestForCreateDto createDto)
        {
            if (createDto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();
                
            var city = CityDataStore.Instance.Cities.FirstOrDefault(cit => cit.Id == cityId);
            if (city == null)
                return NotFound();


            var newId = CityDataStore.Instance.Cities
                .SelectMany(cit => cit.PointsOfInterest)
                .OrderBy(cit => cit.Id).Last().Id ++;

            var newDto = new PointOfInterestDto()
            {
                Id = newId,
                Description = createDto.Description,
                Name = createDto.Name
            };
            city.PointsOfInterest.Add(newDto);

            return CreatedAtRoute("GetPointOfInterest", 
                new { cityId = cityId, poiId = newId }, newDto);
        }
    }
}