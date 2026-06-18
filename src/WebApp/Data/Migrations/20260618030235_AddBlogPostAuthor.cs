using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuValorLiquido.WebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBlogPostAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "blog_posts",
                type: "character varying(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "Matteus Oberdan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "blog_posts");
        }
    }
}
