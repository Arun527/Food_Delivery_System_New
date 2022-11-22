using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliverySystem.Migrations
{
    /// <inheritdoc />
    public partial class addname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShipmentStatus",
                table: "OrderShipmentDetail",
                newName: "TrackingStatus");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryStatus",
                table: "DeliveryPerson",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrackingStatus",
                table: "OrderShipmentDetail",
                newName: "ShipmentStatus");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryStatus",
                table: "DeliveryPerson",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
