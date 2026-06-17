namespace MeuValorLiquido.Calculators.Tests;

public sealed class Sprint53HoleriteTests
{
    private readonly CalculatorApplicationService service = CalculatorTestFactory.CreateService();
    private readonly NetSalaryCalculator netSalary = new(new InssCalculator(), new IrrfCalculator());

    [Fact]
    public void Net_Salary_Should_Separate_Holerite_Discounts_In_Extrato()
    {
        var result = service.Calculate(
            "salario-liquido",
            new CalculatorInput(
                Amount: 5000m,
                Dependents: 0,
                TransportDiscount: 200m,
                MealVoucherDiscount: 80m,
                HealthPlanDiscount: 120m,
                AlimonyAmount: 300m,
                OtherDiscounts: 50m));

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(i => i.Label == "Vale-transporte" && i.Amount.Amount == 200m);
        result.Value.LineItems.Should().Contain(i => i.Label == "Vale-refeição/alimentação" && i.Amount.Amount == 80m);
        result.Value.LineItems.Should().Contain(i => i.Label == "Plano de saúde" && i.Amount.Amount == 120m);
        result.Value.LineItems.Should().Contain(i => i.Label == "Pensão alimentícia" && i.Amount.Amount == 300m);
        result.Value.LineItems.Should().Contain(i => i.Label == "Outros descontos" && i.Amount.Amount == 50m);
        result.Value.LineItems.Should().NotContain(i => i.Label == "Outros descontos" && i.Amount.Amount == 550m);
    }

    [Fact]
    public void Net_Salary_Should_Show_Irrf_Exemption_When_Base_Up_To_5000()
    {
        var result = service.Calculate("salario-liquido", new CalculatorInput(Amount: 4500m));

        result.IsSuccess.Should().BeTrue();
        var irrf = result.Value!.LineItems.Single(i => i.Label == "IRRF");
        irrf.Amount.Amount.Should().Be(0m);
        irrf.DisplayText.Should().Contain("Isento");
        irrf.DisplayText.Should().Contain("15.270");
    }

    [Fact]
    public void Forward_And_Inverse_Should_Be_Parity_With_Full_Holerite_Discounts()
    {
        var input = new CalculatorInput(
            Amount: 5500m,
            Dependents: 2,
            TransportDiscount: 200m,
            MealVoucherDiscount: 60m,
            HealthPlanDiscount: 150m,
            AlimonyPercent: 10m,
            OtherDiscounts: 40m);

        var forward = service.Calculate("salario-liquido", input);
        forward.IsSuccess.Should().BeTrue();

        var targetNet = forward.Value!.EstimatedNetAmount.Amount;
        var inverse = service.Calculate(
            "salario-bruto-necessario",
            input with { Amount = targetNet });

        inverse.IsSuccess.Should().BeTrue();
        inverse.Value!.GrossAmount.Amount.Should().BeApproximately(5500m, 0.10m);
        inverse.Value.LineItems.Should().Contain(i => i.Label == "Faixa de bruto estimada");
    }

    [Fact]
    public void Required_Gross_Should_Return_Gross_Range()
    {
        var discounts = new HoleriteDiscountInput(150m, 50m, 100m, 0m, 0m, 80m);
        var range = GrossSalarySolver.SolveRange(netSalary, 3500m, 1, discounts);

        range.MinGross.Should().BeLessThanOrEqualTo(range.MidGross);
        range.MaxGross.Should().BeGreaterThanOrEqualTo(range.MidGross);

        var result = service.Calculate(
            "salario-bruto-necessario",
            new CalculatorInput(
                Amount: 3500m,
                Dependents: 1,
                TransportDiscount: 150m,
                MealVoucherDiscount: 50m,
                HealthPlanDiscount: 100m,
                OtherDiscounts: 80m));

        result.IsSuccess.Should().BeTrue();
        var band = result.Value!.LineItems.Single(i => i.Label == "Faixa de bruto estimada");
        band.DisplayText.Should().Contain("entre");
    }

    [Fact]
    public void Salary_Proposal_Scenario_3000_To_3500()
    {
        AssertProposalSuccess(3000m, 3500m, 0, 0m, 0m, 0m, 0m, 0m, 0m);
    }

    [Fact]
    public void Salary_Proposal_Scenario_4000_To_4800_With_Vt()
    {
        AssertProposalSuccess(4000m, 4800m, 0, 200m, 0m, 0m, 0m, 0m, 0m);
    }

    [Fact]
    public void Salary_Proposal_Scenario_6000_To_7000_With_Full_Discounts()
    {
        AssertProposalSuccess(6000m, 7000m, 1, 300m, 80m, 120m, 0m, 0m, 100m);
    }

    [Fact]
    public void Salary_Proposal_Scenario_5000_To_4500_Reduction()
    {
        AssertProposalSuccess(5000m, 4500m, 0, 0m, 0m, 0m, 0m, 0m, 0m);
    }

    private void AssertProposalSuccess(
        decimal currentGross,
        decimal proposedGross,
        int dependents,
        decimal transport,
        decimal meal,
        decimal health,
        decimal alimonyAmount,
        decimal alimonyPercent,
        decimal other)
    {
        var result = service.Calculate(
            "proposta-salarial",
            new CalculatorInput(
                Amount: currentGross,
                SecondaryAmount: proposedGross,
                Dependents: dependents,
                TransportDiscount: transport,
                MealVoucherDiscount: meal,
                HealthPlanDiscount: health,
                AlimonyAmount: alimonyAmount,
                AlimonyPercent: alimonyPercent,
                OtherDiscounts: other));

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(i => i.Label == "Líquido proposto estimado");
        result.Value.LineItems.Should().Contain(i =>
            i.Label == "Ganho líquido mensal" || i.Label == "Redução líquida mensal");
        result.Value.LineItems.Should().Contain(i => i.Label == "INSS");
        result.Value.LineItems.Should().Contain(i => i.Label == "IRRF");
    }

    [Fact]
    public void Alimony_Percent_Should_Scale_With_Gross()
    {
        var breakdown = netSalary.Calculate(
            6000m,
            dependents: 0,
            new HoleriteDiscountInput(0m, 0m, 0m, 0m, 10m, 0m));

        breakdown.AlimonyDiscount.Should().Be(600m);
    }

    [Fact]
    public void Legacy_Meal_Discount_On_Bruto_Necessario_Should_Use_SecondaryAmount()
    {
        var legacy = service.Calculate(
            "salario-bruto-necessario",
            new CalculatorInput(
                Amount: 3500m,
                TransportDiscount: 150m,
                SecondaryAmount: 50m,
                OtherDiscounts: 100m));

        var modern = service.Calculate(
            "salario-bruto-necessario",
            new CalculatorInput(
                Amount: 3500m,
                TransportDiscount: 150m,
                MealVoucherDiscount: 50m,
                OtherDiscounts: 100m));

        legacy.IsSuccess.Should().BeTrue();
        modern.IsSuccess.Should().BeTrue();
        legacy.Value!.GrossAmount.Amount.Should().Be(modern.Value!.GrossAmount.Amount);
    }
}
