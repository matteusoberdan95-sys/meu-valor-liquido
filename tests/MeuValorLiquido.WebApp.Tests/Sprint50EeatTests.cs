namespace MeuValorLiquido.WebApp.Tests;

public class Sprint50EeatTests : IClassFixture<WebApplicationFactory<Program>>
{
    private static readonly string[] PriorityEditorialSlugs =
    [
        "o-que-e-salario-liquido",
        "como-conferir-holerite",
        "como-avaliar-proposta-salarial",
        "rescisao-clt-vs-trct",
        "como-calcular-ferias",
        "como-calcular-rescisao-clt",
        "como-calcular-inss",
        "entenda-o-irrf",
        "fgts-guia-completo",
        "mei-faturamento-e-das"
    ];

    private readonly HttpClient client;

    public Sprint50EeatTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Como_Calculamos_Should_Show_Category_Methodology_And_Calibration()
    {
        var html = await client.GetStringAsync("/como-calculamos");

        html.Should().Contain("Metodologia por categoria");
        html.Should().Contain("Calculadoras trabalhistas");
        html.Should().Contain("Calculadoras fiscais");
        html.Should().Contain("Calculadoras financeiras");
        html.Should().Contain("Validado com cen");
        html.Should().Contain(CalculatorBenchmarkHelper.FormatCalibrationDate());
        html.Should().Contain("estimativas educativas");
        html.Should().Contain("BreadcrumbList");
    }

    [Theory]
    [InlineData("/calculadoras/salario-liquido")]
    [InlineData("/calculadoras/rescisao-clt")]
    [InlineData("/calculadoras/inss")]
    [InlineData("/calculadoras/fgts")]
    public async Task Priority_Calculator_Should_Show_Validation_Badge(string url)
    {
        var html = await client.GetStringAsync(url);

        html.Should().Contain("valora-benchmark-badge");
        html.Should().Contain("Validado com cen");
        html.Should().Contain("/como-calculamos");
        html.Should().Contain("estimativa educativa");
    }

    [Theory]
    [MemberData(nameof(EditorialArticleData))]
    public async Task Editorial_Article_Should_Link_Calculator_And_Methodology(string slug)
    {
        var html = await client.GetStringAsync($"/blog/{slug}");

        html.Should().Contain("/como-calculamos");
        html.Should().Contain("Como validamos esta estimativa");
        html.Should().Contain("Estimativa educativa");
        html.Should().Contain("/calculadoras/");
        html.Should().Contain("Article");
        html.Should().Contain("headline");
    }

    [Fact]
    public async Task Blog_Post_Should_Link_Methodology_In_Aside()
    {
        var html = await client.GetStringAsync("/blog/o-que-e-salario-liquido");

        html.Should().Contain("Como calculamos");
        html.Should().Contain("/como-calculamos");
    }

    [Fact]
    public async Task Mei_Article_Should_Exist_And_Link_Simulator()
    {
        var html = await client.GetStringAsync("/blog/mei-faturamento-e-das");

        html.Should().Contain("/calculadoras/simulador-mei");
        html.Should().Contain("desenquadramento");
    }

    [Fact]
    public async Task Legal_Pages_Should_Remain_Accessible_From_Methodology()
    {
        var html = await client.GetStringAsync("/como-calculamos");

        html.Should().Contain("/aviso-legal");
        html.Should().Contain("/termos-de-uso");
        html.Should().Contain("/politica-de-privacidade");
    }

    public static IEnumerable<object[]> EditorialArticleData() =>
        PriorityEditorialSlugs.Select(slug => new object[] { slug });
}
