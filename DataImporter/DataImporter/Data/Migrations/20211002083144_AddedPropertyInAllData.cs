using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.Data.Migrations
{
    public partial class AddedPropertyInAllData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "AllDatas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "AllDatas");
        }
    }
}
