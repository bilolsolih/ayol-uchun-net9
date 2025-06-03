using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AyolUchun.Migrations
{
    /// <inheritdoc />
    public partial class social_accounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "social_accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    link = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    icon = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_social_accounts", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_social_accounts_title",
                table: "social_accounts",
                column: "title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "social_accounts");
        }
    }
}
