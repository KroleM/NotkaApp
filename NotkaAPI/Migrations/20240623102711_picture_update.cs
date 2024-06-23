using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotkaAPI.Migrations
{
    /// <inheritdoc />
    public partial class picture_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feed_Picture_PictureId",
                table: "Feed");

            migrationBuilder.AlterColumn<int>(
                name: "PictureId",
                table: "Feed",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Feed_Picture_PictureId",
                table: "Feed",
                column: "PictureId",
                principalTable: "Picture",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feed_Picture_PictureId",
                table: "Feed");

            migrationBuilder.AlterColumn<int>(
                name: "PictureId",
                table: "Feed",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Feed_Picture_PictureId",
                table: "Feed",
                column: "PictureId",
                principalTable: "Picture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
