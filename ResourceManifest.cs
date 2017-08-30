using Orchard.UI.Resources;

namespace Moov2.Orchard.Location
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            builder.Add()
                .DefineScript("LocationJs").SetUrl("orchard.location.min.js", "orchard.location.js");
            builder.Add()
                .DefineStyle("LocationAdminStyle").SetUrl("admin-location.css");
        }
    }
}