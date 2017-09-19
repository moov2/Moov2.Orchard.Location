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
            if (!string.IsNullOrWhiteSpace(part.Latitude) && !string.IsNullOrWhiteSpace(part.Longitude))
            {
                return $"https://www.google.com/maps/search/{part.Latitude},{part.Longitude}";
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