using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkDataAccess.Migrations
{
    public partial class RelationshipsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Users_UserId",
                table: "Statistics");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_GrammaticalGenders_GrammaticalGenderId",
                table: "Words");

            migrationBuilder.DropTable(
                name: "UserWord");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_UserId",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "LexicalId",
                table: "Words");

            migrationBuilder.AlterColumn<int>(
                name: "GrammaticalGenderId",
                table: "Words",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Words_UserId",
                table: "Words",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_GrammaticalGenders_GrammaticalGenderId",
                table: "Words",
                column: "GrammaticalGenderId",
                principalTable: "GrammaticalGenders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Users_UserId",
                table: "Words",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_GrammaticalGenders_GrammaticalGenderId",
                table: "Words");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Users_UserId",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_UserId",
                table: "Words");

            migrationBuilder.AlterColumn<int>(
                name: "GrammaticalGenderId",
                table: "Words",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "Words",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LexicalId",
                table: "Words",
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
                name: "IX_Statistics_UserId",
                table: "Statistics",
                column: "UserId");

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
                name: "FK_Words_GrammaticalGenders_GrammaticalGenderId",
                table: "Words",
                column: "GrammaticalGenderId",
                principalTable: "GrammaticalGenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
