namespace MeuValorLiquido.WebApp.Tests;
public class PublicPagesTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;
    private readonly HttpClient client;

    public PublicPagesTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory;
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Theory]
    [InlineData("/")]
    [InlineData("/calculadoras")]
    [InlineData("/calculadoras/salario-liquido")]
    [InlineData("/calculadoras/salario-bruto-necessario")]
    [InlineData("/salario-liquido")]
    [InlineData("/salario-liquido/3000")]
    [InlineData("/sobre")]
    [InlineData("/contato")]
    [InlineData("/politica-de-privacidade")]
    [InlineData("/termos-de-uso")]
    [InlineData("/aviso-legal")]
    [InlineData("/blog")]
    [InlineData("/newsletter")]
    [InlineData("/health")]
    [InlineData("/sitemap.xml")]
    public async Task Public_Page_Should_Load(string url)
    {
        var response = await client.GetAsync(url);

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task Salary_Bruto_Alias_Should_Redirect_To_Calculator()
    {
        using var noRedirectClient = factory
            .WithWebHostBuilder(builder => builder.UseEnvironment("Testing"))
            .CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

        var response = await noRedirectClient.GetAsync("/calculadora-salario-bruto");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.MovedPermanently);
        response.Headers.Location!.ToString().Should().Contain("salario-bruto-necessario");
    }

    [Fact]
    public async Task Invalid_Salary_Band_Should_Return_NotFound()
    {
        var response = await client.GetAsync("/salario-liquido/3333");
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}
