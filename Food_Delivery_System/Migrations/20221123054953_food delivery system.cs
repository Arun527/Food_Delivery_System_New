using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliverySystem.Migrations
{
    /// <inheritdoc />
    public partial class fooddeliverysystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "TrackingStatusDescription",
                table: "OrderShipmentDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "OrderStatus",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackingStatusDescription",
                table: "OrderShipmentDetail");

            migrationBuilder.AlterColumn<string>(
                name: "OrderStatus",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryStatus",
                table: "DeliveryPerson",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
