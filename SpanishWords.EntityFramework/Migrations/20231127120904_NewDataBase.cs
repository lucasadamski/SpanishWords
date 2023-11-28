using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpanishWords.EntityFramework.Migrations
{
    public partial class NewDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GrammaticalGenders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrammaticalGenders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LexicalCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LexicalCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimesCorrect = table.Column<int>(type: "int", nullable: false),
                    TimesIncorrect = table.Column<int>(type: "int", nullable: false),
                    TimesTrained = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Spanish = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    English = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    LexicalCategoryId = table.Column<int>(type: "int", nullable: false),
                    GrammaticalGenderId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StatisticId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Words_GrammaticalGenders_GrammaticalGenderId",
                        column: x => x.GrammaticalGenderId,
                        principalTable: "GrammaticalGenders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Words_LexicalCategories_LexicalCategoryId",
                        column: x => x.LexicalCategoryId,
                        principalTable: "LexicalCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Words_Statistics_StatisticId",
                        column: x => x.StatisticId,
                        principalTable: "Statistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Words_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GrammaticalGenders",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Masculine" },
                    { 2, "Masculine" }
                });

            migrationBuilder.InsertData(
                table: "LexicalCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Noun" },
                    { 2, "Verb" },
                    { 3, "Adjective" }
                });

            migrationBuilder.InsertData(
                table: "Statistics",
                columns: new[] { "Id", "CreateDate", "DeleteTime", "LastUpdated", "TimesCorrect", "TimesIncorrect", "TimesTrained" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, 0 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, 0 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, 0 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, 0 },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, 0 },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, 0 },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, 0 },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, 0 },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, 0 },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, 0 },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, 0 },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, 0 },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, 0 },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, 0 },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "Password" },
                values: new object[,]
                {
                    { 1, "Luki", "1234" },
                    { 2, "Zdzichu", "jabol1234" },
                    { 3, "Miroslaw", "karpackieMocne" }
                });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "English", "GrammaticalGenderId", "LexicalCategoryId", "Spanish", "StatisticId", "UserId" },
                values: new object[] { 1, "car", 1, 1, "coche", 1, 1 });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "English", "GrammaticalGenderId", "LexicalCategoryId", "Spanish", "StatisticId", "UserId" },
                values: new object[] { 2, "cat", 1, 1, "gato", 2, 1 });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "English", "GrammaticalGenderId", "LexicalCategoryId", "Spanish", "StatisticId", "UserId" },
                values: new object[] { 3, "dog", 1, 1, "perro", 3, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Words_GrammaticalGenderId",
                table: "Words",
                column: "GrammaticalGenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_LexicalCategoryId",
                table: "Words",
                column: "LexicalCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_StatisticId",
                table: "Words",
                column: "StatisticId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Words_UserId",
                table: "Words",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "GrammaticalGenders");

            migrationBuilder.DropTable(
                name: "LexicalCategories");

            migrationBuilder.DropTable(
                name: "Statistics");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
