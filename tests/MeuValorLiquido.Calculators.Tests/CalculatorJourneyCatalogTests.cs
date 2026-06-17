namespace MeuValorLiquido.Calculators.Tests;

public sealed class CalculatorJourneyCatalogTests
{
    [Fact]
    public void Proposta_Recebida_Should_Expose_Two_Next_Steps_From_Entry()
    {
        var journey = CalculatorJourneyCatalog.TryGetByEntrySlug("proposta-salarial");
        journey.Should().NotBeNull();

        var steps = CalculatorJourneyCatalog.GetRemainingSteps(journey!, "proposta-salarial");
        steps.Should().HaveCount(2);
        steps[0].Target.Should().Be("salario-liquido");
        steps[1].Target.Should().Be("pj-vs-clt");
    }

    [Fact]
    public void Saida_Empresa_Should_Expose_Fgts_And_Faq_From_Rescisao()
    {
        var journey = CalculatorJourneyCatalog.TryGetByEntrySlug("rescisao-clt");
        journey.Should().NotBeNull();

        var steps = CalculatorJourneyCatalog.GetRemainingSteps(journey!, "rescisao-clt");
        steps.Should().HaveCount(2);
        steps[0].Target.Should().Be("fgts");
        steps[1].Kind.Should().Be(CalculatorJourneyStepKind.Faq);
    }

    [Fact]
    public void Liquido_Desejado_Should_Map_Bruto_To_Salario_Liquido_Input()
    {
        var service = CalculatorTestFactory.CreateService();
        var sourceInput = new CalculatorInput(
            Amount: 4000m,
            Dependents: 1,
            TransportDiscount: 200m,
            MealVoucherDiscount: 80m);
        var sourceResult = service.Calculate("salario-bruto-necessario", sourceInput);
        sourceResult.IsSuccess.Should().BeTrue();

        var mapped = CalculatorJourneyInputMapper.MapForCalculatorStep(
            CalculatorJourneyCatalog.LiquidoDesejado,
            "salario-liquido",
            sourceInput,
            sourceResult.Value);

        mapped.Should().NotBeNull();
        mapped!.Amount.Should().Be(sourceResult.Value!.GrossAmount.Amount);
        mapped.Dependents.Should().Be(1);
        mapped.TransportDiscount.Should().Be(200m);
        mapped.MealVoucherDiscount.Should().Be(80m);
    }

    [Fact]
    public void Proposta_Recebida_Should_Map_Proposed_Gross_To_Liquido()
    {
        var input = new CalculatorInput(4000m, SecondaryAmount: 4800m, TransportDiscount: 150m);
        var mapped = CalculatorJourneyInputMapper.MapForCalculatorStep(
            CalculatorJourneyCatalog.PropostaRecebida,
            "salario-liquido",
            input,
            null);

        mapped!.Amount.Should().Be(4800m);
        mapped.TransportDiscount.Should().Be(150m);
    }
}
