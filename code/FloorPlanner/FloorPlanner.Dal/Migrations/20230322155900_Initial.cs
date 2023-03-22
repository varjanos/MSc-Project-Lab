using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FloorPlanner.Dal.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TranslationFilePath = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IconFilePath = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateTimeFormat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DecimalSeparator = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Translation",
                columns: table => new
                {
                    Language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ParametersCount = table.Column<int>(type: "int", nullable: false),
                    LastModAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    LastModBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translation", x => new { x.Language, x.Key });
                    table.ForeignKey(
                        name: "FK_Translation_Language",
                        column: x => x.Language,
                        principalTable: "Language",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Domain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageName = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfile_Language_LanguageName",
                        column: x => x.LanguageName,
                        principalTable: "Language",
                        principalColumn: "Name");
                });

            migrationBuilder.InsertData(
                table: "Language",
                columns: new[] { "Name", "DateTimeFormat", "DecimalSeparator", "DisplayName", "IconFilePath", "TranslationFilePath" },
                values: new object[] { "en", "yyyy.MM.dd. HH:mm", ".", "English", "flags/en-US.svg", "Translations/en-US.json" });

            migrationBuilder.InsertData(
                table: "Language",
                columns: new[] { "Name", "DateTimeFormat", "DecimalSeparator", "DisplayName", "IconFilePath", "TranslationFilePath" },
                values: new object[] { "hu", "yyyy.MM.dd. HH:mm", ".", "Hungarian", "flags/hu-HU.svg", "Translations/hu-HU.json" });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_LanguageName",
                table: "UserProfile",
                column: "LanguageName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Translation");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "Language");
        }
    }
}
