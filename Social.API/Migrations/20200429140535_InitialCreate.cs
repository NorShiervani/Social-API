using Microsoft.EntityFrameworkCore.Migrations;

namespace Social.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fake",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fake", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Fake",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Bill" });

            migrationBuilder.InsertData(
                table: "Fake",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Shaun" });

            migrationBuilder.InsertData(
                table: "Fake",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Hillary" });

            migrationBuilder.InsertData(
                table: "Fake",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Emma" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fake");
        }
    }
}
