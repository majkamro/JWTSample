using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTSample.Database.Migrations
{
    public partial class up3addUId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UId",
                table: "Users");
        }
    }
}
