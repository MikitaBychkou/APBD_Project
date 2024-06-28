using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class AllClient_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clients_KRS",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_PESEL",
                table: "Clients");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSigned",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsSigned",
                table: "Contracts",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_KRS",
                table: "Clients",
                column: "KRS",
                unique: true,
                filter: "[KRS] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PESEL",
                table: "Clients",
                column: "PESEL",
                unique: true,
                filter: "[PESEL] IS NOT NULL");
        }
    }
}
