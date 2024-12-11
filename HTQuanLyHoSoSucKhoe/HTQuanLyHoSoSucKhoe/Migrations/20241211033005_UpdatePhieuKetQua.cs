using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTQuanLyHoSoSucKhoe.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePhieuKetQua : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ho_so_benh_an_benh_vien_BenhVienId",
                table: "ho_so_benh_an");

            migrationBuilder.AddColumn<int>(
                name: "BenhVienId",
                table: "PhieuKetQuas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DonThuoc",
                table: "PhieuKetQuas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DuongDanPhieu",
                table: "PhieuKetQuas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "PhieuKetQuas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayCapNhat",
                table: "PhieuKetQuas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PhieuKetQuas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PhieuKetQuas_BenhVienId",
                table: "PhieuKetQuas",
                column: "BenhVienId");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuKetQuas_UserId",
                table: "PhieuKetQuas",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ho_so_benh_an_benh_vien_BenhVienId",
                table: "ho_so_benh_an",
                column: "BenhVienId",
                principalTable: "benh_vien",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuKetQuas_benh_vien_BenhVienId",
                table: "PhieuKetQuas",
                column: "BenhVienId",
                principalTable: "benh_vien",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuKetQuas_users_UserId",
                table: "PhieuKetQuas",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ho_so_benh_an_benh_vien_BenhVienId",
                table: "ho_so_benh_an");

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuKetQuas_benh_vien_BenhVienId",
                table: "PhieuKetQuas");

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuKetQuas_users_UserId",
                table: "PhieuKetQuas");

            migrationBuilder.DropIndex(
                name: "IX_PhieuKetQuas_BenhVienId",
                table: "PhieuKetQuas");

            migrationBuilder.DropIndex(
                name: "IX_PhieuKetQuas_UserId",
                table: "PhieuKetQuas");

            migrationBuilder.DropColumn(
                name: "BenhVienId",
                table: "PhieuKetQuas");

            migrationBuilder.DropColumn(
                name: "DonThuoc",
                table: "PhieuKetQuas");

            migrationBuilder.DropColumn(
                name: "DuongDanPhieu",
                table: "PhieuKetQuas");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "PhieuKetQuas");

            migrationBuilder.DropColumn(
                name: "NgayCapNhat",
                table: "PhieuKetQuas");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PhieuKetQuas");

            migrationBuilder.AddForeignKey(
                name: "FK_ho_so_benh_an_benh_vien_BenhVienId",
                table: "ho_so_benh_an",
                column: "BenhVienId",
                principalTable: "benh_vien",
                principalColumn: "Id");
        }
    }
}
