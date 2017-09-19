using GoogleMapsApi;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using Moov2.Orchard.Location.Models;
using Moov2.Orchard.Location.Utilities;
using Orchard;
using Orchard.ContentManagement;
using System.Linq;

namespace Moov2.Orchard.Location.Services
{
    public class GoogleGeocodeService : IGeocodeService
    {
        #region Dependencies
        private readonly IWorkContextAccessor _workContextAccessor;
        #endregion

        #region Constructor
        public GoogleGeocodeService(IWorkContextAccessor workContextAccessor)
        {
            _workContextAccessor = workContextAccessor;
        }
        #endregion

        #region IGeocodeService Implementation
        public LocationPart GeocodeIfRequired(LocationPart part)
        {
            if (part == null || string.IsNullOrWhiteSpace(GetApiKey()))
                return part;
            // Don't geocode if we already have coordinate values
            if (!string.IsNullOrWhiteSpace(part.Latitude) || !string.IsNullOrWhiteSpace(part.Longitude))
                return part;
            var address = LocationUtilities.AddressForLocation(part);
            var response = GoogleMaps.Geocode.Query(new GeocodingRequest
            {
                Address = address,
                ApiKey = GetApiKey(),
                Sensor = false
            });
            if (response.Status == Status.OK)
            {
                var result = response.Results.First();
                part.Latitude = result.Geometry.Location.Latitude.ToString();
                part.Longitude = result.Geometry.Location.Longitude.ToString();
            }
            return part;
        }
        #endregion

        #region Helpers
        private string GetApiKey()
        {
            var settings = _workContextAccessor.GetContext().CurrentSite.As<GoogleMapSettingsPart>();
            return settings?.ApiKey;
        }
        #endregion
    }
}