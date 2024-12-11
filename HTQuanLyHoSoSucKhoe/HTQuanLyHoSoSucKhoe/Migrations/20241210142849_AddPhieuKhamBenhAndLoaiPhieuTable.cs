using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTQuanLyHoSoSucKhoe.Migrations
{
    /// <inheritdoc />
    public partial class AddPhieuKhamBenhAndLoaiPhieuTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "chanDoan",
                table: "ho_so_benh_an");

            migrationBuilder.DropColumn(
                name: "hinhAnh",
                table: "ho_so_benh_an");

            migrationBuilder.DropColumn(
                name: "trieuChung",
                table: "ho_so_benh_an");

            migrationBuilder.CreateTable(
                name: "LoaiPhieus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiPhieus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhieuKetQuas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiPhieuId = table.Column<int>(type: "int", nullable: false),
                    BacSiId = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuKetQuas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhieuKetQuas_LoaiPhieus_LoaiPhieuId",
                        column: x => x.LoaiPhieuId,
                        principalTable: "LoaiPhieus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhieuKetQuas_bac_sis_BacSiId",
                        column: x => x.BacSiId,
                        principalTable: "bac_sis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhieuKetQuas_BacSiId",
                table: "PhieuKetQuas",
                column: "BacSiId");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuKetQuas_LoaiPhieuId",
                table: "PhieuKetQuas",
                column: "LoaiPhieuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhieuKetQuas");

            migrationBuilder.DropTable(
                name: "LoaiPhieus");

            migrationBuilder.AddColumn<string>(
                name: "chanDoan",
                table: "ho_so_benh_an",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "hinhAnh",
                table: "ho_so_benh_an",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "trieuChung",
                table: "ho_so_benh_an",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
