using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotkaAPI.Migrations
{
    /// <inheritdoc />
    public partial class lists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListType",
                table: "List");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListType",
                table: "List",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
