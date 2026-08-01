namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint87CalculatorEditorialTests : IClassFixture<WebApplicationFactory<Program>>
{
    private static readonly string[] PrioritySlugs =
    [
        "salario-liquido",
        "rescisao-clt",
        "ferias",
        "decimo-terceiro",
        "inss",
        "irrf",
        "hora-extra",
        "fgts",
        "pj-vs-clt",
        "simulador-mei",
        "juros-compostos",
        "financiamento",
        "salario-bruto-necessario",
        "proposta-salarial",
        "seguro-desemprego",
        "vale-transporte-hibrido",
        "custo-funcionario",
        "multa-atraso",
        "conversor-salario"
    ];

    private readonly WebApplicationFactory<Program> factory;
    private readonly HttpClient client;

    public Sprint87CalculatorEditorialTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing"));
        client = this.factory.CreateClient();
    }

    [Fact]
    public void Editorial_Catalog_Should_Cover_Priority_Calculators_With_Unique_Content()
    {
        var contents = CalculatorEditorialCatalog.All.ToList();

        contents.Should().HaveCount(PrioritySlugs.Length);
        contents.Select(content => content.Slug).Should().BeEquivalentTo(PrioritySlugs);
        contents.Select(content => content.Summary).Should().OnlyHaveUniqueItems();
        contents.Select(content => content.HowItWorks).Should().OnlyHaveUniqueItems();

        foreach (var content in contents)
        {
            content.IncludedItems.Should().HaveCountGreaterThanOrEqualTo(3);
            content.ExcludedItems.Should().HaveCountGreaterThanOrEqualTo(3);
            content.CommonErrors.Should().HaveCountGreaterThanOrEqualTo(3);
            content.Sources.Should().NotBeEmpty();
            content.Sources.Should().OnlyContain(source => source.Url.StartsWith("https://"));
            content.RelatedCalculatorSlugs.Should().HaveCountGreaterThanOrEqualTo(2);
            CalculatorEditorialCatalog.GetFaqs(content.Slug).Should().HaveCountGreaterThanOrEqualTo(2);
            content.LastReviewedAt.Should().Be(new DateOnly(2026, 8, 1));
            content.ReviewedBy.Should().Contain("Matteus Oberdan");
            content.EducationalNotice.ToLowerInvariant().Should().NotContain("resultado garantido");
        }

        contents
            .SelectMany(content => CalculatorEditorialCatalog.GetFaqs(content.Slug))
            .Select(faq => faq.Question)
            .Should()
            .OnlyHaveUniqueItems();
    }

    [Theory]
    [MemberData(nameof(PriorityCalculatorSlugs))]
    public async Task Priority_Page_Should_Render_Complete_Editorial_Content_And_Domain_Example(string slug)
    {
        var content = CalculatorEditorialCatalog.GetBySlug(slug)!;
        using var scope = factory.Services.CreateScope();
        var calculatorService = scope.ServiceProvider.GetRequiredService<ICalculatorApplicationService>();
        var exampleResult = calculatorService.Calculate(slug, content.Example.Input);

        var html = WebUtility.HtmlDecode(await client.GetStringAsync($"/calculadoras/{slug}"));

        exampleResult.IsFailure.Should().BeFalse();
        html.Should().Contain("Como esta calculadora funciona");
        html.Should().Contain("aria-label=\"Etapas desta calculadora\"");
        html.Should().Contain("href=\"#simular\"");
        html.Should().Contain("href=\"#resultado\"");
        html.Should().Contain("href=\"#entenda-calculo\"");
        html.Should().Contain("href=\"#fontes-calculo\"");
        html.Should().Contain("href=\"#perguntas-frequentes\"");
        html.Should().Contain("O que entra no cálculo");
        html.Should().Contain("O que não entra no cálculo");
        html.Should().Contain("Como interpretar o resultado");
        html.Should().Contain("Exemplo calculado pelo mesmo motor");
        html.Should().Contain(content.Example.Title);
        html.Should().Contain("Resultado principal da simulação");
        html.Should().Contain(exampleResult.Value.EstimatedNetAmount.ToString());
        html.Should().Contain("Erros comuns");
        html.Should().Contain("Fontes oficiais");
        html.Should().Contain("Última revisão");
        html.Should().Contain("Matteus Oberdan");
        html.Should().Contain("Calculadoras relacionadas");
        html.Should().Contain(content.EducationalNotice);
        html.Should().Contain(CalculatorEditorialCatalog.GetFaqs(slug)[0].Question);
        html.Should().Contain("data-editorial-example");
        html.Should().Contain("data-editorial-sources");
        html.Should().Contain("data-editorial-related");
        html.Should().Contain($"href=\"{EditorialAuthorCatalog.Primary.ProfilePath}\"");
    }

    [Theory]
    [MemberData(nameof(PriorityCalculatorSlugs))]
    public async Task Embed_Should_Not_Render_Long_Editorial_Content(string slug)
    {
        if (!EmbedWidgetCatalog.IsEmbeddable(slug))
        {
            return;
        }

        var html = await client.GetStringAsync($"/calculadoras/{slug}?embed=1");

        html.Should().NotContain("valora-calculator-editorial");
        html.Should().NotContain("Exemplo calculado pelo mesmo motor");
        html.Should().NotContain("Etapas desta calculadora");
        html.Should().NotContain("data-editorial-example");
    }

    [Theory]
    [InlineData("seguro-desemprego")]
    [InlineData("conversor-salario")]
    [InlineData("custo-funcionario")]
    [InlineData("multa-atraso")]
    public async Task How_To_Use_Block_Should_Prefer_Editorial_Summary(string slug)
    {
        var content = CalculatorEditorialCatalog.GetBySlug(slug)!;
        var html = WebUtility.HtmlDecode(await client.GetStringAsync($"/calculadoras/{slug}"));

        html.Should().Contain("Como usar esta calculadora");
        html.Should().Contain(content.Summary);
        html.Should().NotContain("Use os campos indicados para obter uma estimativa educativa");
    }

    [Fact]
    public async Task Editorial_Journey_Should_Render_In_Useful_Order()
    {
        var html = await client.GetStringAsync("/calculadoras/salario-liquido");

        var formIndex = html.IndexOf("id=\"simular\"", StringComparison.Ordinal);
        var resultIndex = html.IndexOf("id=\"resultado\"", StringComparison.Ordinal);
        var editorialIndex = html.IndexOf("id=\"entenda-calculo\"", StringComparison.Ordinal);
        var sourcesIndex = html.IndexOf("id=\"fontes-calculo\"", StringComparison.Ordinal);
        var faqIndex = html.IndexOf("id=\"perguntas-frequentes\"", StringComparison.Ordinal);

        formIndex.Should().BeGreaterThan(0);
        resultIndex.Should().BeGreaterThan(formIndex);
        editorialIndex.Should().BeGreaterThan(resultIndex);
        sourcesIndex.Should().BeGreaterThan(editorialIndex);
        faqIndex.Should().BeGreaterThan(sourcesIndex);
    }

    public static TheoryData<string> PriorityCalculatorSlugs()
    {
        var data = new TheoryData<string>();
        foreach (var slug in PrioritySlugs)
        {
            data.Add(slug);
        }

        return data;
    }
}
