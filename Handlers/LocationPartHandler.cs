using Moov2.Orchard.Location.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Moov2.Orchard.Location.Handlers
{
    public class LocationPartHandler : ContentHandler
    {
        public LocationPartHandler(IRepository<LocationPartRecord> repo)
        {
            Filters.Add(StorageFilter.For(repo));

            OnIndexing<LocationPart>((context, locationPart) =>
            {
                context.DocumentIndex
                    .Add(Constants.CompanyIndexPropertyName, locationPart.Company).Analyze().Store()
                    .Add(Constants.CountryIndexPropertyName, locationPart.Country).Analyze().Store()
                    .Add(Constants.CountyStateIndexPropertyName, locationPart.CountyState).Analyze().Store()
                    .Add(Constants.LatitudeIndexPropertyName, locationPart.Latitude).Store()
                    .Add(Constants.LongitudeIndexPropertyName, locationPart.Longitude).Store()
                    .Add(Constants.NameIndexPropertyName, locationPart.Name).Analyze().Store()
                    .Add(Constants.NameOrNumberIndexPropertyName, locationPart.NameOrNumber).Store()
                    .Add(Constants.PostcodeIndexPropertyName, locationPart.Postcode).Store()
                    .Add(Constants.StreetIndexPropertyName, locationPart.Street).Analyze().Store()
                    .Add(Constants.TownIndexPropertyName, locationPart.Town).Analyze().Store()
                    .Add(Constants.UnitApartmentIndexPropertyName, locationPart.UnitApartment).Store();
            });
        }
    }
}