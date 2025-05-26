using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSale.Managerment.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatebookmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Genres_CategoryId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Books",
                newName: "GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                newName: "IX_Books_GenreId");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Books",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Genres_GenreId",
                table: "Books",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Genres_GenreId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Books",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                newName: "IX_Books_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Genres_CategoryId",
                table: "Books",
                column: "CategoryId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
