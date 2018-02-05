using Moov2.Orchard.Location.Models;
using Orchard.Indexing;
using System.Collections.Generic;

namespace Moov2.Orchard.Location.Search
{
    public static class SearchBuilderExtensions
    {
        public static ISearchBuilder SetGeneralLocationQuery(this ISearchBuilder searchBuilder, string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return searchBuilder;

            searchBuilder.Parse(new[] { Constants.CompanyIndexPropertyName, Constants.CountryIndexPropertyName, Constants.CountyStateIndexPropertyName, Constants.PostcodeIndexPropertyName, Constants.TownIndexPropertyName }, query);

            return searchBuilder;
        }

        public static ISearchBuilder SetSpacialQuery(this ISearchBuilder searchBuilder, double latitude, double longitude, double radius, float weighting, bool mandatory)
        {
            searchBuilder = searchBuilder.WithinRange(Constants.LatitudeIndexPropertyName, latitude - radius, latitude + radius).Weighted(weighting);
            if (mandatory)
                searchBuilder = searchBuilder.Mandatory();
            searchBuilder = searchBuilder.WithinRange(Constants.LongitudeIndexPropertyName, longitude - radius, longitude + radius).Weighted(weighting);
            if (mandatory)
                searchBuilder = searchBuilder.Mandatory();
            return searchBuilder;
        }

        public static ISearchBuilder SetSpacialQuery(this ISearchBuilder searchBuilder, LocationResult location, double radius, float weighting, bool mandatory)
        {
            return searchBuilder.SetSpacialQuery(location.Latitude, location.Longitude, radius, weighting, mandatory);
        }

        public static ISearchBuilder SetSpacialQuery(this ISearchBuilder searchBuilder, IList<LocationResult> results, double radius, float weighting)
        {
            foreach (var location in results)
            {
                searchBuilder = searchBuilder.SetSpacialQuery(location.Latitude, location.Longitude, radius, weighting, false);
            }
            return searchBuilder;
        }

        public static ISearchBuilder SetLocationParameters(this ISearchBuilder searchBuilder, ILocationSearchParameters parameters)
        {
            if (parameters == null)
                return searchBuilder;

            searchBuilder = searchBuilder.SetGeneralLocationQuery(parameters.Query);

            searchBuilder = SetFieldQuery(Constants.CompanyIndexPropertyName, parameters.Company, searchBuilder);
            searchBuilder = SetFieldQuery(Constants.StreetIndexPropertyName, parameters.Street, searchBuilder);
            searchBuilder = SetFieldQuery(Constants.PostcodeIndexPropertyName, parameters.Postcode, searchBuilder);
            searchBuilder = SetFieldQuery(Constants.TownIndexPropertyName, parameters.Town, searchBuilder);
            searchBuilder = SetFieldQuery(Constants.CountyStateIndexPropertyName, parameters.CountyState, searchBuilder);
            searchBuilder = SetFieldQuery(Constants.CountryIndexPropertyName, parameters.Country, searchBuilder);

            return searchBuilder;
        }

        private static ISearchBuilder SetFieldQuery(string field, string value, ISearchBuilder searchBuilder)
        {
            if (string.IsNullOrWhiteSpace(field) || string.IsNullOrWhiteSpace(value))
                return searchBuilder;

            searchBuilder.WithField(field, value).AsFilter();

            return searchBuilder;
        }
    }
}