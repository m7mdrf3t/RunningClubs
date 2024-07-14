using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunGroupWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddressIndexFaild2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Adresses_AddressID",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "AddressID",
                table: "AspNetUsers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Adresses_AddressID",
                table: "AspNetUsers",
                column: "AddressID",
                principalTable: "Adresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Adresses_AddressID",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "AddressID",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Adresses_AddressID",
                table: "AspNetUsers",
                column: "AddressID",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
