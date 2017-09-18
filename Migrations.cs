using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace Moov2.Orchard.Location
{
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable("LocationPartRecord",
                table => table
                    .ContentPartVersionRecord()
                    .Column<string>("Name")
                    .Column<string>("Company")
                    .Column<string>("UnitApartment")
                    .Column<string>("NameOrNumber")
                    .Column<string>("Street")
                    .Column<string>("Postcode")
                    .Column<string>("Town")
                    .Column<string>("CountyState")
                    .Column<string>("Country")

                    .Column<string>("Latitude")
                    .Column<string>("Longitude")

                    .Column<bool>("ShowMap", c => c.NotNull().WithDefault(false))
                );

            ContentDefinitionManager.AlterPartDefinition("LocationPart", builder => builder
                .WithDescription("Adds location fields to a content type.")
                .Attachable());

            return 2;
        }

        public int UpdateFrom1()
        {
            SchemaBuilder.AlterTable("LocationPartRecord",
                table => table
                    .AddColumn<bool>("ShowMap", c => c.NotNull().WithDefault(false))
            );

            return 2;
        }
    }
}