using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreAngularCRUDApp.Migrations
{
    public partial class PostWithCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "BlogPost",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPost_CategoryId",
                table: "BlogPost",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPost_BlogCategory_CategoryId",
                table: "BlogPost",
                column: "CategoryId",
                principalTable: "BlogCategory",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPost_BlogCategory_CategoryId",
                table: "BlogPost");

            migrationBuilder.DropIndex(
                name: "IX_BlogPost_CategoryId",
                table: "BlogPost");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "BlogPost");
        }
    }
}
