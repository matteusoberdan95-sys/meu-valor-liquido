using System.Globalization;
using System.Text.RegularExpressions;

namespace MeuValorLiquido.WebApp.Tests;

public sealed class CalculatorSubmissionSmokeTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public CalculatorSubmissionSmokeTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient(
            new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
    }

    public static IEnumerable<object[]> AllCalculatorSlugs =>
        CalculatorSeedData.GetDefinitions().Select(definition => new object[] { definition.Slug });

    [Theory]
    [MemberData(nameof(AllCalculatorSlugs))]
    public async Task All_Calculators_Should_Submit_With_Defaults(string slug)
    {
        var getResponse = await client.GetAsync($"/calculadoras/{slug}");
        getResponse.IsSuccessStatusCode.Should().BeTrue();
        var html = await getResponse.Content.ReadAsStringAsync();
        var token = ExtractAntiforgeryToken(html);
        token.Should().NotBeNullOrEmpty($"calculadora {slug} should expose antiforgery token");

        var form = BuildFormFields(slug, token!);
        using var postResponse = await client.PostAsync($"/calculadoras/{slug}", new FormUrlEncodedContent(form));

        postResponse.StatusCode.Should().Be(HttpStatusCode.Redirect, $"calculadora {slug} should redirect after successful POST");
        postResponse.Headers.Location!.OriginalString.Should().Contain("?r=", $"calculadora {slug} should return share token");
    }

    private static Dictionary<string, string> BuildFormFields(string slug, string antiforgeryToken)
    {
        var input = CalculatorInputDefaults.ForSlug(slug);
        var fields = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["__RequestVerificationToken"] = antiforgeryToken,
            ["Input.Amount"] = FormatDecimal(input.Amount)
        };

        if (input.SecondaryAmount > 0m)
        {
            fields["Input.SecondaryAmount"] = FormatDecimal(input.SecondaryAmount);
        }

        if (input.Dependents > 0)
        {
            fields["Input.Dependents"] = input.Dependents.ToString(CultureInfo.InvariantCulture);
        }

        if (input.Months != 12)
        {
            fields["Input.Months"] = input.Months.ToString(CultureInfo.InvariantCulture);
        }

        if (input.Rate > 0m)
        {
            fields["Input.Rate"] = FormatDecimal(input.Rate);
        }

        if (input.TransportDiscount > 0m)
        {
            fields["Input.TransportDiscount"] = FormatDecimal(input.TransportDiscount);
        }

        if (input.OtherDiscounts > 0m)
        {
            fields["Input.OtherDiscounts"] = FormatDecimal(input.OtherDiscounts);
        }

        if (input.AdmissionDate is not null)
        {
            fields["Input.AdmissionDate"] = input.AdmissionDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        if (input.TerminationDate is not null)
        {
            fields["Input.TerminationDate"] = input.TerminationDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        fields["Input.TerminationReason"] = ((int)input.TerminationReason).ToString(CultureInfo.InvariantCulture);
        fields["Input.NoticePeriod"] = ((int)input.NoticePeriod).ToString(CultureInfo.InvariantCulture);
        fields["Input.MeiActivity"] = ((int)input.MeiActivity).ToString(CultureInfo.InvariantCulture);
        fields["Input.FinancingAmortization"] = ((int)input.FinancingAmortization).ToString(CultureInfo.InvariantCulture);
        fields["Input.SalaryBasis"] = ((int)input.SalaryBasis).ToString(CultureInfo.InvariantCulture);

        return fields;
    }

    private static string FormatDecimal(decimal value) =>
        value.ToString("0.##", CultureInfo.InvariantCulture);

    private static string? ExtractAntiforgeryToken(string html)
    {
        var match = Regex.Match(
            html,
            "name=\"__RequestVerificationToken\"[^>]*value=\"([^\"]+)\"",
            RegexOptions.IgnoreCase);

        if (!match.Success)
        {
            match = Regex.Match(
                html,
                "value=\"([^\"]+)\"[^>]*name=\"__RequestVerificationToken\"",
                RegexOptions.IgnoreCase);
        }

        return match.Success ? match.Groups[1].Value : null;
    }
}
