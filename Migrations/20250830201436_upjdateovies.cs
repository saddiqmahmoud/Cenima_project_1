﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cenima_project.Migrations
{
    /// <inheritdoc />
    public partial class upjdateovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActorsId",
                table: "ActorMovies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActorsId",
                table: "ActorMovies");
        }
    }
}
