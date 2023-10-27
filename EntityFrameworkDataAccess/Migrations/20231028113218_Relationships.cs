using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkDataAccess.Migrations
{
    public partial class Relationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GrammaticalGenderId",
                table: "Words",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LexicalCategoryId",
                table: "Words",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Statistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserWord",
                columns: table => new
                {
                    UsersId = table.Column<int>(type: "int", nullable: false),
                    WordsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWord", x => new { x.UsersId, x.WordsId });
                    table.ForeignKey(
                        name: "FK_UserWord_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWord_Words_WordsId",
                        column: x => x.WordsId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Words_GrammaticalGenderId",
                table: "Words",
                column: "GrammaticalGenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_LexicalCategoryId",
                table: "Words",
                column: "LexicalCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_UserId",
                table: "Statistics",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_WordId",
                table: "Statistics",
                column: "WordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserWord_WordsId",
                table: "UserWord",
                column: "WordsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Users_UserId",
                table: "Statistics",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Words_WordId",
                table: "Statistics",
                column: "WordId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Words_GrammaticalGenders_GrammaticalGenderId",
                table: "Words",
                column: "GrammaticalGenderId",
                principalTable: "GrammaticalGenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Words_LexicalCategories_LexicalCategoryId",
                table: "Words",
                column: "LexicalCategoryId",
                principalTable: "LexicalCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Users_UserId",
                table: "Statistics");

            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Words_WordId",
                table: "Statistics");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_GrammaticalGenders_GrammaticalGenderId",
                table: "Words");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_LexicalCategories_LexicalCategoryId",
                table: "Words");

            migrationBuilder.DropTable(
                name: "UserWord");

            migrationBuilder.DropIndex(
                name: "IX_Words_GrammaticalGenderId",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_LexicalCategoryId",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_UserId",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_WordId",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "GrammaticalGenderId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "LexicalCategoryId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Statistics");
        }
    }
}
