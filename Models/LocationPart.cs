using Orchard.ContentManagement;

namespace Moov2.Orchard.Location.Models
{
    public class LocationPart : ContentPart<LocationPartRecord>
    {
        public string Name { get { return Retrieve(x => x.Name); } set { Store(x => x.Name, value); } }
        public string Company { get { return Retrieve(x => x.Company); } set { Store(x => x.Company, value); } }
        public string UnitApartment { get { return Retrieve(x => x.UnitApartment); } set { Store(x => x.UnitApartment, value); } }
        public string NameOrNumber { get { return Retrieve(x => x.NameOrNumber); } set { Store(x => x.NameOrNumber, value); } }
        public string Street { get { return Retrieve(x => x.Street); } set { Store(x => x.Street, value); } }
        public string Postcode { get { return Retrieve(x => x.Postcode); } set { Store(x => x.Postcode, value); } }
        public string Town { get { return Retrieve(x => x.Town); } set { Store(x => x.Town, value); } }
        public string CountyState { get { return Retrieve(x => x.CountyState); } set { Store(x => x.CountyState, value); } }
        public string Country { get { return Retrieve(x => x.Country); } set { Store(x => x.Country, value); } }

        public string Latitude { get { return Retrieve(x => x.Latitude); } set { Store(x => x.Latitude, value); } }
        public string Longitude { get { return Retrieve(x => x.Longitude); } set { Store(x => x.Longitude, value); } }

        public bool ShowMap { get { return Retrieve(x => x.ShowMap); } set { Store(x => x.ShowMap, value); } }
    }
}