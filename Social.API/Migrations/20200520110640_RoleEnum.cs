﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Social.API.Migrations
{
    public partial class RoleEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConversationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    IsSuspended = table.Column<bool>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserConversators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(nullable: true),
                    ConversationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConversators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserConversators_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserConversators_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(nullable: true),
                    PostId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PostId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Likes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(nullable: true),
                    UserConversatorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_UserConversators_UserConversatorId",
                        column: x => x.UserConversatorId,
                        principalTable: "UserConversators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Conversations",
                columns: new[] { "Id", "ConversationName" },
                values: new object[] { 1, "The cool guys!" });

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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "City", "Country", "Email", "Firstname", "IsSuspended", "Lastname", "Password", "Username" },
                values: new object[] { 1, "Brighton", "England", "jd@example.com", "John", false, "Doe", "4321234", "LitteJohn2038" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "City", "Country", "Email", "Firstname", "IsSuspended", "Lastname", "Password", "Username" },
                values: new object[] { 2, "El Paso", "USA", "pp@example.com", "Patrick", false, "Plopinopel", "44321554", "BigMan55" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "City", "Country", "Email", "Firstname", "IsSuspended", "Lastname", "Password", "Username" },
                values: new object[] { 3, "Kiev", "Ukraine", "cmso@example.com", "Svetlana", false, "Orgonsk", "44515214", "CrazyMama72" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Text", "UserId" },
                values: new object[] { 2, "Having the most lovely", 1 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Text", "UserId" },
                values: new object[] { 1, "Hey everybody! You all good?", 2 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Text", "UserId" },
                values: new object[] { 3, "Russia... Is not very nice(to us)...", 3 });

            migrationBuilder.InsertData(
                table: "UserConversators",
                columns: new[] { "Id", "ConversationId", "UserId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "UserConversators",
                columns: new[] { "Id", "ConversationId", "UserId" },
                values: new object[] { 2, 1, 2 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "PostId", "Text", "UserId" },
                values: new object[] { 2, 2, "Fast as fuck!", 2 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "PostId", "Text", "UserId" },
                values: new object[] { 1, 3, "Cool yo!", 1 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "PostId", "Text", "UserId" },
                values: new object[] { 3, 3, "Uuugghhh.", 3 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "PostId", "Text", "UserId" },
                values: new object[] { 4, 3, "Haha awesome!", 2 });

            migrationBuilder.InsertData(
                table: "Likes",
                columns: new[] { "Id", "PostId", "UserId" },
                values: new object[] { 2, 2, 1 });

            migrationBuilder.InsertData(
                table: "Likes",
                columns: new[] { "Id", "PostId", "UserId" },
                values: new object[] { 1, 1, 2 });

            migrationBuilder.InsertData(
                table: "Likes",
                columns: new[] { "Id", "PostId", "UserId" },
                values: new object[] { 3, 1, 3 });

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

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_PostId",
                table: "Likes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserConversatorId",
                table: "Messages",
                column: "UserConversatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConversators_ConversationId",
                table: "UserConversators",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConversators_UserId",
                table: "UserConversators",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Fake");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "UserConversators");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
