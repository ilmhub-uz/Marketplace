using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Services.Organizations.Migrations
{
	/// <inheritdoc />
	public partial class init : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Organizations",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					Name = table.Column<string>(type: "text", nullable: false),
					Description = table.Column<string>(type: "text", nullable: true),
					Logo = table.Column<string>(type: "text", nullable: true),
					Contact = table.Column<string>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Organizations", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "OrganizationsAddress",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					OrganizationId = table.Column<Guid>(type: "uuid", nullable: false),
					Address = table.Column<string>(type: "text", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_OrganizationsAddress", x => x.Id);
					table.ForeignKey(
						name: "FK_OrganizationsAddress_Organizations_OrganizationId",
						column: x => x.OrganizationId,
						principalTable: "Organizations",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "OrganizationsUser",
				columns: table => new
				{
					OrganizationId = table.Column<Guid>(type: "uuid", nullable: false),
					UserId = table.Column<Guid>(type: "uuid", nullable: false),
					UserRole = table.Column<int>(type: "integer", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_OrganizationsUser", x => new { x.UserId, x.OrganizationId });
					table.ForeignKey(
						name: "FK_OrganizationsUser_Organizations_OrganizationId",
						column: x => x.OrganizationId,
						principalTable: "Organizations",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_OrganizationsAddress_OrganizationId",
				table: "OrganizationsAddress",
				column: "OrganizationId");

			migrationBuilder.CreateIndex(
				name: "IX_OrganizationsUser_OrganizationId",
				table: "OrganizationsUser",
				column: "OrganizationId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "OrganizationsAddress");

			migrationBuilder.DropTable(
				name: "OrganizationsUser");

			migrationBuilder.DropTable(
				name: "Organizations");
		}
	}
}
