using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CleanArchData");

            migrationBuilder.CreateTable(
                name: "Members",
                schema: "CleanArchData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName_Value = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName_Value = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email_Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members",
                schema: "CleanArchData");
        }
    }
}
