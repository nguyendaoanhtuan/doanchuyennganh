using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTQuanLyHoSoSucKhoe.Migrations
{
    /// <inheritdoc />
    public partial class FixLoaiPhieuIđDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuKetQuas_LoaiPhieus_LoaiPhieuId",
                table: "PhieuKetQuas");

            migrationBuilder.DropIndex(
                name: "IX_PhieuKetQuas_LoaiPhieuId",
                table: "PhieuKetQuas");

            migrationBuilder.DropColumn(
                name: "LoaiPhieuId",
                table: "PhieuKetQuas");
            migrationBuilder.DropPrimaryKey(
        name: "PK_LoaiPhieus",
        table: "LoaiPhieus");
            migrationBuilder.DropColumn(
                name: "Id",
                table: "LoaiPhieus");
              
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "LoaiPhieuId",
            //    table: "PhieuKetQuas",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "LoaiPhieus",
                type: "nvarchar(450)",
                nullable: false);
            migrationBuilder.AddPrimaryKey(
        name: "PK_LoaiPhieus",
        table: "LoaiPhieus",
        column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuKetQuas_LoaiPhieuId",
                table: "PhieuKetQuas",
                column: "LoaiPhieuId");

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
