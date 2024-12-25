using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTQuanLyHoSoSucKhoe.Migrations
{
    /// <inheritdoc />
    public partial class updatever4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_don_thuocs_PhieuKetQuas_PhieuKetQuaId",
                table: "don_thuocs");

            migrationBuilder.DropForeignKey(
                name: "FK_phieu_chi_dinhs_ho_so_benh_an_HoSoBenhAnId",
                table: "phieu_chi_dinhs");

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuKetQuas_bac_sis_BacSiId",
                table: "PhieuKetQuas");

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuKetQuas_benh_vien_BenhVienId",
                table: "PhieuKetQuas");

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuKetQuas_ho_so_benh_an_HoSoBenhAnId",
                table: "PhieuKetQuas");

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuKetQuas_loai_phieus_LoaiPhieuId",
                table: "PhieuKetQuas");

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuKetQuas_phieu_chi_dinhs_PhieuChiDinhId",
                table: "PhieuKetQuas");

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuKetQuas_users_UserId",
                table: "PhieuKetQuas");

            migrationBuilder.DropIndex(
                name: "IX_don_thuocs_PhieuKetQuaId",
                table: "don_thuocs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhieuKetQuas",
                table: "PhieuKetQuas");

            migrationBuilder.DropIndex(
                name: "IX_PhieuKetQuas_LoaiPhieuId",
                table: "PhieuKetQuas");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "ho_so_benh_an");

            migrationBuilder.DropColumn(
                name: "PhieuKetQuaId",
                table: "don_thuocs");

            migrationBuilder.DropColumn(
                name: "DuongDanPhieu",
                table: "PhieuKetQuas");

            migrationBuilder.DropColumn(
                name: "LoaiPhieuId",
                table: "PhieuKetQuas");

            migrationBuilder.DropColumn(
                name: "NgayCapNhat",
                table: "PhieuKetQuas");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "PhieuKetQuas");

            migrationBuilder.RenameTable(
                name: "PhieuKetQuas",
                newName: "phieu_ket_quas");

            migrationBuilder.RenameColumn(
                name: "PhieuChiDinhId",
                table: "phieu_ket_quas",
                newName: "phieuChiDinhId");

            migrationBuilder.RenameColumn(
                name: "HoSoBenhAnId",
                table: "phieu_ket_quas",
                newName: "hoSoBenhAnId");

            migrationBuilder.RenameColumn(
                name: "BacSiId",
                table: "phieu_ket_quas",
                newName: "bacSiId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "phieu_ket_quas",
                newName: "chuyenKhoaId");

            migrationBuilder.RenameColumn(
                name: "GhiChu",
                table: "phieu_ket_quas",
                newName: "duongDanFile3");

            migrationBuilder.RenameColumn(
                name: "BenhVienId",
                table: "phieu_ket_quas",
                newName: "appointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_PhieuKetQuas_UserId",
                table: "phieu_ket_quas",
                newName: "IX_phieu_ket_quas_chuyenKhoaId");

            migrationBuilder.RenameIndex(
                name: "IX_PhieuKetQuas_PhieuChiDinhId",
                table: "phieu_ket_quas",
                newName: "IX_phieu_ket_quas_phieuChiDinhId");

            migrationBuilder.RenameIndex(
                name: "IX_PhieuKetQuas_HoSoBenhAnId",
                table: "phieu_ket_quas",
                newName: "IX_phieu_ket_quas_hoSoBenhAnId");

            migrationBuilder.RenameIndex(
                name: "IX_PhieuKetQuas_BenhVienId",
                table: "phieu_ket_quas",
                newName: "IX_phieu_ket_quas_appointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_PhieuKetQuas_BacSiId",
                table: "phieu_ket_quas",
                newName: "IX_phieu_ket_quas_bacSiId");

            migrationBuilder.AlterColumn<int>(
                name: "LoaiChiDinh",
                table: "phieu_chi_dinhs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "HoSoBenhAnId",
                table: "phieu_chi_dinhs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "appointmentId",
                table: "phieu_chi_dinhs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "hoSoBenhAnId",
                table: "phieu_ket_quas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "duongDanFile1",
                table: "phieu_ket_quas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "duongDanFile2",
                table: "phieu_ket_quas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_phieu_ket_quas",
                table: "phieu_ket_quas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_phieu_chi_dinhs_appointmentId",
                table: "phieu_chi_dinhs",
                column: "appointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_phieu_chi_dinhs_appointments_appointmentId",
                table: "phieu_chi_dinhs",
                column: "appointmentId",
                principalTable: "appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_phieu_chi_dinhs_ho_so_benh_an_HoSoBenhAnId",
                table: "phieu_chi_dinhs",
                column: "HoSoBenhAnId",
                principalTable: "ho_so_benh_an",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_phieu_ket_quas_appointments_appointmentId",
                table: "phieu_ket_quas",
                column: "appointmentId",
                principalTable: "appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_phieu_ket_quas_bac_sis_bacSiId",
                table: "phieu_ket_quas",
                column: "bacSiId",
                principalTable: "bac_sis",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_phieu_ket_quas_chuyen_khoa_chuyenKhoaId",
                table: "phieu_ket_quas",
                column: "chuyenKhoaId",
                principalTable: "chuyen_khoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_phieu_ket_quas_ho_so_benh_an_hoSoBenhAnId",
                table: "phieu_ket_quas",
                column: "hoSoBenhAnId",
                principalTable: "ho_so_benh_an",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_phieu_ket_quas_phieu_chi_dinhs_phieuChiDinhId",
                table: "phieu_ket_quas",
                column: "phieuChiDinhId",
                principalTable: "phieu_chi_dinhs",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_phieu_chi_dinhs_appointments_appointmentId",
                table: "phieu_chi_dinhs");

            migrationBuilder.DropForeignKey(
                name: "FK_phieu_chi_dinhs_ho_so_benh_an_HoSoBenhAnId",
                table: "phieu_chi_dinhs");

            migrationBuilder.DropForeignKey(
                name: "FK_phieu_ket_quas_appointments_appointmentId",
                table: "phieu_ket_quas");

            migrationBuilder.DropForeignKey(
                name: "FK_phieu_ket_quas_bac_sis_bacSiId",
                table: "phieu_ket_quas");

            migrationBuilder.DropForeignKey(
                name: "FK_phieu_ket_quas_chuyen_khoa_chuyenKhoaId",
                table: "phieu_ket_quas");

            migrationBuilder.DropForeignKey(
                name: "FK_phieu_ket_quas_ho_so_benh_an_hoSoBenhAnId",
                table: "phieu_ket_quas");

            migrationBuilder.DropForeignKey(
                name: "FK_phieu_ket_quas_phieu_chi_dinhs_phieuChiDinhId",
                table: "phieu_ket_quas");

            migrationBuilder.DropIndex(
                name: "IX_phieu_chi_dinhs_appointmentId",
                table: "phieu_chi_dinhs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_phieu_ket_quas",
                table: "phieu_ket_quas");

            migrationBuilder.DropColumn(
                name: "appointmentId",
                table: "phieu_chi_dinhs");

            migrationBuilder.DropColumn(
                name: "duongDanFile1",
                table: "phieu_ket_quas");

            migrationBuilder.DropColumn(
                name: "duongDanFile2",
                table: "phieu_ket_quas");

            migrationBuilder.RenameTable(
                name: "phieu_ket_quas",
                newName: "PhieuKetQuas");

            migrationBuilder.RenameColumn(
                name: "phieuChiDinhId",
                table: "PhieuKetQuas",
                newName: "PhieuChiDinhId");

            migrationBuilder.RenameColumn(
                name: "hoSoBenhAnId",
                table: "PhieuKetQuas",
                newName: "HoSoBenhAnId");

            migrationBuilder.RenameColumn(
                name: "bacSiId",
                table: "PhieuKetQuas",
                newName: "BacSiId");

            migrationBuilder.RenameColumn(
                name: "duongDanFile3",
                table: "PhieuKetQuas",
                newName: "GhiChu");

            migrationBuilder.RenameColumn(
                name: "chuyenKhoaId",
                table: "PhieuKetQuas",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "appointmentId",
                table: "PhieuKetQuas",
                newName: "BenhVienId");

            migrationBuilder.RenameIndex(
                name: "IX_phieu_ket_quas_phieuChiDinhId",
                table: "PhieuKetQuas",
                newName: "IX_PhieuKetQuas_PhieuChiDinhId");

            migrationBuilder.RenameIndex(
                name: "IX_phieu_ket_quas_hoSoBenhAnId",
                table: "PhieuKetQuas",
                newName: "IX_PhieuKetQuas_HoSoBenhAnId");

            migrationBuilder.RenameIndex(
                name: "IX_phieu_ket_quas_chuyenKhoaId",
                table: "PhieuKetQuas",
                newName: "IX_PhieuKetQuas_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_phieu_ket_quas_bacSiId",
                table: "PhieuKetQuas",
                newName: "IX_PhieuKetQuas_BacSiId");

            migrationBuilder.RenameIndex(
                name: "IX_phieu_ket_quas_appointmentId",
                table: "PhieuKetQuas",
                newName: "IX_PhieuKetQuas_BenhVienId");

            migrationBuilder.AlterColumn<string>(
                name: "LoaiChiDinh",
                table: "phieu_chi_dinhs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HoSoBenhAnId",
                table: "phieu_chi_dinhs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "ho_so_benh_an",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhieuKetQuaId",
                table: "don_thuocs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "HoSoBenhAnId",
                table: "PhieuKetQuas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DuongDanPhieu",
                table: "PhieuKetQuas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LoaiPhieuId",
                table: "PhieuKetQuas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayCapNhat",
                table: "PhieuKetQuas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "PhieuKetQuas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhieuKetQuas",
                table: "PhieuKetQuas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_don_thuocs_PhieuKetQuaId",
                table: "don_thuocs",
                column: "PhieuKetQuaId");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuKetQuas_LoaiPhieuId",
                table: "PhieuKetQuas",
                column: "LoaiPhieuId");

            migrationBuilder.AddForeignKey(
                name: "FK_don_thuocs_PhieuKetQuas_PhieuKetQuaId",
                table: "don_thuocs",
                column: "PhieuKetQuaId",
                principalTable: "PhieuKetQuas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_phieu_chi_dinhs_ho_so_benh_an_HoSoBenhAnId",
                table: "phieu_chi_dinhs",
                column: "HoSoBenhAnId",
                principalTable: "ho_so_benh_an",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuKetQuas_bac_sis_BacSiId",
                table: "PhieuKetQuas",
                column: "BacSiId",
                principalTable: "bac_sis",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuKetQuas_benh_vien_BenhVienId",
                table: "PhieuKetQuas",
                column: "BenhVienId",
                principalTable: "benh_vien",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuKetQuas_ho_so_benh_an_HoSoBenhAnId",
                table: "PhieuKetQuas",
                column: "HoSoBenhAnId",
                principalTable: "ho_so_benh_an",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuKetQuas_loai_phieus_LoaiPhieuId",
                table: "PhieuKetQuas",
                column: "LoaiPhieuId",
                principalTable: "loai_phieus",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuKetQuas_phieu_chi_dinhs_PhieuChiDinhId",
                table: "PhieuKetQuas",
                column: "PhieuChiDinhId",
                principalTable: "phieu_chi_dinhs",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuKetQuas_users_UserId",
                table: "PhieuKetQuas",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");
        }
    }
}
