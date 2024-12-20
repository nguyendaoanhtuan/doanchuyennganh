using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTQuanLyHoSoSucKhoe.Migrations
{
    /// <inheritdoc />
    public partial class updatever20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Updated_At",
                table: "appointments",
                newName: "NgayTao");

            migrationBuilder.RenameColumn(
                name: "Created_At",
                table: "appointments",
                newName: "NgayCapNhat");

            migrationBuilder.AddColumn<int>(
                name: "soThuTu",
                table: "appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "soThuTu",
                table: "appointments");

            migrationBuilder.RenameColumn(
                name: "NgayTao",
                table: "appointments",
                newName: "Updated_At");

            migrationBuilder.RenameColumn(
                name: "NgayCapNhat",
                table: "appointments",
                newName: "Created_At");
        }
    }
}
