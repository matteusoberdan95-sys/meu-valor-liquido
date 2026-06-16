using FluentAssertions;
using MeuValorLiquido.Modules.Calculators;
using MeuValorLiquido.Modules.Calculators.Tax;

namespace MeuValorLiquido.Calculators.Tests;

public class GrossSalarySolverTests
{
    private readonly NetSalaryCalculator netSalary = new(new InssCalculator(), new IrrfCalculator());
    private readonly CalculatorApplicationService service = CalculatorTestFactory.CreateService();

    [Fact]
    public void Solve_3000_Net_No_Discounts_Should_Match_Forward_Calculation()
    {
        var gross = GrossSalarySolver.Solve(netSalary, 3000m, 0, 0m, 0m, 0m);
        var breakdown = netSalary.Calculate(gross, 0, 0m, 0m, 0m);

        breakdown.Net.Should().BeApproximately(3000m, GrossSalarySolver.Tolerance);
        gross.Should().BeGreaterThan(3000m);
    }

    [Fact]
    public void Solve_With_Transport_And_Dependents_Should_Reach_Target_Net()
    {
        var target = 4000m;
        var gross = GrossSalarySolver.Solve(netSalary, target, 1, 240m, 0m, 0m);
        var breakdown = netSalary.Calculate(gross, 1, 240m, 0m, 0m);

        breakdown.Net.Should().BeApproximately(target, GrossSalarySolver.Tolerance);
    }

    [Fact]
    public void Required_Gross_Calculator_Should_Return_Bruto_As_Gross_Amount()
    {
        var result = service.Calculate("salario-bruto-necessario", new CalculatorInput(
            Amount: 3500m,
            Dependents: 0,
            TransportDiscount: 150m,
            SecondaryAmount: 50m,
            OtherDiscounts: 100m));

        result.IsSuccess.Should().BeTrue();
        result.Value!.GrossAmount.Amount.Should().BeGreaterThan(3500m);
        result.Value.EstimatedNetAmount.Amount.Should().BeApproximately(3500m, GrossSalarySolver.Tolerance);
        result.Value.LineItems.Should().Contain(i => i.Label == "INSS");
        result.Value.LineItems.Should().Contain(i => i.Label == "IRRF");
    }

    [Fact]
    public void Forward_And_Inverse_Should_Be_Consistent()
    {
        var forward = service.Calculate("salario-liquido", new CalculatorInput(5500m, Dependents: 2, TransportDiscount: 200m));
        forward.IsSuccess.Should().BeTrue();

        var targetNet = forward.Value!.EstimatedNetAmount.Amount;
        var inverse = service.Calculate("salario-bruto-necessario", new CalculatorInput(
            Amount: targetNet,
            Dependents: 2,
            TransportDiscount: 200m));

        inverse.IsSuccess.Should().BeTrue();
        inverse.Value!.GrossAmount.Amount.Should().BeApproximately(5500m, 0.05m);
    }
}
