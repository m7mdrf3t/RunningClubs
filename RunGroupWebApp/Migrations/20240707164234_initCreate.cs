using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RunGroupWebApp.Migrations
{
    /// <inheritdoc />
    public partial class initCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Street = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    state = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    pace = table.Column<int>(type: "integer", nullable: true),
                    Millage = table.Column<int>(type: "integer", nullable: true),
                    adressId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.id);
                    table.ForeignKey(
                        name: "FK_AppUser_Adresses_adressId",
                        column: x => x.adressId,
                        principalTable: "Adresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    AdressId = table.Column<int>(type: "integer", nullable: false),
                    clubcategory = table.Column<int>(type: "integer", nullable: false),
                    AppuserID = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.id);
                    table.ForeignKey(
                        name: "FK_Clubs_Adresses_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clubs_AppUser_AppuserID",
                        column: x => x.AppuserID,
                        principalTable: "AppUser",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    AdressId = table.Column<int>(type: "integer", nullable: false),
                    clubcategory = table.Column<int>(type: "integer", nullable: false),
                    AppuserID = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.id);
                    table.ForeignKey(
                        name: "FK_Races_Adresses_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Races_AppUser_AppuserID",
                        column: x => x.AppuserID,
                        principalTable: "AppUser",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_adressId",
                table: "AppUser",
                column: "adressId");

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_AdressId",
                table: "Clubs",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_AppuserID",
                table: "Clubs",
                column: "AppuserID");

            migrationBuilder.CreateIndex(
                name: "IX_Races_AdressId",
                table: "Races",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_AppuserID",
                table: "Races",
                column: "AppuserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "Adresses");
        }
    }
}
