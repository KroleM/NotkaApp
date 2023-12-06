using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotkaAPI.Migrations
{
    /// <inheritdoc />
    public partial class M3_nullable_note_picture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Picture_PictureId",
                table: "Note");
            
            migrationBuilder.DropIndex(
                name: "IX_Note_PictureId",
                table: "Note");
            
            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Note");
            */
            migrationBuilder.AlterColumn<int>(
                name: "NoteId",
                table: "Picture",
                type: "int",
                nullable: true);
            /*
            migrationBuilder.CreateIndex(
                name: "IX_Picture_NoteId",
                table: "Picture",
                column: "NoteId",
                unique: true,
                filter: "[NoteId] IS NOT NULL");
            
            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Note_NoteId",
                table: "Picture",
                column: "NoteId",
                principalTable: "Note",
                principalColumn: "Id");
            */
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Note_NoteId",
                table: "Picture");

            migrationBuilder.DropIndex(
                name: "IX_Picture_NoteId",
                table: "Picture");

            migrationBuilder.DropColumn(
                name: "NoteId",
                table: "Picture");

            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "Note",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Note_PictureId",
                table: "Note",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Picture_PictureId",
                table: "Note",
                column: "PictureId",
                principalTable: "Picture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
