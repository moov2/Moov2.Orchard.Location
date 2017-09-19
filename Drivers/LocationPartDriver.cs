using Moov2.Orchard.Location.Models;
using Moov2.Orchard.Location.Services;
using Moov2.Orchard.Location.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;

namespace Moov2.Orchard.Location.Drivers
{
    public class LocationPartDriver : ContentPartDriver<LocationPart>
    {
        #region Constants
        private const string TemplateName = "Parts.Location.LocationPart";
        #endregion

        #region Dependencies
        private readonly IGeocodeService _geocodeService;
        private readonly IWorkContextAccessor _workContextAccessor;
        #endregion

        #region Constructor
        public LocationPartDriver(IGeocodeService geocodeService, IWorkContextAccessor workContextAccessor)
        {
            _geocodeService = geocodeService;
            _workContextAccessor = workContextAccessor;
        }
        #endregion

        #region Driver
        #region Properties
        protected override string Prefix
        {
            get { return "Location"; }
        }
        #endregion

        #region Display
        protected override DriverResult Display(LocationPart part, string displayType, dynamic shapeHelper)
        {
            var locationShape = ContentShape("Parts_Location", () => shapeHelper.Parts_Location(part));
            if (ShouldRenderMap(part))
            {
                return Combined(
                        locationShape,
                        ContentShape("Parts_Location_Map", () => shapeHelper.Parts_Location_Map(ViewModelForMap(part)))
                    );
            }
            else
            {
                return locationShape;
            }
        }
        #endregion

        #region Editor
        protected override DriverResult Editor(LocationPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Location_Edit", () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(LocationPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            if (updater.TryUpdateModel(part, Prefix, null, null))
            {
                part = _geocodeService.GeocodeIfRequired(part);
            }

            return Editor(part, shapeHelper);
        }
        #endregion

        #region Import Export
        protected override void Importing(LocationPart part, ImportContentContext context)
        {
            if (context.Data.Element(part.PartDefinition.Name) == null)
                return;
            context.ImportAttribute(part.PartDefinition.Name, "Name", x => part.Name = x);
            context.ImportAttribute(part.PartDefinition.Name, "Company", x => part.Company = x);
            context.ImportAttribute(part.PartDefinition.Name, "UnitApartment", x => part.UnitApartment = x);
            context.ImportAttribute(part.PartDefinition.Name, "NameOrNumber", x => part.NameOrNumber = x);
            context.ImportAttribute(part.PartDefinition.Name, "Street", x => part.Street = x);
            context.ImportAttribute(part.PartDefinition.Name, "Postcode", x => part.Postcode = x);
            context.ImportAttribute(part.PartDefinition.Name, "Town", x => part.Town = x);
            context.ImportAttribute(part.PartDefinition.Name, "CountyState", x => part.CountyState = x);
            context.ImportAttribute(part.PartDefinition.Name, "Country", x => part.Country = x);

            context.ImportAttribute(part.PartDefinition.Name, "Latitude", x => part.Latitude = x);
            context.ImportAttribute(part.PartDefinition.Name, "Longitude", x => part.Longitude = x);

            context.ImportAttribute(part.PartDefinition.Name, "ShowMap", x => part.ShowMap = true.ToString().Equals(x, System.StringComparison.InvariantCultureIgnoreCase));
        }

        protected override void Exporting(LocationPart part, ExportContentContext context)
        {
            context.Element(part.PartDefinition.Name).SetAttributeValue("Name", part.Name);
            context.Element(part.PartDefinition.Name).SetAttributeValue("Company", part.Company);
            context.Element(part.PartDefinition.Name).SetAttributeValue("UnitApartment", part.UnitApartment);
            context.Element(part.PartDefinition.Name).SetAttributeValue("NameOrNumber", part.NameOrNumber);
            context.Element(part.PartDefinition.Name).SetAttributeValue("Street", part.Street);
            context.Element(part.PartDefinition.Name).SetAttributeValue("Postcode", part.Postcode);
            context.Element(part.PartDefinition.Name).SetAttributeValue("Town", part.Town);
            context.Element(part.PartDefinition.Name).SetAttributeValue("CountyState", part.CountyState);
            context.Element(part.PartDefinition.Name).SetAttributeValue("Country", part.Country);

            context.Element(part.PartDefinition.Name).SetAttributeValue("Latitude", part.Latitude);
            context.Element(part.PartDefinition.Name).SetAttributeValue("Longitude", part.Longitude);

            context.Element(part.PartDefinition.Name).SetAttributeValue("ShowMap", part.ShowMap);
        }
        #endregion
        #endregion

        #region Helpers
        private string GetApiKey()
        {
            var googleMapSettings = _workContextAccessor.GetContext().CurrentSite.As<GoogleMapSettingsPart>();
            return googleMapSettings?.ApiKey;
        }

        private bool ShouldGeocode()
        {
            var googleMapSettings = _workContextAccessor.GetContext().CurrentSite.As<GoogleMapSettingsPart>();
            return googleMapSettings?.AutomaticGeocoding ?? false;
        }

        private bool ShouldRenderMap(LocationPart part)
        {
            float tester;
            return part != null &&
                part.ShowMap &&
                !string.IsNullOrWhiteSpace(GetApiKey()) &&
                !string.IsNullOrWhiteSpace(part.Latitude) &&
                !string.IsNullOrWhiteSpace(part.Longitude) &&
                float.TryParse(part.Latitude, out tester) &&
                float.TryParse(part.Longitude, out tester);
        }

        private LocationMapViewModel ViewModelForMap(LocationPart part)
        {
            var model = new LocationMapViewModel
            {
                ApiKey = GetApiKey(),
                ContentItemId = part.ContentItem.Id,
                Latitude = part.Latitude,
                Longitude = part.Longitude
            };
            return model;
        }
        #endregion
    }
}