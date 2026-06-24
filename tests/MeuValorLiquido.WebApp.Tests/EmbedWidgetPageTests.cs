namespace MeuValorLiquido.WebApp.Tests;

public class EmbedWidgetPageTests
{
    [Fact]
    public void Catalog_Should_List_Embeddable_Slugs()
    {
        var widgets = EmbedWidgetCatalog.GetAll();

        widgets.Should().HaveCountGreaterThanOrEqualTo(6);
        widgets.Select(w => w.Slug).Should().OnlyHaveUniqueItems();
        EmbedWidgetCatalog.IsEmbeddable("salario-liquido").Should().BeTrue();
        EmbedWidgetCatalog.IsEmbeddable("rescisao-clt").Should().BeFalse();
    }

    [Fact]
    public void EmbedCode_Should_Include_Iframe_And_Attribution()
    {
        var widget = EmbedWidgetCatalog.GetBySlug("salario-liquido")!;
        var html = EmbedCodeBuilder.BuildIframeHtml("https://meuvalorliquido.com.br", widget);

        html.Should().Contain("/widget/salario-liquido");
        html.Should().Contain("iframe");
        html.Should().Contain("Calculadora por Meu Valor Líquido");
    }

    [Fact]
    public async Task Widget_Hub_Should_Load()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(b => b.UseEnvironment("Testing")).CreateClient();

        var html = await client.GetStringAsync("/widget");

        html.Should().Contain("Widget incorpor");
        html.Should().Contain("data-copy-widget");
        html.Should().Contain("valora-stitch-widget-hub");
        html.Should().Contain("mvl-widget-catalog");
        html.Should().Contain("salario-liquido");
        html.Should().Contain("BreadcrumbList");
    }

    [Fact]
    public async Task Widget_Slug_Should_Redirect_To_Embed_Calculator()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(b => b.UseEnvironment("Testing")).CreateClient(
            new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

        var response = await client.GetAsync("/widget/salario-liquido");

        response.StatusCode.Should().Be(HttpStatusCode.Redirect);
        response.Headers.Location?.ToString().Should().Contain("/calculadoras/salario-liquido?embed=1");
    }

    [Fact]
    public async Task Embed_Calculator_Should_Be_Frameable_And_Without_Ads()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(b => b.UseEnvironment("Testing")).CreateClient();

        using var request = new HttpRequestMessage(HttpMethod.Get, "/calculadoras/salario-liquido?embed=1");
        var response = await client.SendAsync(request);
        var html = await response.Content.ReadAsStringAsync();

        response.IsSuccessStatusCode.Should().BeTrue();
        response.Headers.Contains("X-Frame-Options").Should().BeFalse();
        response.Headers.GetValues("Content-Security-Policy").First().Should().Contain("frame-ancestors *");
        html.Should().Contain("valora-embed-body");
        html.Should().Contain("noindex,nofollow");
        html.Should().NotContain("ad-slot");
        html.Should().NotContain("data-local-panel-save");
    }

    [Fact]
    public async Task Embed_On_Non_Whitelisted_Slug_Should_Return_NotFound()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(b => b.UseEnvironment("Testing")).CreateClient();

        var response = await client.GetAsync("/calculadoras/rescisao-clt?embed=1");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Sitemap_Should_Include_Widget_Hub()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(b => b.UseEnvironment("Testing")).CreateClient();

        var xml = await client.GetStringAsync("/sitemap.xml");

        xml.Should().Contain("/widget");
    }
}
