using Orchard.ContentManagement;

namespace Moov2.Orchard.Location.Models
{
    public class GoogleMapSettingsPart : ContentPart
    {
        public string ApiKey {
            get { return this.Retrieve(x => x.ApiKey); }
            set { this.Store(x => x.ApiKey, value); }
        }
    }
}