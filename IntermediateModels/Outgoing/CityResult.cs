using System;

namespace CityDetails.IntermediateModels.Outgoing
{
    public class CityResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public decimal? TouristRating { get; set; }
        public DateTime? EstablishedDate { get; set; }
        public long? EstimatedPopulation { get; set; }
        public int? CountryCode2Digit { get; set; }
        public int? CountryCode3Digit { get; set; }
        public string CurrencyCode { get; set; }
        public string Weather { get; set; }
    }
}
