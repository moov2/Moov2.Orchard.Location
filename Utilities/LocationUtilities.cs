using Moov2.Orchard.Location.Models;
using System;
using System.Text;

namespace Moov2.Orchard.Location.Utilities
{
    public static class LocationUtilities
    {
        public static string AddressForLocation(LocationPart location)
        {
            var addressBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(location.Name))
            {
                addressBuilder.AppendLine(location.Name);
            }
            if (!string.IsNullOrWhiteSpace(location.Company))
            {
                addressBuilder.AppendLine(location.Company);
            }
            if (!string.IsNullOrWhiteSpace(location.UnitApartment))
            {
                addressBuilder.AppendLine(location.UnitApartment);
            }
            if (!string.IsNullOrWhiteSpace(location.NameOrNumber))
            {
                addressBuilder.Append(location.NameOrNumber);
            }
            if (!string.IsNullOrWhiteSpace(location.Street))
            {
                if (!string.IsNullOrWhiteSpace(location.NameOrNumber))
                {
                    addressBuilder.Append(" ");
                }
                addressBuilder.AppendLine(location.Street);
            }
            else
            {
                addressBuilder.AppendLine();
            }
            if (!string.IsNullOrWhiteSpace(location.Town))
            {
                addressBuilder.AppendLine(location.Town);
            }
            if (!string.IsNullOrWhiteSpace(location.Postcode))
            {
                addressBuilder.AppendLine(location.Postcode);
            }
            if (!string.IsNullOrWhiteSpace(location.CountyState))
            {
                addressBuilder.AppendLine(location.CountyState);
            }
            if (!string.IsNullOrWhiteSpace(location.Country))
            {
                addressBuilder.AppendLine(location.Country);
            }
            return addressBuilder.ToString();
        }

        public static string AddressForLocationWeb(LocationPart location)
        {
            var address = AddressForLocation(location);
            return address.Replace(Environment.NewLine, "<br/>");
        }
    }
}