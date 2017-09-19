using Moov2.Orchard.Location.Models;
using Orchard;

namespace Moov2.Orchard.Location.Services
{
    public interface IMapUrlService : IDependency
    {
        string GetMapUrl(LocationPart part);
    }
}
