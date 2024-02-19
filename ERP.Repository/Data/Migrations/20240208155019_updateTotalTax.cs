using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateTotalTax : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxTotal",
                table: "Invoices",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "SubTotal",
                table: "Invoices",
                newName: "TaxValue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Invoices",
                newName: "TaxTotal");

            migrationBuilder.RenameColumn(
                name: "TaxValue",
                table: "Invoices",
                newName: "SubTotal");
        }
    }
}
