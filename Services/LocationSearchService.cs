using Moov2.Orchard.Location.Models;
using Orchard;
using Orchard.ContentManagement;

namespace Moov2.Orchard.Location.Services
{
    public class LocationSearchService : ILocationSearchService
    {
        #region Dependencies
        private readonly IWorkContextAccessor _workContextAccessor;
        #endregion

        #region Constructor
        public LocationSearchService(IWorkContextAccessor workContextAccessor)
        {
            _workContextAccessor = workContextAccessor;
        }
        #endregion

        #region Implementation
        public double DefaultRadius => LocationSettingsPart?.DefaultRadius ?? 1.5;

        public float DefaultWeighting => LocationSettingsPart?.DefaultWeighting ?? 1.0f;

        public bool LocationSearchEnabled => LocationSettingsPart?.UseSpacialSearch ?? true;
        #endregion

        #region Helpers
        private LocationSearchSettingsPart LocationSettingsPart => _workContextAccessor.GetContext()?.CurrentSite?.As<LocationSearchSettingsPart>();
        #endregion
    }

    public interface ILocationSearchService : IDependency
    {
        double DefaultRadius { get; }
        float DefaultWeighting { get; }
        bool LocationSearchEnabled { get; }
    }
}