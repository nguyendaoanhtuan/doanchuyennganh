using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTQuanLyHoSoSucKhoe.Migrations
{
    /// <inheritdoc />
    public partial class updateThem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhieuKetQuas_LoaiPhieus_LoaiPhieuId",
                table: "PhieuKetQuas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoaiPhieus",
                table: "LoaiPhieus");

            migrationBuilder.DropColumn(
                name: "DonThuoc",
                table: "PhieuKetQuas");

            migrationBuilder.DropColumn(
                name: "thuocDuocKe",
                table: "ho_so_benh_an");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "Phone_Number",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "TenBenhVien",
                table: "appointments");

            migrationBuilder.RenameTable(
                name: "LoaiPhieus",
                newName: "loai_phieus");

            migrationBuilder.AddColumn<int>(
                name: "HoSoBenhAnId",
                table: "PhieuKetQuas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhieuChiDinhId",
                table: "PhieuKetQuas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "GhiChu",
                table: "ho_so_benh_an",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "ho_so_benh_an",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChuyenKhoaId",
                table: "appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HoSoBenhAnId",
                table: "appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoaiDichVuId",
                table: "appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "trangThaiPhieu",
                table: "appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_loai_phieus",
                table: "loai_phieus",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "don_thuocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhieuKetQuaId = table.Column<int>(type: "int", nullable: false),
                    DuongDanDonThuoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoSoBenhAnId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_don_thuocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_don_thuocs_PhieuKetQuas_PhieuKetQuaId",
                        column: x => x.PhieuKetQuaId,
                        principalTable: "PhieuKetQuas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_don_thuocs_ho_so_benh_an_HoSoBenhAnId",
                        column: x => x.HoSoBenhAnId,
                        principalTable: "ho_so_benh_an",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "loai_dich_vu_khams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDichVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loai_dich_vu_khams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "phieu_chi_dinhs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoSoBenhAnId = table.Column<int>(type: "int", nullable: false),
                    LoaiChiDinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonThuoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoaiPhieuId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phieu_chi_dinhs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_phieu_chi_dinhs_ho_so_benh_an_HoSoBenhAnId",
                        column: x => x.HoSoBenhAnId,
                        principalTable: "ho_so_benh_an",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_phieu_chi_dinhs_loai_phieus_LoaiPhieuId",
                        column: x => x.LoaiPhieuId,
                        principalTable: "loai_phieus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "loai_dich_vu_chuyen_khoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiDichVuId = table.Column<int>(type: "int", nullable: false),
                    ChuyenKhoaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loai_dich_vu_chuyen_khoas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_loai_dich_vu_chuyen_khoas_chuyen_khoa_ChuyenKhoaId",
                        column: x => x.ChuyenKhoaId,
                        principalTable: "chuyen_khoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_loai_dich_vu_chuyen_khoas_loai_dich_vu_khams_LoaiDichVuId",
                        column: x => x.LoaiDichVuId,
                        principalTable: "loai_dich_vu_khams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhieuKetQuas_HoSoBenhAnId",
                table: "PhieuKetQuas",
                column: "HoSoBenhAnId");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuKetQuas_PhieuChiDinhId",
                table: "PhieuKetQuas",
                column: "PhieuChiDinhId");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_ChuyenKhoaId",
                table: "appointments",
                column: "ChuyenKhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_HoSoBenhAnId",
                table: "appointments",
                column: "HoSoBenhAnId");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_LoaiDichVuId",
                table: "appointments",
                column: "LoaiDichVuId");

            migrationBuilder.CreateIndex(
                name: "IX_don_thuocs_HoSoBenhAnId",
                table: "don_thuocs",
                column: "HoSoBenhAnId");

            migrationBuilder.CreateIndex(
                name: "IX_don_thuocs_PhieuKetQuaId",
                table: "don_thuocs",
                column: "PhieuKetQuaId");

            migrationBuilder.CreateIndex(
                name: "IX_loai_dich_vu_chuyen_khoas_ChuyenKhoaId",
                table: "loai_dich_vu_chuyen_khoas",
                column: "ChuyenKhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_loai_dich_vu_chuyen_khoas_LoaiDichVuId",
                table: "loai_dich_vu_chuyen_khoas",
                column: "LoaiDichVuId");

            migrationBuilder.CreateIndex(
                name: "IX_phieu_chi_dinhs_HoSoBenhAnId",
                table: "phieu_chi_dinhs",
                column: "HoSoBenhAnId");

            migrationBuilder.CreateIndex(
                name: "IX_phieu_chi_dinhs_LoaiPhieuId",
                table: "phieu_chi_dinhs",
                column: "LoaiPhieuId");

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_chuyen_khoa_ChuyenKhoaId",
                table: "appointments",
                column: "ChuyenKhoaId",
                principalTable: "chuyen_khoa",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_ho_so_benh_an_HoSoBenhAnId",
                table: "appointments",
                column: "HoSoBenhAnId",
                principalTable: "ho_so_benh_an",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_loai_dich_vu_khams_LoaiDichVuId",
                table: "appointments",
                column: "LoaiDichVuId",
                principalTable: "loai_dich_vu_khams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuKetQuas_phieu_chi_dinhs_PhieuChiDinhId",
                table: "PhieuKetQuas",
                column: "PhieuChiDinhId",
                principalTable: "phieu_chi_dinhs",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_chuyen_khoa_ChuyenKhoaId",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_appointments_ho_so_benh_an_HoSoBenhAnId",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_appointments_loai_dich_vu_khams_LoaiDichVuId",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuKetQuas_ho_so_benh_an_HoSoBenhAnId",
                table: "PhieuKetQuas");

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuKetQuas_loai_phieus_LoaiPhieuId",
                table: "PhieuKetQuas");

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuKetQuas_phieu_chi_dinhs_PhieuChiDinhId",
                table: "PhieuKetQuas");

            migrationBuilder.DropTable(
                name: "don_thuocs");

            migrationBuilder.DropTable(
                name: "loai_dich_vu_chuyen_khoas");

            migrationBuilder.DropTable(
                name: "phieu_chi_dinhs");

            migrationBuilder.DropTable(
                name: "loai_dich_vu_khams");

            migrationBuilder.DropIndex(
                name: "IX_PhieuKetQuas_HoSoBenhAnId",
                table: "PhieuKetQuas");

            migrationBuilder.DropIndex(
                name: "IX_PhieuKetQuas_PhieuChiDinhId",
                table: "PhieuKetQuas");

            migrationBuilder.DropIndex(
                name: "IX_appointments_ChuyenKhoaId",
                table: "appointments");

            migrationBuilder.DropIndex(
                name: "IX_appointments_HoSoBenhAnId",
                table: "appointments");

            migrationBuilder.DropIndex(
                name: "IX_appointments_LoaiDichVuId",
                table: "appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_loai_phieus",
                table: "loai_phieus");

            migrationBuilder.DropColumn(
                name: "HoSoBenhAnId",
                table: "PhieuKetQuas");

            migrationBuilder.DropColumn(
                name: "PhieuChiDinhId",
                table: "PhieuKetQuas");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "ho_so_benh_an");

            migrationBuilder.DropColumn(
                name: "ChuyenKhoaId",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "HoSoBenhAnId",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "LoaiDichVuId",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "trangThaiPhieu",
                table: "appointments");

            migrationBuilder.RenameTable(
                name: "loai_phieus",
                newName: "LoaiPhieus");

            migrationBuilder.AddColumn<string>(
                name: "DonThuoc",
                table: "PhieuKetQuas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GhiChu",
                table: "ho_so_benh_an",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "thuocDuocKe",
                table: "ho_so_benh_an",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone_Number",
                table: "appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenBenhVien",
                table: "appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoaiPhieus",
                table: "LoaiPhieus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuKetQuas_LoaiPhieus_LoaiPhieuId",
                table: "PhieuKetQuas",
                column: "LoaiPhieuId",
                principalTable: "LoaiPhieus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
