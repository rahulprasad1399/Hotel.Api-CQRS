using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class employeeConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_hotels_HotelId",
                table: "employees");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_employees_hotels_HotelId",
                table: "employees",
                column: "HotelId",
                principalTable: "hotels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_hotels_HotelId",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "employees");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_employees_hotels_HotelId",
                table: "employees",
                column: "HotelId",
                principalTable: "hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
