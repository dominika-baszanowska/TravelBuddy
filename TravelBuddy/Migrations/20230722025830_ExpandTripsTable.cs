using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBuddy.Migrations
{
    /// <inheritdoc />
    public partial class ExpandTripsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Trips",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_CityId",
                table: "Trips",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_CountryId",
                table: "Trips",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Cities_CityId",
                table: "Trips",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Countries_CountryId",
                table: "Trips",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Cities_CityId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Countries_CountryId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_CityId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_CountryId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Trips");
        }
    }
}
