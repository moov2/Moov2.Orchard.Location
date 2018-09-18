using System.Collections.Generic;
using System.Web.Mvc;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.UI.Admin.Notification;
using Orchard.UI.Notify;
using Moov2.Orchard.Location.Models;
using Orchard;

namespace Moov2.Orchard.Location.Services
{
    public class MissingSettingsBanner : INotificationProvider
    {
        private readonly IOrchardServices _orchardServices;

        public MissingSettingsBanner(IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public IEnumerable<NotifyEntry> GetNotifications()
        {
            var workContext = _orchardServices.WorkContext;
            var googleMapSettings = workContext.CurrentSite.As<GoogleMapSettingsPart>();

            if (googleMapSettings == null || string.IsNullOrWhiteSpace(googleMapSettings.ApiKey))
            {
                var urlHelper = new UrlHelper(workContext.HttpContext.Request.RequestContext);
                var url = urlHelper.Action("Google", "Admin", new { Area = "Settings" });
                yield return new NotifyEntry { Message = T("The <a href=\"{0}\">Google Maps Api Key</a> need to be configured.", url), Type = NotifyType.Warning };
            }
        }
    }
}