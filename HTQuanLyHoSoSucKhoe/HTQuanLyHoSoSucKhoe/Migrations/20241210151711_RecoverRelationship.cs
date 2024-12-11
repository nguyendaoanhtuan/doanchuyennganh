using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTQuanLyHoSoSucKhoe.Migrations
{
    /// <inheritdoc />
    public partial class RecoverRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "LoaiPhieus",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");
            migrationBuilder.AddPrimaryKey(
        name: "PK_LoaiPhieus",
        table: "LoaiPhieus",
        column: "Id");
            migrationBuilder.AddColumn<string>(
                name: "LoaiPhieuId",
                table: "PhieuKetQuas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
