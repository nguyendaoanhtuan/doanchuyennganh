using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTQuanLyHoSoSucKhoe.Migrations
{
    /// <inheritdoc />
    public partial class updateVer30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_don_thuocs_ho_so_benh_an_HoSoBenhAnId",
                table: "don_thuocs");

            migrationBuilder.RenameColumn(
                name: "HoSoBenhAnId",
                table: "don_thuocs",
                newName: "hoSoBenhAnId");

            migrationBuilder.RenameIndex(
                name: "IX_don_thuocs_HoSoBenhAnId",
                table: "don_thuocs",
                newName: "IX_don_thuocs_hoSoBenhAnId");

            migrationBuilder.AddColumn<int>(
                name: "BacSiId",
                table: "phieu_chi_dinhs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "chuyenKhoaId",
                table: "phieu_chi_dinhs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "bacSiId",
                table: "ho_so_benh_an",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "chuyenKhoaId",
                table: "ho_so_benh_an",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "hoSoBenhAnId",
                table: "don_thuocs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "bacSiId",
                table: "don_thuocs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "taoHoSo",
                table: "appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_phieu_chi_dinhs_BacSiId",
                table: "phieu_chi_dinhs",
                column: "BacSiId");

            migrationBuilder.CreateIndex(
                name: "IX_phieu_chi_dinhs_chuyenKhoaId",
                table: "phieu_chi_dinhs",
                column: "chuyenKhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_ho_so_benh_an_bacSiId",
                table: "ho_so_benh_an",
                column: "bacSiId");

            migrationBuilder.CreateIndex(
                name: "IX_ho_so_benh_an_chuyenKhoaId",
                table: "ho_so_benh_an",
                column: "chuyenKhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_don_thuocs_bacSiId",
                table: "don_thuocs",
                column: "bacSiId");

            migrationBuilder.AddForeignKey(
                name: "FK_don_thuocs_bac_sis_bacSiId",
                table: "don_thuocs",
                column: "bacSiId",
                principalTable: "bac_sis",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_don_thuocs_ho_so_benh_an_hoSoBenhAnId",
                table: "don_thuocs",
                column: "hoSoBenhAnId",
                principalTable: "ho_so_benh_an",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ho_so_benh_an_bac_sis_bacSiId",
                table: "ho_so_benh_an",
                column: "bacSiId",
                principalTable: "bac_sis",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ho_so_benh_an_chuyen_khoa_chuyenKhoaId",
                table: "ho_so_benh_an",
                column: "chuyenKhoaId",
                principalTable: "chuyen_khoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_phieu_chi_dinhs_bac_sis_BacSiId",
                table: "phieu_chi_dinhs",
                column: "BacSiId",
                principalTable: "bac_sis",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_phieu_chi_dinhs_chuyen_khoa_chuyenKhoaId",
                table: "phieu_chi_dinhs",
                column: "chuyenKhoaId",
                principalTable: "chuyen_khoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_don_thuocs_bac_sis_bacSiId",
                table: "don_thuocs");

            migrationBuilder.DropForeignKey(
                name: "FK_don_thuocs_ho_so_benh_an_hoSoBenhAnId",
                table: "don_thuocs");

            migrationBuilder.DropForeignKey(
                name: "FK_ho_so_benh_an_bac_sis_bacSiId",
                table: "ho_so_benh_an");

            migrationBuilder.DropForeignKey(
                name: "FK_ho_so_benh_an_chuyen_khoa_chuyenKhoaId",
                table: "ho_so_benh_an");

            migrationBuilder.DropForeignKey(
                name: "FK_phieu_chi_dinhs_bac_sis_BacSiId",
                table: "phieu_chi_dinhs");

            migrationBuilder.DropForeignKey(
                name: "FK_phieu_chi_dinhs_chuyen_khoa_chuyenKhoaId",
                table: "phieu_chi_dinhs");

            migrationBuilder.DropIndex(
                name: "IX_phieu_chi_dinhs_BacSiId",
                table: "phieu_chi_dinhs");

            migrationBuilder.DropIndex(
                name: "IX_phieu_chi_dinhs_chuyenKhoaId",
                table: "phieu_chi_dinhs");

            migrationBuilder.DropIndex(
                name: "IX_ho_so_benh_an_bacSiId",
                table: "ho_so_benh_an");

            migrationBuilder.DropIndex(
                name: "IX_ho_so_benh_an_chuyenKhoaId",
                table: "ho_so_benh_an");

            migrationBuilder.DropIndex(
                name: "IX_don_thuocs_bacSiId",
                table: "don_thuocs");

            migrationBuilder.DropColumn(
                name: "BacSiId",
                table: "phieu_chi_dinhs");

            migrationBuilder.DropColumn(
                name: "chuyenKhoaId",
                table: "phieu_chi_dinhs");

            migrationBuilder.DropColumn(
                name: "bacSiId",
                table: "ho_so_benh_an");

            migrationBuilder.DropColumn(
                name: "chuyenKhoaId",
                table: "ho_so_benh_an");

            migrationBuilder.DropColumn(
                name: "bacSiId",
                table: "don_thuocs");

            migrationBuilder.DropColumn(
                name: "taoHoSo",
                table: "appointments");

            migrationBuilder.RenameColumn(
                name: "hoSoBenhAnId",
                table: "don_thuocs",
                newName: "HoSoBenhAnId");

            migrationBuilder.RenameIndex(
                name: "IX_don_thuocs_hoSoBenhAnId",
                table: "don_thuocs",
                newName: "IX_don_thuocs_HoSoBenhAnId");

            migrationBuilder.AlterColumn<int>(
                name: "HoSoBenhAnId",
                table: "don_thuocs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_don_thuocs_ho_so_benh_an_HoSoBenhAnId",
                table: "don_thuocs",
                column: "HoSoBenhAnId",
                principalTable: "ho_so_benh_an",
                principalColumn: "Id");
        }
    }
}
