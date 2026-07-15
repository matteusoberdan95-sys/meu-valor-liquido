namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint84GscCtrTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint84GscCtrTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Fact]
    public void ValeTransporteHibrido_Article_Should_Target_Gsc_Query_Cluster()
    {
        var article = BlogArticleSeedData.GetAll().Single(a => a.Slug == "vale-transporte-home-office-hibrido");

        article.Title.Should().Be("Vale-transporte no trabalho híbrido: cálculo proporcional por dias presenciais");
        article.Summary.Should().Contain("vale-transporte no regime híbrido");
        article.Summary.Should().Contain("proporcional aos dias presenciais");
        article.Content.Should().Contain("vale-transporte no trabalho híbrido");
        article.Content.Should().Contain("teletrabalho");
    }

    [Fact]
    public async Task ValeTransporteHibrido_Page_Should_Render_Ctr_Title_And_Meta_Description()
    {
        var html = WebUtility.HtmlDecode(await client.GetStringAsync("/blog/vale-transporte-home-office-hibrido"));

        html.Should().Contain("Vale-transporte no trabalho híbrido: cálculo proporcional por dias presenciais");
        html.Should().Contain("Entenda como calcular vale-transporte no regime híbrido");
        html.Should().Contain("proporcional aos dias presenciais");
        html.Should().Contain("Regime híbrido fixo");
    }

    [Fact]
    public void Secondary_Gsc_Pages_Should_Use_Click_Oriented_Titles_And_Summaries()
    {
        var posts = BlogArticleSeedData.GetAll().ToDictionary(post => post.Slug, StringComparer.OrdinalIgnoreCase);

        posts["vale-refeicao-desconto-holerite"].Title.Should().Contain("Desconto de VR e VA");
        posts["vale-refeicao-desconto-holerite"].Summary.Should().Contain("descontam do salário");

        posts["home-office-clt-descontos"].Title.Should().Contain("descontos, vale-transporte e VR");
        posts["home-office-clt-descontos"].Summary.Should().Contain("regime híbrido");
        posts["home-office-clt-descontos"].Content.Should().Contain("/blog/vale-transporte-home-office-hibrido");

        posts["ferias-coletivas-clt-guia-completo"].Title.Should().Contain("como calcular");
        posts["ferias-coletivas-clt-guia-completo"].Summary.Should().Contain("férias proporcionais");

        posts["acordo-484a-verbas-e-multa-fgts"].Title.Should().Contain("Acordo 484-A CLT");
        posts["acordo-484a-verbas-e-multa-fgts"].Summary.Should().Contain("saque parcial");
    }

    [Theory]
    [InlineData(20000, 0, "Salário líquido de R$ 20.000,00 em 2026: quanto sobra do bruto?")]
    [InlineData(1621, 2, "Salário líquido de R$ 1.621,00 com 2 dependentes em 2026: quanto sobra?")]
    public void Salary_Band_Pages_Should_Lead_With_Salario_Liquido_Query(int gross, int dependents, string expectedTitle)
    {
        var breakdown = new NetSalaryCalculator(new InssCalculator(), new IrrfCalculator())
            .Calculate(gross, dependents, 0m);

        var content = SalaryBandContentBuilder.Build(gross, breakdown, dependents);

        content.Title.Should().Be(expectedTitle);
        content.Description.Should().StartWith($"Calcule o salário líquido de {SalaryBandCatalog.FormatCurrency(gross)} bruto");
    }
}
