using Orchard.ContentManagement.Records;

namespace Moov2.Orchard.Location.Models
{
    public class LocationPartRecord : ContentPartVersionRecord
    {
        public virtual string Name { get; set; }
        public virtual string Company { get; set; }
        public virtual string UnitApartment { get; set; }
        public virtual string NameOrNumber { get; set; }
        public virtual string Street { get; set; }
        public virtual string Postcode { get; set; }
        public virtual string Town { get; set; }
        public virtual string CountyState { get; set; }
        public virtual string Country { get; set; }

        public virtual double? Latitude { get; set; }
        public virtual double? Longitude { get; set; }

        public virtual bool ShowMap { get; set; }
        public virtual bool ShowMapLink { get; set; }
    }
}