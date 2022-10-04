using System;

namespace CityDetails.IntermediateModels.Incoming
{
    public class CityDelta
    {
        public int Id { get; set; }
        public decimal? TouristRating { get; set; }
        public DateTime? EstablishedDate { get; set; }
        public long? EstimatedPopulation { get; set; }
    }
}
