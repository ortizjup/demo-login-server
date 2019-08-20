﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.API.Migrations
{
    public partial class FixedRelationShiptBetweenUserAndLocation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Countries",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "States",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                table: "Users",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryId",
                table: "Users",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StateId",
                table: "Users",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_City_CityId",
                table: "Users",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Country_CountryId",
                table: "Users",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_State_StateId",
                table: "Users",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_City_CityId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Country_CountryId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_State_StateId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CountryId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_StateId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Cities",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Countries",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "States",
                table: "Users",
                nullable: false,
                defaultValue: 0);

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Country_Countries",
                table: "Users",
                column: "Countries",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_State_States",
                table: "Users",
                column: "States",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
