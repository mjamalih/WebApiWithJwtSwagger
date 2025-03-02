using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiWithJwtSwagger.Migrations
{
    /// <inheritdoc />
    public partial class mig_add_subModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubModelDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<long>(type: "bigint", nullable: false),
                    RequestPaymentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubModelDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubModelDetail_PaymentRequest_RequestPaymentId",
                        column: x => x.RequestPaymentId,
                        principalTable: "PaymentRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubModelDetail_RequestPaymentId",
                table: "SubModelDetail",
                column: "RequestPaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubModelDetail");
        }
    }
}
