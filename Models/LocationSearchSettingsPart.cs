using Orchard.ContentManagement;

namespace Moov2.Orchard.Location.Models
{
    public class LocationSearchSettingsPart : ContentPart
    {
        public double DefaultRadius
        {
            get { return this.Retrieve(x => x.DefaultRadius, defaultValue: 1.5); }
            set { this.Store(x => x.DefaultRadius, value); }
        }

        public float DefaultWeighting
        {
            get { return this.Retrieve(x => x.DefaultWeighting, defaultValue: 1.0f); }
            set { this.Store(x => x.DefaultWeighting, value); }
        }

        public bool UseSpacialSearch
        {
            get { return this.Retrieve(x => x.UseSpacialSearch, defaultValue: true); }
            set { this.Store(x => x.UseSpacialSearch, value); }
        }
    }
}