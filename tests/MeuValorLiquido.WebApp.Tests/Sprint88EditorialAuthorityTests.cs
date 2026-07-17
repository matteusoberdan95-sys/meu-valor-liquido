namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint88EditorialAuthorityTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint88EditorialAuthorityTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Author_Profile_Should_Be_Indexable_Verifiable_And_List_Published_Content()
    {
        var html = WebUtility.HtmlDecode(await client.GetStringAsync("/autores/matteus-oberdan"));

        html.Should().Contain("<h1 class=\"valora-h1\">Matteus Oberdan</h1>");
        html.Should().Contain("Atuação no projeto");
        html.Should().Contain("Como o conteúdo é revisado");
        html.Should().Contain("O que é salário líquido?");
        html.Should().Contain("17/07/2026");
        html.Should().Contain("https://www.linkedin.com/in/matteus-oberdan-203205289/");
        html.Should().Contain("/images/authors/matteus-oberdan.svg");
        html.Should().Contain("\"@type\":\"Person\"");
        html.Should().Contain("\"url\":\"https://meuvalorliquido.com/autores/matteus-oberdan\"");
        html.Should().Contain("content=\"index,follow\"");
        html.Should().NotContain("certificação profissional verificada");
    }

    [Fact]
    public async Task Blog_Post_Should_Link_Author_Profile_In_Visible_Byline_And_Schema()
    {
        var html = await client.GetStringAsync("/blog/o-que-e-salario-liquido");

        html.Should().Contain("href=\"/autores/matteus-oberdan\" rel=\"author\">Matteus Oberdan</a>");
        html.Should().Contain("Ver perfil editorial");
        html.Should().Contain("\"url\":\"https://meuvalorliquido.com/autores/matteus-oberdan\"");
        html.Should().Contain("\"sameAs\":[\"https://www.linkedin.com/in/matteus-oberdan-203205289/\"]");
    }

    [Fact]
    public async Task Editorial_Policy_Should_Disclose_Sources_Corrections_Sponsorship_Automation_And_Review()
    {
        var html = await client.GetStringAsync("/politica-editorial");

        html.Should().Contain("Não inventamos números, fontes, credenciais, avaliações");
        html.Should().Contain("Conteúdo patrocinado e publicidade");
        html.Should().Contain("Uso responsável de automação");
        html.Should().Contain("Frequência de atualização");
        html.Should().Contain("href=\"/correcoes\"");
        html.Should().Contain("href=\"/autores/matteus-oberdan\"");
    }

    [Fact]
    public async Task Corrections_Page_Should_Explain_Process_And_Remain_Out_Of_Xml_Sitemap()
    {
        var corrections = await client.GetStringAsync("/correcoes");
        var sitemap = await client.GetStringAsync("/sitemap.xml");

        corrections.Should().Contain("Como reportar uma divergência");
        corrections.Should().Contain("Como analisamos");
        corrections.Should().Contain("não há correções editoriais relevantes publicadas");
        corrections.Should().Contain("content=\"noindex,follow\"");
        sitemap.Should().NotContain("/correcoes");
    }

    [Fact]
    public async Task Author_Profile_Should_Be_Discoverable_And_Avatar_Should_Exist()
    {
        var sitemap = await client.GetStringAsync("/sitemap.xml");
        var siteMap = await client.GetStringAsync("/mapa-do-site");
        var footer = await client.GetStringAsync("/");
        var avatar = await client.GetAsync("/images/authors/matteus-oberdan.svg");

        sitemap.Should().Contain("/autores/matteus-oberdan");
        siteMap.Should().Contain("Matteus Oberdan — responsável editorial");
        footer.Should().Contain("Responsável editorial");
        avatar.EnsureSuccessStatusCode();
        avatar.Content.Headers.ContentType?.MediaType.Should().Be("image/svg+xml");
    }
}
