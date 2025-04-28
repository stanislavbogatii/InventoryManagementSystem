using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class lst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "ProductPropertiesId",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ModelName = table.Column<string>(type: "TEXT", nullable: true),
                    Weight = table.Column<double>(type: "REAL", nullable: true),
                    Dimensions = table.Column<string>(type: "TEXT", nullable: true),
                    Color = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProperties", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductPropertiesId",
                table: "Products",
                column: "ProductPropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProperties_ModelName_Weight_Dimensions_Color_Category",
                table: "ProductProperties",
                columns: new[] { "ModelName", "Weight", "Dimensions", "Color", "Category" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductProperties_ProductPropertiesId",
                table: "Products",
                column: "ProductPropertiesId",
                principalTable: "ProductProperties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductProperties_ProductPropertiesId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductProperties");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductPropertiesId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductPropertiesId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
