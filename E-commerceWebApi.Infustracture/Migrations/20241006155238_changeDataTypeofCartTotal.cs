using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerceWebApi.Infustracture.Migrations
{
    public partial class changeDataTypeofCartTotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "CartTotal",
                table: "Carts",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "CartTotal",
                table: "Carts",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
