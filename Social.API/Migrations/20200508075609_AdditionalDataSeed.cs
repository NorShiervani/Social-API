using Microsoft.EntityFrameworkCore.Migrations;

namespace Social.API.Migrations
{
    public partial class AdditionalDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "PostId", "Text", "UserId" },
                values: new object[] { 1, 3, "Cool yo!", 1 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "PostId", "Text", "UserId" },
                values: new object[] { 2, 2, "Fast as fuck!", 2 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "PostId", "Text", "UserId" },
                values: new object[] { 3, 3, "Uuugghhh.", 3 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "PostId", "Text", "UserId" },
                values: new object[] { 4, 3, "Haha awesome!", 2 });

            migrationBuilder.InsertData(
                table: "Conversations",
                columns: new[] { "Id", "ConversationName" },
                values: new object[] { 1, "The cool guys!" });

            migrationBuilder.InsertData(
                table: "UserConversators",
                columns: new[] { "Id", "ConversationId", "UserId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "UserConversators",
                columns: new[] { "Id", "ConversationId", "UserId" },
                values: new object[] { 2, 1, 2 });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Text", "UserConversatorId" },
                values: new object[] { 1, "Hello friends!", 1 });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Text", "UserConversatorId" },
                values: new object[] { 3, "What up?!", 1 });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Text", "UserConversatorId" },
                values: new object[] { 5, "Eating breakfast, and staying chill!", 1 });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Text", "UserConversatorId" },
                values: new object[] { 2, "Hello!", 2 });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Text", "UserConversatorId" },
                values: new object[] { 4, "Doing laundry, and you?", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserConversators",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserConversators",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Conversations",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
