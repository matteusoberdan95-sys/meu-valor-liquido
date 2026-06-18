namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint68BlogTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint68BlogTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    public static IEnumerable<object[]> Sprint68EditorialSlugs =>
        BlogEditorialCatalog.Sprint68EditorialSlugs.Select(slug => new object[] { slug });

    [Theory]
    [MemberData(nameof(Sprint68EditorialSlugs))]
    public async Task Sprint68_Article_Should_Link_Calculator_Methodology_And_Faq(string slug)
    {
        var html = await client.GetStringAsync($"/blog/{slug}");

        html.Should().Contain("/como-calculamos");
        html.Should().Contain("id=\"como-validamos\"");
        html.Should().Contain("/calculadoras/");
        html.Should().Contain("/duvidas/");
        html.Should().Contain("Article");
    }

    [Fact]
    public async Task Sprint68_Should_Add_Seven_Editorial_Articles_Completing_Calendar()
    {
        BlogEditorialCatalog.Sprint68EditorialSlugs.Should().HaveCount(7);

        foreach (var slug in BlogEditorialCatalog.Sprint68EditorialSlugs)
        {
            BlogArticleSeedData.GetAll().Any(a => a.Slug == slug).Should().BeTrue();
            var response = await client.GetAsync($"/blog/{slug}");
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }

    [Fact]
    public async Task Sitemap_Should_Include_Sprint68_Articles()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");

        foreach (var slug in BlogEditorialCatalog.Sprint68EditorialSlugs)
        {
            xml.Should().Contain($"/blog/{slug}");
        }
    }

    [Fact]
    public async Task Negociar_Hub_Should_List_Bruto_Necessario_Article()
    {
        var html = await client.GetStringAsync("/negociar-salario");

        html.Should().Contain("/blog/quanto-preciso-ganhar-para-receber-x");
    }

    [Fact]
    public async Task Virar_Pj_Hub_Should_List_Mei_And_Prolabore_Articles()
    {
        var html = await client.GetStringAsync("/virar-pj");

        html.Should().Contain("/blog/mei-desenquadramento-o-que-fazer");
        html.Should().Contain("/blog/pro-labore-pj-quanto-retirar");
    }

    [Theory]
    [InlineData("quanto-preciso-ganhar-para-receber-x", "/calculadoras/salario-bruto-necessario")]
    [InlineData("mei-desenquadramento-o-que-fazer", "/calculadoras/simulador-mei")]
    [InlineData("pro-labore-pj-quanto-retirar", "/calculadoras/pj-vs-clt")]
    [InlineData("decimo-terceiro-primeira-segunda-parcela", "/calculadoras/decimo-terceiro")]
    [InlineData("ferias-abono-pecuniario-vale-a-pena", "/calculadoras/ferias")]
    public async Task Sprint68_Article_Should_Link_Primary_Calculator(string slug, string calculatorPath)
    {
        var html = await client.GetStringAsync($"/blog/{slug}");

        html.Should().Contain(calculatorPath);
    }
}
