namespace MeuValorLiquido.Calculators.Tests;

public class SalaryProposalTests
{
    private readonly CalculatorApplicationService service = CalculatorTestFactory.CreateService();

    [Fact]
    public void Catalog_Should_Contain_Salary_Proposal_Calculator()
    {
        var catalog = new InMemoryCalculatorCatalogService();

        catalog.GetAll().Should().HaveCount(19);
        catalog.GetBySlug("proposta-salarial").Should().NotBeNull();
    }

    [Fact]
    public void Salary_Proposal_Should_Show_Net_Gain_And_Percentages()
    {
        var result = service.Calculate(
            "proposta-salarial",
            new CalculatorInput(4000m, SecondaryAmount: 4800m, Dependents: 0, TransportDiscount: 200m));

        result.IsSuccess.Should().BeTrue();
        result.Value.LineItems.Should().Contain(item => item.Label == "Ganho líquido mensal");
        result.Value.LineItems.Should().Contain(item => item.Label == "Aumento no bruto" && item.DisplayText == "20%");
        result.Value.LineItems.Should().Contain(item => item.Label == "Líquido proposto estimado");
        result.Value.Explanation.Should().Contain("Impostos progressivos");
    }

    [Fact]
    public void Salary_Proposal_Should_Reject_Missing_Proposed_Gross()
    {
        var result = service.Calculate(
            "proposta-salarial",
            new CalculatorInput(4000m, SecondaryAmount: 0m));

        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain("proposto");
    }

    [Fact]
    public void Share_Text_Should_Highlight_Monthly_And_Annual_Gain()
    {
        var result = service.Calculate(
            "proposta-salarial",
            new CalculatorInput(4000m, SecondaryAmount: 4800m));

        result.IsSuccess.Should().BeTrue();
        var shareText = CalculatorShareTextBuilder.Build(result.Value, "https://example.com/share");

        shareText.Should().Contain("Proposta salarial");
        shareText.Should().Contain("Ganho líquido");
        shareText.Should().Contain("Atual:");
        shareText.Should().Contain("Proposta:");
    }
}
