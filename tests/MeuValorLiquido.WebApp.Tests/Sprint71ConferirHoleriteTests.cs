namespace MeuValorLiquido.WebApp.Tests;

using System.Globalization;
using System.Text.RegularExpressions;

public sealed class Sprint71ConferirHoleriteTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint71ConferirHoleriteTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Fact]
    public async Task Conferir_Holerite_Page_Should_Load_With_Seo()
    {
        var html = await client.GetStringAsync("/conferir-holerite");

        html.Should().Contain("Seu holerite está certo?");
        html.Should().Contain("Diagnóstico do Holerite");
        html.Should().Contain("Gerar diagnóstico do holerite");
        html.Should().Contain("Como funciona o diagnóstico");
        html.Should().Contain("INSS descontado");
        html.Should().Contain("rel=\"canonical\"");
    }

    [Fact]
    public async Task Conferir_Holerite_Post_Should_Show_Match_Result()
    {
        var html = await PostValidationAsync(new Dictionary<string, string>
        {
            ["Input.GrossSalary"] = FormatDecimal(3000m),
            ["Input.Dependents"] = "0",
            ["Input.ReportedInss"] = FormatDecimal(248.60m),
            ["Input.ReportedIrrf"] = FormatDecimal(0m)
        });

        html.Should().Contain("Seu holerite parece correto");
        html.Should().Contain("O que conferir no RH");
        html.Should().Contain("Abrir salário líquido");
        html.Should().Contain("INSS");
    }

    [Fact]
    public async Task Conferir_Holerite_Post_Should_Show_Mismatch_Result()
    {
        var html = await PostValidationAsync(new Dictionary<string, string>
        {
            ["Input.GrossSalary"] = FormatDecimal(3000m),
            ["Input.Dependents"] = "0",
            ["Input.ReportedInss"] = FormatDecimal(400m),
            ["Input.ReportedIrrf"] = FormatDecimal(0m)
        });

        html.Should().Contain("valora-stitch-payslip-diagnosis--danger");
        html.Should().Contain("valora-stitch-payslip-diagnostic-card--warn");
        html.Should().Contain("Tirar dúvida no chat");
    }

    [Fact]
    public async Task Sitemap_Should_Include_Conferir_Holerite()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");
        xml.Should().Contain("/conferir-holerite");
    }

    [Fact]
    public async Task Negociar_Salario_Hub_Should_Link_Conferir_Holerite()
    {
        var html = await client.GetStringAsync("/negociar-salario");
        html.Should().Contain("/conferir-holerite");
    }

    [Fact]
    public async Task Blog_Como_Conferir_Holerite_Should_Link_Tool()
    {
        var html = await client.GetStringAsync("/blog/como-conferir-holerite");
        html.Should().Contain("/conferir-holerite");
    }

    private async Task<string> PostValidationAsync(Dictionary<string, string> fields)
    {
        var getHtml = await client.GetStringAsync("/conferir-holerite");
        var token = ExtractAntiforgeryToken(getHtml);
        token.Should().NotBeNullOrEmpty();

        fields["__RequestVerificationToken"] = token!;
        using var response = await client.PostAsync("/conferir-holerite", new FormUrlEncodedContent(fields));
        response.IsSuccessStatusCode.Should().BeTrue();
        return await response.Content.ReadAsStringAsync();
    }

    private static string FormatDecimal(decimal value) =>
        value.ToString("0.##", CultureInfo.GetCultureInfo("pt-BR"));

    private static string? ExtractAntiforgeryToken(string html)
    {
        var match = Regex.Match(html, "name=\"__RequestVerificationToken\"[^>]*value=\"([^\"]+)\"");
        if (match.Success)
        {
            return match.Groups[1].Value;
        }

        match = Regex.Match(html, "value=\"([^\"]+)\"[^>]*name=\"__RequestVerificationToken\"");
        return match.Success ? match.Groups[1].Value : null;
    }
}
