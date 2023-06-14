﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Services.Chat.Migrations
{
	/// <inheritdoc />
	public partial class Init : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Conversations",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					UserIds = table.Column<List<Guid>>(type: "uuid[]", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Conversations", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Messages",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					ConversationId = table.Column<Guid>(type: "uuid", nullable: false),
					FromUserId = table.Column<Guid>(type: "uuid", nullable: false),
					Text = table.Column<string>(type: "text", nullable: false),
					Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Messages", x => x.Id);
					table.ForeignKey(
						name: "FK_Messages_Conversations_ConversationId",
						column: x => x.ConversationId,
						principalTable: "Conversations",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Messages_ConversationId",
				table: "Messages",
				column: "ConversationId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Messages");

			migrationBuilder.DropTable(
				name: "Conversations");
		}
	}
}
