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

                    .Column<double>("Latitude", c => c.WithPrecision(9).WithScale(6))
                    .Column<double>("Longitude", c => c.WithPrecision(9).WithScale(6))

                    .Column<bool>("ShowMap", c => c.NotNull().WithDefault(false))
                    .Column<bool>("ShowMapLink", c => c.NotNull().WithDefault(false))
                );

            ContentDefinitionManager.AlterPartDefinition("LocationPart", builder => builder
                .WithDescription("Adds location fields to a content type.")
                .Attachable());

            return 4;
        }

        public int UpdateFrom1()
        {
            SchemaBuilder.AlterTable("LocationPartRecord",
                table => table
                    .AddColumn<bool>("ShowMap", c => c.NotNull().WithDefault(false))
            );

            return 2;
        }

        public int UpdateFrom2()
        {
            SchemaBuilder.AlterTable("LocationPartRecord",
                table => table
                    .AddColumn<bool>("ShowMapLink", c => c.NotNull().WithDefault(false))
            );

            return 3;
        }

        public int UpdateFrom3()
        {
            SchemaBuilder.AlterTable("LocationPartRecord", table => table.DropColumn("Latitude"));
            SchemaBuilder.AlterTable("LocationPartRecord", table => table.DropColumn("Longitude"));

            SchemaBuilder.AlterTable("LocationPartRecord", table => table
                     .AddColumn<double>("Latitude", c => c.Nullable().WithPrecision(9).WithScale(6))
            );
            SchemaBuilder.AlterTable("LocationPartRecord", table => table
                     .AddColumn<double>("Longitude", c => c.Nullable().WithPrecision(9).WithScale(6))
            );

            return 4;
        }
    }
}