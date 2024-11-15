using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTQuanLyHoSoSucKhoe.Migrations
{
    /// <inheritdoc />
    public partial class AddChuyenKhoaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChuyenKhoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BenhVienId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BenhVienId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuyenKhoas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChuyenKhoas_BenhVien_BenhVienId",
                        column: x => x.BenhVienId,
                        principalTable: "BenhVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChuyenKhoas_BenhVien_BenhVienId1",
                        column: x => x.BenhVienId1,
                        principalTable: "BenhVien",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChuyenKhoas_BenhVienId",
                table: "ChuyenKhoas",
                column: "BenhVienId");

            migrationBuilder.CreateIndex(
                name: "IX_ChuyenKhoas_BenhVienId1",
                table: "ChuyenKhoas",
                column: "BenhVienId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChuyenKhoas");
        }
    }
}
