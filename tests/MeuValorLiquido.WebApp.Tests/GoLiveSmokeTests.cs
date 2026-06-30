namespace MeuValorLiquido.WebApp.Tests;

/// <summary>Rotas e assets críticos para go-live (Sprint 19).</summary>
public class GoLiveSmokeTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public GoLiveSmokeTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Theory]
    [InlineData("/duvidas")]
    [InlineData("/duvidas/quanto-desconta-inss-2026")]
    [InlineData("/meu-painel")]
    [InlineData("/widget")]
    [InlineData("/mapa-do-site")]
    [InlineData("/desligamento")]
    [InlineData("/negociar-salario")]
    [InlineData("/virar-pj")]
    [InlineData("/conferir-holerite")]
    [InlineData("/robots.txt")]
    [InlineData("/favicon.ico")]
    public async Task Go_Live_Route_Or_Asset_Should_Be_Available(string path)
    {
        var response = await client.GetAsync(path);

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task Robots_Should_Reference_Sitemap()
    {
        var body = await client.GetStringAsync("/robots.txt");

        body.Should().Contain("Sitemap:");
        body.Should().Contain("sitemap.xml");
    }

    [Fact]
    public async Task Sitemap_Should_Accept_Head_For_Search_Console()
    {
        using var request = new HttpRequestMessage(HttpMethod.Head, "/sitemap.xml");
        using var response = await client.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Headers.ContentType?.MediaType.Should().Be("application/xml");
    }

    [Fact]
    public async Task Sitemap_Should_Include_Core_Pages_And_Calculators()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");

        xml.Should().Contain("<loc>https://meuvalorliquido.com/</loc>");
        xml.Should().Contain("/calculadoras/salario-liquido");
        xml.Should().Contain("/como-calculamos");
        xml.Should().Contain("/politica-de-privacidade");
        xml.Should().Contain("/politica-de-cookies");
    }

    [Fact]
    public async Task Health_Should_Report_Healthy()
    {
        var response = await client.GetAsync("/health");

        response.IsSuccessStatusCode.Should().BeTrue();
        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Healthy");
    }

    [Fact]
    public async Task Calculadoras_Hub_Should_Match_Stitch_Bento_Layout()
    {
        var html = await client.GetStringAsync("/calculadoras");

        html.Should().Contain("valora-stitch-calc-hub");
        html.Should().Contain("valora-stitch-calc-featured");
        html.Should().Contain("Mais utilizada");
        html.Should().Contain("valora-bottom-nav");
    }

    [Fact]
    public async Task Home_Should_Match_Stitch_Mobile_Shell()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("valora-bottom-nav");
        html.Should().Contain("valora-stitch-hero");
        html.Should().Contain("valora-stitch-bento");
        html.Should().Contain("valora-nav-search");
        html.Should().Contain("Nossa Metodologia");
        html.Should().Contain("images/social-proof/avatar-1");
        html.Should().Contain("valora-stitch-social-proof-rating");
        html.Should().Contain("valora-stitch-star-icon");
    }

    [Fact]
    public async Task Calculator_Detail_Should_Match_Stitch_Layout()
    {
        var html = await client.GetStringAsync("/calculadoras/salario-liquido");

        html.Should().Contain("valora-stitch-calc-detail");
        html.Should().Contain("valora-stitch-salario");
        html.Should().Contain("valora-stitch-calc-detail-hero");
        html.Should().Contain("valora-stitch-calc-detail-badge");
        (html.Contains("Simulação CLT") || html.Contains("Simula&#xE7;&#xE3;o CLT")).Should().BeTrue();
        html.Should().Contain("Calcular Valor");
        html.Should().Contain("valora-bottom-nav");
    }

    [Fact]
    public async Task PjVsClt_Should_Match_Stitch_Comparator_Layout()
    {
        var html = await client.GetStringAsync("/calculadoras/pj-vs-clt");

        html.Should().Contain("valora-stitch-cltpj");
        html.Should().Contain("Entradas CLT");
        html.Should().Contain("Entradas PJ");
        html.Should().Contain("CALCULAR AGORA");
        html.Should().Contain("data-pj-wizard");
        html.Should().Contain("valora-pj-wizard-stepper");
        html.Should().Contain("valora-bottom-nav");
    }

    [Fact]
    public async Task Faq_Hub_Should_Match_Stitch_Layout()
    {
        var html = await client.GetStringAsync("/duvidas");

        html.Should().Contain("valora-stitch-faq-hub");
        html.Should().Contain("valora-stitch-faq-layout");
        html.Should().Contain("valora-stitch-faq-sidebar");
        html.Should().Contain("Como podemos ajudar hoje?");
        html.Should().Contain("Perguntas Populares");
        html.Should().Contain("Regime CLT");
        html.Should().Contain("Falar com suporte");
        html.Should().Contain("valora-bottom-nav");
    }

    [Fact]
    public async Task Blog_Hub_Should_Match_Stitch_Layout()
    {
        var html = await client.GetStringAsync("/blog");

        html.Should().Contain("valora-stitch-blog-hub");
        html.Should().Contain("Conteúdo Educativo");
        html.Should().Contain("valora-stitch-blog-chips");
        html.Should().Contain("valora-stitch-blog-featured");
        html.Should().Contain("CLT &amp; Direitos");
    }

    [Fact]
    public async Task Metodologia_Should_Match_Stitch_Layout()
    {
        var html = await client.GetStringAsync("/como-calculamos");

        html.Should().Contain("valora-stitch-metodologia");
        html.Should().Contain("Transparência e Metodologia");
        html.Should().Contain("Tabelas de 2026");
        html.Should().Contain("INSS progressivo");
        html.Should().Contain("IRRF mensal");
    }

    [Fact]
    public async Task Local_Panel_Should_Match_Stitch_Layout()
    {
        var html = await client.GetStringAsync("/meu-painel");

        html.Should().Contain("valora-stitch-panel--bento");
        html.Should().Contain("valora-stitch-panel-bento");
        html.Should().Contain("Olá!");
        html.Should().Contain("data-local-panel-page");
        html.Should().Contain("valora-bottom-nav");
    }

    [Fact]
    public async Task Contato_Should_Match_Stitch_Layout()
    {
        var html = await client.GetStringAsync("/contato");

        html.Should().Contain("valora-stitch-contact");
        html.Should().Contain("valora-stitch-contact-form");
        html.Should().Contain("Enviar mensagem");
        html.Should().Contain("Dúvidas frequentes");
    }

    [Fact]
    public async Task Newsletter_Should_Match_Stitch_Layout()
    {
        var html = await client.GetStringAsync("/newsletter");

        html.Should().Contain("valora-stitch-newsletter");
        html.Should().Contain("valora-stitch-newsletter-form");
        html.Should().Contain("Inscrever-se");
    }

    [Fact]
    public async Task Institutional_Pages_Should_Match_Stitch_Layout()
    {
        var sobre = await client.GetStringAsync("/sobre");
        var privacidade = await client.GetStringAsync("/politica-de-privacidade");
        var termos = await client.GetStringAsync("/termos-de-uso");

        sobre.Should().Contain("valora-stitch-about");
        sobre.Should().Contain("Nossa missão");
        privacidade.Should().Contain("valora-stitch-legal");
        privacidade.Should().Contain("valora-stitch-legal-sidebar");
        privacidade.Should().Contain("Google AdSense");
        termos.Should().Contain("valora-stitch-legal-toc");
        termos.Should().Contain("Natureza do serviço");
    }

    [Fact]
    public async Task Stitch_Desktop_Structure_Should_Be_Present_On_Key_Pages()
    {
        var blog = await client.GetStringAsync("/blog");
        var article = await client.GetStringAsync("/blog/o-que-e-salario-liquido");
        var metodologia = await client.GetStringAsync("/como-calculamos");
        var faq = await client.GetStringAsync("/duvidas");

        blog.Should().Contain("valora-stitch-blog-hub");
        article.Should().Contain("valora-stitch-blog-article-layout");
        article.Should().Contain("valora-stitch-blog-article-aside");
        metodologia.Should().Contain("valora-stitch-metodologia-tables");
        faq.Should().Contain("valora-stitch-faq-hub");
    }

    [Fact]
    public async Task Error_Page_Should_Match_Stitch_Layout()
    {
        var html = await client.GetStringAsync("/Error");

        html.Should().Contain("valora-stitch-error");
        html.Should().Contain("Erro 500");
        html.Should().Contain("valora-stitch-error-grid");
        html.Should().Contain("noindex,nofollow");
    }

    [Fact]
    public async Task NotFound_Page_Should_Match_Stitch_Layout()
    {
        var response = await client.GetAsync("/salario-liquido/3333");
        var html = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        html.Should().Contain("valora-stitch-error");
        html.Should().Contain("Erro 404");
        html.Should().Contain("saiu da rota");
        html.Should().Contain("Central de ajuda");
    }

    [Fact]
    public async Task Header_Should_Include_Desktop_Search_Pill()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("valora-nav-search");
        html.Should().Contain("valora-nav-search-input");
        html.Should().Contain("Buscar...");
    }

    [Fact]
    public async Task Home_Should_Load_Stylesheets()
    {
        var html = await client.GetStringAsync("/");

        var siteCss = System.Text.RegularExpressions.Regex.Match(
            html,
            @"href=""(?<path>/css/site\.[^""]+\.css)""");
        siteCss.Success.Should().BeTrue();
        using var siteResponse = await client.GetAsync(siteCss.Groups["path"].Value);
        siteResponse.IsSuccessStatusCode.Should().BeTrue();
        siteResponse.Content.Headers.ContentType?.MediaType.Should().Be("text/css");

        var scopedCss = System.Text.RegularExpressions.Regex.Match(
            html,
            @"href=""(?<path>/MeuValorLiquido\.WebApp\.[^""]+\.styles\.css)""");
        scopedCss.Success.Should().BeTrue();
        using var scopedResponse = await client.GetAsync(scopedCss.Groups["path"].Value);
        scopedResponse.IsSuccessStatusCode.Should().BeTrue();
        scopedResponse.Content.Headers.ContentType?.MediaType.Should().Be("text/css");
    }

    [Fact]
    public async Task Home_Should_Send_Security_Headers()
    {
        var response = await client.GetAsync("/");

        response.Headers.TryGetValues("X-Content-Type-Options", out var nosniff).Should().BeTrue();
        nosniff!.First().Should().Be("nosniff");
        response.Headers.TryGetValues("Content-Security-Policy", out _).Should().BeTrue();
    }

  public static IEnumerable<object[]> AllCalculatorSlugs =>
      CalculatorSeedData.GetDefinitions().Select(definition => new object[] { definition.Slug });

  [Theory]
  [MemberData(nameof(AllCalculatorSlugs))]
  public async Task PostDeploy_All_Calculators_Should_Load(string slug)
  {
      var response = await client.GetAsync($"/calculadoras/{slug}");

      response.IsSuccessStatusCode.Should().BeTrue($"calculadora {slug} should return 200");
      var html = await response.Content.ReadAsStringAsync();
      html.Should().Contain("Input_Amount", $"calculadora {slug} should expose amount field or equivalent form");
  }
}
