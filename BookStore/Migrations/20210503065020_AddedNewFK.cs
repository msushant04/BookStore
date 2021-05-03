using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class AddedNewFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGallary_Books_BooksId",
                table: "BookGallary");

            migrationBuilder.DropIndex(
                name: "IX_BookGallary_BooksId",
                table: "BookGallary");

            migrationBuilder.DropColumn(
                name: "BooksId",
                table: "BookGallary");

            migrationBuilder.CreateIndex(
                name: "IX_BookGallary_bookId",
                table: "BookGallary",
                column: "bookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookGallary_Books_bookId",
                table: "BookGallary",
                column: "bookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGallary_Books_bookId",
                table: "BookGallary");

            migrationBuilder.DropIndex(
                name: "IX_BookGallary_bookId",
                table: "BookGallary");

            migrationBuilder.AddColumn<int>(
                name: "BooksId",
                table: "BookGallary",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookGallary_BooksId",
                table: "BookGallary",
                column: "BooksId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookGallary_Books_BooksId",
                table: "BookGallary",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
