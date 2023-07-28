using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDExample.Migrations
{
    /// <inheritdoc />
    public partial class update_TIN_name_datatype_and_add_constraint_to_tin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TIN",
                table: "persons",
                newName: "tax_identification_number");

            migrationBuilder.AlterColumn<string>(
                name: "tax_identification_number",
                table: "persons",
                type: "varchar(8)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CHK_TIN",
                table: "persons",
                sql: "length(tax_identification_number) = 8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHK_TIN",
                table: "persons");

            migrationBuilder.RenameColumn(
                name: "tax_identification_number",
                table: "persons",
                newName: "TIN");

            migrationBuilder.AlterColumn<string>(
                name: "TIN",
                table: "persons",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldNullable: true);
        }
    }
}
