using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CityDetails.Models
{
    public partial class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public decimal? TouristRating { get; set; }
        public DateTime? EstablishedDate { get; set; }
        public long? EstimatedPopulation { get; set; }
    }
}
