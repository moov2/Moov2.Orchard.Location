using Moov2.Orchard.Location.Models;
using Moov2.Orchard.Location.Utilities;
using System;

namespace Moov2.Orchard.Location.Services
{
    public class GoogleMapUrlService : IMapUrlService
    {
        public string GetMapUrl(LocationPart part)
        {
            if (part == null)
                return null;
            if (part.Latitude.HasValue && part.Longitude.HasValue)
            {
                return $"https://www.google.com/maps/search/{part.Latitude.Value.ToString("0.000000")},{part.Longitude.Value.ToString("0.000000")}";
            }
            var address = LocationUtilities.AddressForLocation(part).Replace(Environment.NewLine, ",");
            if (!string.IsNullOrWhiteSpace(address))
            {
                return $"https://www.google.com/maps/search/{address}";
            }
            return null;
        }
    }
}