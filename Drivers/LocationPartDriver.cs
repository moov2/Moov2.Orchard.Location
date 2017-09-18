using Moov2.Orchard.Location.Models;
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
        #endregion

        #region Constructor
        public LocationPartDriver()
        {

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
            if ("Detailed".Equals(displayType))
                return ContentShape("Parts_Location", () => shapeHelper.Parts_Location(part));
            return null;
        }
        #endregion

        #region Editor
        protected override DriverResult Editor(LocationPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Location_Edit", () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(LocationPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

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

        }
        #endregion
        #endregion
    }
}