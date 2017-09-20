using GoogleMapsApi;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using Moov2.Orchard.Location.Models;
using Moov2.Orchard.Location.Utilities;
using Orchard;
using Orchard.ContentManagement;
using System.Collections.Generic;
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
        public IList<LocationResult> Geocode(string term)
        {
            var results = new List<LocationResult>();
            if (string.IsNullOrWhiteSpace(term) || string.IsNullOrWhiteSpace(GetApiKey()))
                return results;
            var response = GoogleMaps.Geocode.Query(new GeocodingRequest
            {
                Address = term,
                ApiKey = GetApiKey(),
                Sensor = false
            });
            if (response.Status != Status.OK)
                return results;
            results.AddRange(response.Results.Select(x => new LocationResult {
                Latitude = x.Geometry.Location.Latitude,
                Longitude = x.Geometry.Location.Longitude
            }));
            return results;
        }

        public LocationPart GeocodeIfRequired(LocationPart part)
        {
            if (part == null || string.IsNullOrWhiteSpace(GetApiKey()))
                return part;
            // Don't geocode if we already have coordinate values
            if (part.Latitude.HasValue && part.Longitude.HasValue)
                return part;
            var address = LocationUtilities.AddressForLocation(part);
            var results = Geocode(address);
            if (!results.Any())
                return part;
            var result = results.First();
            part.Latitude = result.Latitude;
            part.Longitude = result.Longitude;
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