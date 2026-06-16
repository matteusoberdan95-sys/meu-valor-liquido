namespace MeuValorLiquido.WebApp.Tests;

public class PopularQuestionsPageTests
{
    [Fact]
    public void Catalog_Should_Have_Unique_Slugs_And_Valid_Related_Links()
    {
        var all = PopularQuestionsCatalog.GetAll();

        all.Should().HaveCountGreaterThanOrEqualTo(15);
        all.Select(q => q.Slug).Should().OnlyHaveUniqueItems();

        foreach (var question in all)
        {
            question.Title.Should().NotBeNullOrWhiteSpace();
            question.AnswerHtml.Should().Contain("<p>");

            if (!string.IsNullOrEmpty(question.RelatedCalculatorSlug))
            {
                question.RelatedCalculatorSlug.Should().NotContain("/");
            }

            foreach (var relatedSlug in question.RelatedQuestionSlugs)
            {
                PopularQuestionsCatalog.GetBySlug(relatedSlug).Should().NotBeNull(
                    because: $"related slug '{relatedSlug}' on '{question.Slug}' must exist");
            }
        }
    }

    [Fact]
    public async Task Hub_Page_Should_Load()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(b => b.UseEnvironment("Testing")).CreateClient();

        var response = await client.GetAsync("/duvidas");

        response.IsSuccessStatusCode.Should().BeTrue();
        var html = await response.Content.ReadAsStringAsync();
        html.Should().Contain("Dúvidas populares");
        html.Should().Contain("/duvidas/como-calcular-salario-liquido");
        html.Should().Contain("BreadcrumbList");
    }

    [Fact]
    public async Task Detail_Page_Should_Show_Faq_And_Calculator_Cta()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(b => b.UseEnvironment("Testing")).CreateClient();

        var html = await client.GetStringAsync("/duvidas/como-calcular-salario-liquido");

        html.Should().Contain("FAQPage");
        html.Should().Contain("como-calcular-salario-liquido");
        html.Should().Contain("Simule agora");
        html.Should().Contain("/calculadoras/salario-liquido");
        html.Should().Contain("Leia também");
    }

    [Fact]
    public async Task Detail_Page_Should_Return_NotFound_For_Invalid_Slug()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(b => b.UseEnvironment("Testing")).CreateClient();

        var response = await client.GetAsync("/duvidas/slug-inexistente-xyz");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Sitemap_Should_Include_Duvidas_Urls()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(b => b.UseEnvironment("Testing")).CreateClient();

        var xml = await client.GetStringAsync("/sitemap.xml");

        xml.Should().Contain("/duvidas");
        xml.Should().Contain("/duvidas/como-calcular-salario-liquido");
    }
}
