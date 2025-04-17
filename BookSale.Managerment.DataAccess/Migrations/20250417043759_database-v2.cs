using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSale.Managerment.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class databasev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "BookImages");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "BookImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AvatarId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CloudStorages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Account = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Key = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Secret = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloudStorages", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Key = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CloudStorageId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_CloudStorages_CloudStorageId",
                        column: x => x.CloudStorageId,
                        principalTable: "CloudStorages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BookImages_ImageId",
                table: "BookImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_CloudStorageId",
                table: "Files",
                column: "CloudStorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Files_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookImages_Files_ImageId",
                table: "BookImages",
                column: "ImageId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Files_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BookImages_Files_ImageId",
                table: "BookImages");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "CloudStorages");

            migrationBuilder.DropIndex(
                name: "IX_BookImages_ImageId",
                table: "BookImages");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "BookImages");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "BookImages",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
