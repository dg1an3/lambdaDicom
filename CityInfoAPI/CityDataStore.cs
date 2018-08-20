using CityInfoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoAPI
{
    public class CityDataStore
    {
        public static CityDataStore Instance { get; } = new CityDataStore();

        public IList<CityDto> Cities { get; set; }

        public CityDataStore()
        {
            Cities = new List<CityDto>
            {
                new CityDto
                {
                    Id =1,
                    Name ="New York City",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto() { Id=1, Name="Central Park", Description="Central Park" }
                    }
                },
                new CityDto
                {
                    Id =2,
                    Name ="Antwerp",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto() { Id=3, Name="Central Market", Description="The center of the market"},
                    }
                },
                new CityDto
                {
                    Id =3,
                    Name = "Paris",
                    PointsOfInterest = new List<PointOfInterestDto>()
                }

            };
        }
    }
}
