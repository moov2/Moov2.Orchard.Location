using Orchard.Indexing;

namespace Moov2.Orchard.Location.Search
{
    public class LocationSearchHelpers
    {
        public static ISearchBuilder SetGeneralQuery(string query, ISearchBuilder searchBuilder)
        {
            if (string.IsNullOrWhiteSpace(query))
                return searchBuilder;

            searchBuilder.Parse(new[] { Constants.CompanyIndexPropertyName, Constants.CountryIndexPropertyName, Constants.CountyStateIndexPropertyName, Constants.PostcodeIndexPropertyName, Constants.TownIndexPropertyName }, query);

            return searchBuilder;
        }

        public static ISearchBuilder SetLocationParameters(ILocationSearchParameters parameters, ISearchBuilder searchBuilder)
        {
            if (parameters == null)
                return searchBuilder;

            searchBuilder = SetGeneralQuery(parameters.Query, searchBuilder);

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