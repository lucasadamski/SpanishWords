using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkDataAccess.Migrations
{
    public partial class RelationshipsUpdated2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Statistics");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Statistics",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
