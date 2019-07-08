using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.API.Migrations
{
    public partial class Addedfielsandconstrainsttouserentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Users",
                newName: "StateCode");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Users",
                newName: "CountryCode");

            migrationBuilder.AddColumn<int>(
                name: "Cities",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityCode",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Countries",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "States",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Cities",
                table: "Users",
                column: "Cities");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Countries",
                table: "Users",
                column: "Countries");

            migrationBuilder.CreateIndex(
                name: "IX_Users_States",
                table: "Users",
                column: "States");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_City_Cities",
                table: "Users",
                column: "Cities",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Country_Countries",
                table: "Users",
                column: "Countries",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_State_States",
                table: "Users",
                column: "States",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_City_Cities",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Country_Countries",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_State_States",
                table: "Users");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropIndex(
                name: "IX_Users_Cities",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Countries",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_States",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Cities",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CityCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Countries",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "States",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "StateCode",
                table: "Users",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "CountryCode",
                table: "Users",
                newName: "City");
        }
    }
}
