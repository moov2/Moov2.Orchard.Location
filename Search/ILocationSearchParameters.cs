namespace Moov2.Orchard.Location.Search
{
    public interface ILocationSearchParameters
    {
        string Query { get; set; }
        
        string Company { get; set; }
        string Street { get; set; }
        string Postcode { get; set; }
        string Town { get; set; }
        string CountyState { get; set; }
        string Country { get; set; }

        double? Longitude { get; set; }
        double? Latitude { get; set; }
        double? Radius { get; set; }
    }
}
