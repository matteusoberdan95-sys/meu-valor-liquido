namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint77TaxTablesBadgeAndFaqTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint77TaxTablesBadgeAndFaqTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Theory]
    [InlineData("inss")]
    [InlineData("ferias")]
    [InlineData("proposta-salarial")]
    public async Task TemplateC1_InssIrrf_Calculator_Should_Show_Tax_Tables_Badge(string slug)
    {
        var html = await client.GetStringAsync($"/calculadoras/{slug}");

        html.Should().Contain("valora-tax-tables-badge");
        html.Should().Contain("INSS/IRRF 2026");
        html.Should().Contain("Revisado em");
        html.Should().Contain("/como-calculamos");
    }

    [Theory]
    [InlineData("juros-compostos")]
    [InlineData("financiamento")]
    [InlineData("multa-atraso")]
    public async Task TemplateC1_Financial_Calculator_Should_Not_Show_Tax_Tables_Badge(string slug)
    {
        var html = await client.GetStringAsync($"/calculadoras/{slug}");

        html.Should().NotContain("valora-tax-tables-badge");
    }

    [Fact]
    public void TaxTablesBadgeHelper_Should_Target_InssIrrf_TemplateC1_Slugs()
    {
        TaxTablesBadgeHelper.ShouldShowOnCalculator("inss").Should().BeTrue();
        TaxTablesBadgeHelper.ShouldShowOnCalculator("ferias").Should().BeTrue();
        TaxTablesBadgeHelper.ShouldShowOnCalculator("juros-compostos").Should().BeFalse();
        TaxTablesBadgeHelper.FormatBadgeLabel().Should().Contain("INSS/IRRF 2026");
    }

    [Theory]
    [InlineData("como-conferir-holerite")]
    [InlineData("reducao-irrf-2026")]
    [InlineData("teto-inss-2026")]
    [InlineData("vender-ferias-abono-pecuniario")]
    [InlineData("acordo-demissao-484-a")]
    [InlineData("desconto-plano-saude-folha")]
    [InlineData("fgts-saque-rescisao")]
    public async Task New_Faq_Pages_Should_Be_Indexable(string slug)
    {
        var html = await client.GetStringAsync($"/duvidas/{slug}");

        html.Should().Contain("FAQPage");
        html.Should().Contain(slug);
        html.Should().Contain("Simule agora");
    }

    [Fact]
    public void PopularQuestionsCatalog_Should_Include_Seven_New_Faqs()
    {
        PopularQuestionsCatalog.GetAll().Should().HaveCountGreaterThanOrEqualTo(25);

        var newSlugs = new[]
        {
            "como-conferir-holerite",
            "reducao-irrf-2026",
            "teto-inss-2026",
            "vender-ferias-abono-pecuniario",
            "acordo-demissao-484-a",
            "desconto-plano-saude-folha",
            "fgts-saque-rescisao"
        };

        foreach (var slug in newSlugs)
        {
            PopularQuestionsCatalog.GetBySlug(slug).Should().NotBeNull();
        }
    }
}
