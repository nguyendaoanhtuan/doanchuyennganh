using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTQuanLyHoSoSucKhoe.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChuyenKhoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChuyenKhoaId",
                table: "tai_khoan",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "chuyen_khoa",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "chuyen_khoa",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_tai_khoan_ChuyenKhoaId",
                table: "tai_khoan",
                column: "ChuyenKhoaId",
                unique: true,
                filter: "[ChuyenKhoaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_chuyen_khoa_RoleId",
                table: "chuyen_khoa",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_chuyen_khoa_roles_RoleId",
                table: "chuyen_khoa",
                column: "RoleId",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tai_khoan_chuyen_khoa_ChuyenKhoaId",
                table: "tai_khoan",
                column: "ChuyenKhoaId",
                principalTable: "chuyen_khoa",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chuyen_khoa_roles_RoleId",
                table: "chuyen_khoa");

            migrationBuilder.DropForeignKey(
                name: "FK_tai_khoan_chuyen_khoa_ChuyenKhoaId",
                table: "tai_khoan");

            migrationBuilder.DropIndex(
                name: "IX_tai_khoan_ChuyenKhoaId",
                table: "tai_khoan");

            migrationBuilder.DropIndex(
                name: "IX_chuyen_khoa_RoleId",
                table: "chuyen_khoa");

            migrationBuilder.DropColumn(
                name: "ChuyenKhoaId",
                table: "tai_khoan");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "chuyen_khoa");

            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "chuyen_khoa");
        }
    }
}
