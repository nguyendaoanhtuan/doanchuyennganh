using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTQuanLyHoSoSucKhoe.Migrations
{
    /// <inheritdoc />
    public partial class ver6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_phieu_chi_dinhs_bac_sis_BacSiId",
                table: "phieu_chi_dinhs");

            migrationBuilder.RenameColumn(
                name: "BacSiId",
                table: "phieu_chi_dinhs",
                newName: "bacSiId");

            migrationBuilder.RenameIndex(
                name: "IX_phieu_chi_dinhs_BacSiId",
                table: "phieu_chi_dinhs",
                newName: "IX_phieu_chi_dinhs_bacSiId");

            migrationBuilder.AlterColumn<int>(
                name: "bacSiId",
                table: "phieu_chi_dinhs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_phieu_chi_dinhs_bac_sis_bacSiId",
                table: "phieu_chi_dinhs",
                column: "bacSiId",
                principalTable: "bac_sis",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_phieu_chi_dinhs_bac_sis_bacSiId",
                table: "phieu_chi_dinhs");

            migrationBuilder.RenameColumn(
                name: "bacSiId",
                table: "phieu_chi_dinhs",
                newName: "BacSiId");

            migrationBuilder.RenameIndex(
                name: "IX_phieu_chi_dinhs_bacSiId",
                table: "phieu_chi_dinhs",
                newName: "IX_phieu_chi_dinhs_BacSiId");

            migrationBuilder.AlterColumn<int>(
                name: "BacSiId",
                table: "phieu_chi_dinhs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_phieu_chi_dinhs_bac_sis_BacSiId",
                table: "phieu_chi_dinhs",
                column: "BacSiId",
                principalTable: "bac_sis",
                principalColumn: "Id");
        }
    }
}
