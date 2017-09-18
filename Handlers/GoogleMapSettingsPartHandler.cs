using Moov2.Orchard.Location.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization;

namespace Moov2.Orchard.Location.Handlers
{
    public class GoogleMapSettingsPartHandler : ContentHandler
    {
        public GoogleMapSettingsPartHandler()
        {
            T = NullLocalizer.Instance;
            Filters.Add(new ActivatingFilter<GoogleMapSettingsPart>("Site"));
            Filters.Add(new TemplateFilterForPart<GoogleMapSettingsPart>("GoogleMapSettings", "Parts.Location.GoogleMapSettings", "google"));
        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Google")));
        }
    }
}