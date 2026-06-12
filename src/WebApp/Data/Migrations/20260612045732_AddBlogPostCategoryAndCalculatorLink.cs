using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuValorLiquido.WebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBlogPostCategoryAndCalculatorLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "blog_posts",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelatedCalculatorSlug",
                table: "blog_posts",
                type: "character varying(80)",
                maxLength: 80,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "blog_posts");

            migrationBuilder.DropColumn(
                name: "RelatedCalculatorSlug",
                table: "blog_posts");
        }
    }
}
