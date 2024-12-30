using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace practiceApi.Migrations
{
    /// <inheritdoc />
    public partial class AddandSeedVillaAndAddComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VillaTbl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sqft = table.Column<int>(type: "int", nullable: false),
                    Occupency = table.Column<int>(type: "int", nullable: false),
                    Imageurl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amenity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillaTbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommentsTbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    VillaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentsTbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentsTbl_VillaTbl_VillaId",
                        column: x => x.VillaId,
                        principalTable: "VillaTbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "VillaTbl",
                columns: new[] { "Id", "Amenity", "Details", "Imageurl", "Name", "Occupency", "Rate", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("3325d1bd-7767-411d-851b-3e142bb13b7e"), "", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa5.jpg", "Diamond Villa", 5, "500", 750, null },
                    { new Guid("5232d126-55b8-4bff-8785-073fe44d7cbe"), "", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa1.jpg", "Premium Pool Villa", 5, "300", 550, null },
                    { new Guid("c7fff27e-093c-4914-9d12-74468441ccb8"), "", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg", "Royal Villa", 5, "200", 550, null },
                    { new Guid("c885e0e9-5dd4-43d2-a4c4-22f930d624c7"), "", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa4.jpg", "Luxury Pool Villa", 5, "400", 450, null },
                    { new Guid("d1382bfb-3ba0-4b3f-b9e6-cf632ceded4d"), "", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa2.jpg", "Diamond Pool Villa", 10, "900", 950, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentsTbl_VillaId",
                table: "CommentsTbl",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentsTbl");

            migrationBuilder.DropTable(
                name: "VillaTbl");
        }
    }
}
