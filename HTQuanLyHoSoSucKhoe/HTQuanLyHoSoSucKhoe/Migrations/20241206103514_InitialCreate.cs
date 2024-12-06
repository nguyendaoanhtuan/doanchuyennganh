using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTQuanLyHoSoSucKhoe.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vaitro = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "benh_vien",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone_Number = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Image_Path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_benh_vien", x => x.Id);
                    table.ForeignKey(
                        name: "FK_benh_vien_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone_Number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cccd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image_Path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chuyen_khoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BenhVienId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chuyen_khoa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chuyen_khoa_benh_vien_BenhVienId",
                        column: x => x.BenhVienId,
                        principalTable: "benh_vien",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BenhVienId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenBenhVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Appointment_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_appointments_benh_vien_BenhVienId",
                        column: x => x.BenhVienId,
                        principalTable: "benh_vien",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_appointments_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ho_so_benh_an",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BenhVienId = table.Column<int>(type: "int", nullable: false),
                    trieuChung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    chanDoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    thuocDuocKe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ho_so_benh_an", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ho_so_benh_an_benh_vien_BenhVienId",
                        column: x => x.BenhVienId,
                        principalTable: "benh_vien",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ho_so_benh_an_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tai_khoan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    passWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BenhVienId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tai_khoan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tai_khoan_benh_vien_BenhVienId",
                        column: x => x.BenhVienId,
                        principalTable: "benh_vien",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tai_khoan_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "bac_sis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    soDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    diaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image_Path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ChuyenKhoaId = table.Column<int>(type: "int", nullable: false),
                    BenhVienId = table.Column<int>(type: "int", nullable: false),
                    ngayTaoBacSi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bac_sis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bac_sis_benh_vien_BenhVienId",
                        column: x => x.BenhVienId,
                        principalTable: "benh_vien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bac_sis_chuyen_khoa_ChuyenKhoaId",
                        column: x => x.ChuyenKhoaId,
                        principalTable: "chuyen_khoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_BenhVienId",
                table: "appointments",
                column: "BenhVienId");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_UserId",
                table: "appointments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_bac_sis_BenhVienId",
                table: "bac_sis",
                column: "BenhVienId");

            migrationBuilder.CreateIndex(
                name: "IX_bac_sis_ChuyenKhoaId",
                table: "bac_sis",
                column: "ChuyenKhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_benh_vien_RoleId",
                table: "benh_vien",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_chuyen_khoa_BenhVienId",
                table: "chuyen_khoa",
                column: "BenhVienId");

            migrationBuilder.CreateIndex(
                name: "IX_ho_so_benh_an_BenhVienId",
                table: "ho_so_benh_an",
                column: "BenhVienId");

            migrationBuilder.CreateIndex(
                name: "IX_ho_so_benh_an_UserId",
                table: "ho_so_benh_an",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tai_khoan_BenhVienId",
                table: "tai_khoan",
                column: "BenhVienId",
                unique: true,
                filter: "[BenhVienId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tai_khoan_UserId",
                table: "tai_khoan",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email_Phone_Number",
                table: "users",
                columns: new[] { "Email", "Phone_Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_RoleId",
                table: "users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "bac_sis");

            migrationBuilder.DropTable(
                name: "ho_so_benh_an");

            migrationBuilder.DropTable(
                name: "tai_khoan");

            migrationBuilder.DropTable(
                name: "chuyen_khoa");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "benh_vien");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
