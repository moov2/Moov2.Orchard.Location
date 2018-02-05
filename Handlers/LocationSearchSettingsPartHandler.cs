using Moov2.Orchard.Location.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization;

namespace Moov2.Orchard.Location.Handlers
{
    public class LocationSearchSettingsPartHandler : ContentHandler
    {
        public LocationSearchSettingsPartHandler()
        {
            T = NullLocalizer.Instance;
            Filters.Add(new ActivatingFilter<LocationSearchSettingsPart>("Site"));
            Filters.Add(new TemplateFilterForPart<LocationSearchSettingsPart>("LocationSearchSettings", "Parts.Location.LocationSearchSettings", "locationsearch"));
        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Location Search")));
        }
    }
}