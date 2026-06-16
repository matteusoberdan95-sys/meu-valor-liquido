namespace MeuValorLiquido.Calculators.Tests;

public class CalculatorSimpleExplanationTests
{
    private readonly ICalculatorApplicationService calculatorService =
        CalculatorTestFactory.CreateService();

    [Fact]
    public void Build_Should_Create_Steps_For_Net_Salary()
    {
        var input = new CalculatorInput(3000m);
        var result = calculatorService.Calculate("salario-liquido", input);

        result.IsSuccess.Should().BeTrue();
        var explanation = CalculatorSimpleExplanationBuilder.Build("salario-liquido", input, result.Value);

        explanation.Steps.Should().NotBeEmpty();
        explanation.Steps[0].Title.Should().Contain("bruto");
        explanation.Steps.Should().Contain(step => step.Title.Contains("INSS", StringComparison.OrdinalIgnoreCase));
        explanation.Steps.Last().Title.Should().Be("O que isso significa na prática");
    }

    [Fact]
    public void Build_Should_Create_Steps_For_Salary_Band()
    {
        var breakdown = new NetSalaryBreakdown(3000m, 250m, 100m, 0m, 0m, 0m, 2650m);
        var explanation = CalculatorSimpleExplanationBuilder.BuildForSalaryBand(3000, breakdown);

        explanation.Steps.Should().HaveCount(4);
        explanation.Summary.Should().Contain("3.000");
    }

    [Fact]
    public void Related_Links_Should_Include_INSS_For_Net_Salary()
    {
        var links = CalculatorRelatedLinksCatalog.GetForSlug("salario-liquido");

        links.Should().Contain(link => link.Slug == "inss");
        links.Should().Contain(link => link.Slug == "salario-bruto-necessario");
    }

    [Fact]
    public void Related_Links_Should_Fallback_For_Unknown_Slug()
    {
        var links = CalculatorRelatedLinksCatalog.GetForSlug("slug-inexistente");

        links.Should().NotBeEmpty();
        links[0].Slug.Should().Be("salario-liquido");
    }
}
