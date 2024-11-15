using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTQuanLyHoSoSucKhoe.Migrations
{
    /// <inheritdoc />
    public partial class AddTKBenhVienTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TKBenhVien",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BenhVienId = table.Column<int>(type: "int", nullable: true),
                    Taikhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Matkhau = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TKBenhVien", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TKBenhVien_BenhVien_BenhVienId",
                        column: x => x.BenhVienId,
                        principalTable: "BenhVien",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TKBenhVien_BenhVienId",
                table: "TKBenhVien",
                column: "BenhVienId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TKBenhVien");
        }
    }
}
