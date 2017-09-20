using Moov2.Orchard.Location.Models;
using Orchard;
using System.Collections.Generic;

namespace Moov2.Orchard.Location.Services
{
    public interface IGeocodeService : IDependency
    {
        IList<LocationResult> Geocode(string term);
        LocationPart GeocodeIfRequired(LocationPart part);
    }
}
