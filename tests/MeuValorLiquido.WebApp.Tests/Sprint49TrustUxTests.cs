namespace MeuValorLiquido.WebApp.Tests;

public class Sprint49TrustUxTests : IClassFixture<WebApplicationFactory<Program>>
{
    private static readonly string[] CalculatorSlugs =
    [
        "salario-liquido",
        "salario-bruto-necessario",
        "proposta-salarial",
        "ferias",
        "decimo-terceiro",
        "rescisao-clt",
        "hora-extra",
        "inss",
        "irrf",
        "pj-vs-clt",
        "juros-compostos",
        "financiamento",
        "fgts",
        "simulador-mei",
        "custo-funcionario",
        "multa-atraso",
        "conversor-salario"
    ];

    private readonly HttpClient client;

    public Sprint49TrustUxTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Theory]
    [InlineData("/calculadoras/salario-liquido")]
    [InlineData("/calculadoras/rescisao-clt")]
    [InlineData("/calculadoras/ferias")]
    [InlineData("/calculadoras/financiamento")]
    [InlineData("/calculadoras/fgts")]
    [InlineData("/calculadoras/inss")]
    public async Task Calculator_Form_Should_Render_Field_Tooltips(string url)
    {
        var html = await GetPageHtml(url);

        html.Should().Contain("valora-field-tip");
        html.Should().Contain("valora-field-tip-btn");
    }

    [Theory]
    [MemberData(nameof(CalculatorSlugsData))]
    public async Task Calculator_Form_Should_Render_Submit_And_Tooltip_Or_Helper(string slug)
    {
        var html = await GetPageHtml($"/calculadoras/{slug}");

        html.Should().Contain("CALCULAR AGORA");
        (html.Contains("valora-field-tip") || html.Contains("valora-helper"))
            .Should()
            .BeTrue($"calculadora {slug} deve ter tooltip ou helper educativo");
    }

    [Fact]
    public async Task Rescisao_Result_Should_Show_Trust_Warnings()
    {
        var input = CalculatorInputDefaults.ForSlug("rescisao-clt") with
        {
            TerminationReason = TerminationReason.DismissalWithoutCause
        };
        var html = await GetSharedResultHtml("rescisao-clt", input);

        html.Should().Contain("valora-result-warnings");
        html.Should().Contain("O TRCT e o holerite oficial");
        html.Should().Contain("seguro-desemprego");
    }

    [Fact]
    public async Task Ferias_Result_Should_Show_Trust_Warnings()
    {
        var html = await GetSharedResultHtml("ferias", CalculatorInputDefaults.ForSlug("ferias"));

        html.Should().Contain("valora-result-warnings");
        html.Should().Contain("holerite de f");
    }

    [Fact]
    public async Task Salario_Liquido_Result_Should_Show_Holerite_Warning()
    {
        var html = await GetSharedResultHtml("salario-liquido", new CalculatorInput(4000m));

        html.Should().Contain("valora-result-warnings");
        html.Should().Contain("Holerite real pode divergir");
    }

    [Fact]
    public async Task Mei_Above_Limit_Result_Should_Show_Disenrollment_Warning()
    {
        var input = new CalculatorInput(10_000m, MeiActivity: MeiActivityType.Services);
        var html = await GetSharedResultHtml("simulador-mei", input);

        html.Should().Contain("valora-result-warning--warning");
        html.Should().Contain("desenquadramento");
    }

    [Fact]
    public async Task PjVsClt_Result_Should_Show_Comparison_Warnings()
    {
        var input = CalculatorInputDefaults.ForSlug("pj-vs-clt");
        var token = CalculatorInputShareCodec.Encode(input);
        var html = await GetPageHtml($"/calculadoras/pj-vs-clt?r={Uri.EscapeDataString(token)}");

        html.Should().Contain("valora-result-warnings");
        html.Should().Contain("13");
    }

    [Fact]
    public void Warning_Builder_Should_Not_Add_Ads_Or_Promotional_Copy()
    {
        var warnings = CalculatorResultWarningBuilder.Build(
            "salario-liquido",
            new CalculatorInput(3000m),
            new CalculationResult(
                "salario-liquido",
                "Salário líquido",
                Money.From(3000m),
                [],
                Money.From(2500m),
                "ok",
                "disclaimer"));

        warnings.Should().NotBeEmpty();
        warnings.Should().OnlyContain(w => !w.Message.Contains("compre", StringComparison.OrdinalIgnoreCase));
        warnings.Should().OnlyContain(w => !w.Message.Contains("promo", StringComparison.OrdinalIgnoreCase));
    }

    public static IEnumerable<object[]> CalculatorSlugsData() =>
        CalculatorSlugs.Select(slug => new object[] { slug });

    private async Task<string> GetSharedResultHtml(string slug, CalculatorInput input)
    {
        var token = CalculatorInputShareCodec.Encode(input);
        return await GetPageHtml($"/calculadoras/{slug}?r={Uri.EscapeDataString(token)}");
    }

    private async Task<string> GetPageHtml(string url)
    {
        var response = await client.GetAsync(url);
        var html = await response.Content.ReadAsStringAsync();
        response.IsSuccessStatusCode.Should().BeTrue(html);
        return html;
    }
}
