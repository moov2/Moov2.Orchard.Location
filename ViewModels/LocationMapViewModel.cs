namespace Moov2.Orchard.Location.ViewModels
{
    public class LocationMapViewModel
    {
        public string ApiKey { get; set; }
        public int ContentItemId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}