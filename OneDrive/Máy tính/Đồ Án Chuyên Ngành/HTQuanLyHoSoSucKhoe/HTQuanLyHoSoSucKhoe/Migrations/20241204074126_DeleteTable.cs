using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTQuanLyHoSoSucKhoe.Migrations
{
    /// <inheritdoc />
    public partial class DeleteTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThongTinBenhNhan");

            migrationBuilder.DropColumn(
                name: "Ho",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Ten",
                table: "Users",
                newName: "hoVaTen");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "hoVaTen",
                table: "Users",
                newName: "Ten");

            migrationBuilder.AddColumn<string>(
                name: "Ho",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ThongTinBenhNhan",
                columns: table => new
                {
                    cccd = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sdt = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinBenhNhan", x => x.cccd);
                    table.ForeignKey(
                        name: "FK_ThongTinBenhNhan_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThongTinBenhNhan_userID",
                table: "ThongTinBenhNhan",
                column: "userID",
                unique: true);
        }
    }
}
