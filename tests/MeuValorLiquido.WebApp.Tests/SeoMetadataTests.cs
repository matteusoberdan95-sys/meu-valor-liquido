namespace MeuValorLiquido.WebApp.Tests;
public class SeoMetadataTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public SeoMetadataTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Calculator_Page_Should_Include_Faq_JsonLd()
    {
        var html = await client.GetStringAsync("/calculadoras/salario-liquido");

        html.Should().Contain("FAQPage");
    }

    [Fact]
    public async Task Blog_Post_Should_Include_Article_JsonLd()
    {
        var html = await client.GetStringAsync("/blog/o-que-e-salario-liquido");

        html.Should().Contain("Article");
        html.Should().Contain("headline");
    }

    [Fact]
    public async Task Home_Should_Include_OpenGraph_Tags()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("property=\"og:title\"");
        html.Should().Contain("property=\"og:description\"");
        html.Should().Contain("property=\"og:image\"");
        html.Should().Contain("name=\"twitter:image\"");
        html.Should().Contain("rel=\"canonical\"");
        html.Should().Contain("name=\"robots\"");
    }

    [Fact]
    public async Task Calculator_Page_Should_Include_Breadcrumb_JsonLd()
    {
        var html = await client.GetStringAsync("/calculadoras/salario-liquido");

        html.Should().Contain("BreadcrumbList");
    }

    [Fact]
    public async Task Error_Page_Should_Use_NoIndex()
    {
        var html = await client.GetStringAsync("/Error");

        html.Should().Contain("name=\"robots\"");
        html.Should().Contain("noindex,nofollow");
    }

    [Fact]
    public async Task Sitemap_Should_Include_Newsletter_And_Site_Map_Page()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");

        xml.Should().Contain("/newsletter");
        xml.Should().Contain("/mapa-do-site");
        xml.Should().Contain("/salario-liquido");
        xml.Should().Contain("/salario-liquido/3000");
        xml.Should().Contain("/duvidas");
        xml.Should().Contain("/duvidas/como-calcular-salario-liquido");
    }

    [Fact]
    public async Task Sitemap_Should_Include_Lastmod_For_Indexation_Signals()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");
        var document = XDocument.Parse(xml);
        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

        xml.Should().Contain("<lastmod>2026-06-29</lastmod>");
        var salaryCalculatorUrls = document
            .Descendants(ns + "url")
            .Where(url => url.Element(ns + "loc")?.Value == "https://meuvalorliquido.com/calculadoras/salario-liquido")
            .ToArray();

        salaryCalculatorUrls.Should().NotBeEmpty();
        salaryCalculatorUrls.Should().OnlyContain(url =>
            url.Element(ns + "lastmod") != null && url.Element(ns + "lastmod")!.Value == "2026-06-29");
    }

    [Fact]
    public async Task Mapa_Do_Site_Should_List_Calculators()
    {
        var html = await client.GetStringAsync("/mapa-do-site");

        html.Should().Contain("/calculadoras/salario-liquido");
        html.Should().Contain("salario-liquido");
    }
}
