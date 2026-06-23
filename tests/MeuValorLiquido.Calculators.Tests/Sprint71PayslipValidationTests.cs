namespace MeuValorLiquido.Calculators.Tests;

public sealed class Sprint71PayslipValidationTests
{
    private readonly IPayslipValidationService service = CreateService();

    [Fact]
    public void Matching_Holerite_Should_Pass_Inss_And_Irrf()
    {
        var result = service.Validate(new PayslipValidationInput(
            GrossSalary: 3000m,
            Dependents: 0,
            ReportedInss: 248.60m,
            ReportedIrrf: 0m));

        result.AllMatched.Should().BeTrue();
        result.Checks.Should().HaveCount(2);
        result.Checks.Should().OnlyContain(check => check.Status == PayslipValidationStatus.Match);
    }

    [Fact]
    public void Wrong_Inss_Should_Flag_Mismatch()
    {
        var result = service.Validate(new PayslipValidationInput(
            GrossSalary: 3000m,
            Dependents: 0,
            ReportedInss: 300m,
            ReportedIrrf: 0m));

        result.AllMatched.Should().BeFalse();
        result.Checks.Single(check => check.Key == "inss").Status.Should().Be(PayslipValidationStatus.Mismatch);
        result.Checks.Single(check => check.Key == "irrf").Status.Should().Be(PayslipValidationStatus.Match);
    }

    [Fact]
    public void Wrong_Irrf_Should_Flag_Mismatch()
    {
        var expected = service.Validate(new PayslipValidationInput(5000m, 0, 0m, 0m));
        var wrongIrrf = expected.ExpectedBreakdown.Irrf + 50m;

        var result = service.Validate(new PayslipValidationInput(
            GrossSalary: 5000m,
            Dependents: 0,
            ReportedInss: expected.ExpectedBreakdown.Inss,
            ReportedIrrf: wrongIrrf));

        result.AllMatched.Should().BeFalse();
        result.Checks.Single(check => check.Key == "irrf").Status.Should().Be(PayslipValidationStatus.Mismatch);
    }

    [Fact]
    public void Net_Check_Should_Be_Included_When_Reported()
    {
        var baseline = service.Validate(new PayslipValidationInput(3000m, 0, 248.60m, 0m));

        var result = service.Validate(new PayslipValidationInput(
            GrossSalary: 3000m,
            Dependents: 0,
            ReportedInss: 248.60m,
            ReportedIrrf: 0m,
            ReportedNet: baseline.ExpectedBreakdown.Net));

        result.Checks.Should().HaveCount(3);
        result.Checks.Single(check => check.Key == "net").Status.Should().Be(PayslipValidationStatus.Match);
    }

    [Fact]
    public void Tolerance_Should_Accept_One_Real_Difference()
    {
        var baseline = service.Validate(new PayslipValidationInput(3000m, 0, 248.60m, 0m));

        var result = service.Validate(new PayslipValidationInput(
            GrossSalary: 3000m,
            Dependents: 0,
            ReportedInss: baseline.ExpectedBreakdown.Inss + 0.80m,
            ReportedIrrf: 0m));

        result.Checks.Single(check => check.Key == "inss").Status.Should().Be(PayslipValidationStatus.Match);
    }

    private static PayslipValidationService CreateService()
    {
        var inss = new InssCalculator();
        var irrf = new IrrfCalculator();
        return new PayslipValidationService(new NetSalaryCalculator(inss, irrf));
    }
}
