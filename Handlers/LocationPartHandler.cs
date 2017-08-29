using Moov2.Orchard.Location.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Moov2.Orchard.Location.Handlers
{
    public class LocationPartHandler : ContentHandler
    {
        public LocationPartHandler(IRepository<LocationPartRecord> repo)
        {
            Filters.Add(StorageFilter.For(repo));
        }
    }
}