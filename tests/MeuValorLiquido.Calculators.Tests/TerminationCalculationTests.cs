namespace MeuValorLiquido.Calculators.Tests;
public class TerminationCalculationTests
{
    private readonly CalculatorApplicationService service = CalculatorTestFactory.CreateService();

    private static CalculatorInput BaseInput(TerminationReason reason) =>
        new(5000m, SecondaryAmount: 15m, Months: 24, TerminationReason: reason, CompletedNoticePeriod: true);

    [Fact]
    public void Termination_Dismissal_Should_Include_Fgts_Fine_And_Notice()
    {
        var result = service.Calculate("rescisao-clt", BaseInput(TerminationReason.DismissalWithoutCause));

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(item => item.Label == "Multa FGTS (40%)");
        result.Value.LineItems.Should().Contain(item => item.Label.StartsWith("Aviso prévio indenizado"));
        result.Value.Explanation.Should().Contain("Demissão sem justa causa");
    }

    [Fact]
    public void Termination_Resignation_Should_Not_Include_Fgts_Fine()
    {
        var result = service.Calculate("rescisao-clt", BaseInput(TerminationReason.Resignation));

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().NotContain(item =>
            item.Label.StartsWith("Multa FGTS") && item.Type == CalculationLineType.Income);
        result.Value.Explanation.Should().Contain("Pedido de demissão");
    }

    [Fact]
    public void Termination_Dismissal_Should_Pay_More_Than_Resignation()
    {
        var dismissal = service.Calculate("rescisao-clt", BaseInput(TerminationReason.DismissalWithoutCause));
        var resignation = service.Calculate("rescisao-clt", BaseInput(TerminationReason.Resignation));

        dismissal.Value!.EstimatedNetAmount.Amount.Should()
            .BeGreaterThan(resignation.Value!.EstimatedNetAmount.Amount);
    }

    [Fact]
    public void Termination_Resignation_1850_9Months_NoNotice_Should_Use_Separate_Taxes()
    {
        var input = new CalculatorInput(
            Amount: 1850m,
            SecondaryAmount: 30m,
            Months: 9,
            TerminationReason: TerminationReason.Resignation,
            CompletedNoticePeriod: false);

        var result = service.Calculate("rescisao-clt", input);

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(item => item.Label == "Desconto aviso prévio (30 dias)");
        result.Value.LineItems.Should().NotContain(item => item.Label == "Férias vencidas + 1/3");
        result.Value.EstimatedNetAmount.Amount.Should().BeApproximately(2991.26m, 0.01m);
    }

    [Fact]
    public void Termination_Resignation_With_Notice_Should_Not_Deduct_Notice()
    {
        var input = new CalculatorInput(
            Amount: 1850m,
            SecondaryAmount: 30m,
            Months: 9,
            TerminationReason: TerminationReason.Resignation,
            CompletedNoticePeriod: true);

        var result = service.Calculate("rescisao-clt", input);

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().NotContain(item => item.Label.StartsWith("Desconto aviso prévio"));
        result.Value.EstimatedNetAmount.Amount.Should().BeApproximately(4841.26m, 0.01m);
    }

    [Fact]
    public void Termination_Unpaid_Vacation_Below_12_Months_Should_Be_Ignored()
    {
        var input = new CalculatorInput(
            Amount: 1850m,
            SecondaryAmount: 30m,
            Months: 9,
            TerminationReason: TerminationReason.Resignation,
            CompletedNoticePeriod: false,
            HasUnpaidVacation: true);

        var result = service.Calculate("rescisao-clt", input);

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().NotContain(item => item.Label == "Férias vencidas + 1/3");
        result.Value.Explanation.Should().Contain("12 meses");
        result.Value.EstimatedNetAmount.Amount.Should().BeApproximately(2991.26m, 0.01m);
    }

    [Fact]
    public void Termination_Resignation_10Months_12Days_Should_Be_Lower_Than_Full_Month()
    {
        var fullMonth = new CalculatorInput(
            Amount: 1850m,
            SecondaryAmount: 30m,
            Months: 10,
            MonthsWorkedInYear: 10,
            TerminationReason: TerminationReason.Resignation,
            CompletedNoticePeriod: false);

        var partialMonth = fullMonth with { SecondaryAmount = 12m };

        var full = service.Calculate("rescisao-clt", fullMonth);
        var partial = service.Calculate("rescisao-clt", partialMonth);

        partial.Value!.EstimatedNetAmount.Amount.Should()
            .BeLessThan(full.Value!.EstimatedNetAmount.Amount);
        partial.Value.EstimatedNetAmount.Amount.Should().BeApproximately(2316.10m, 1m);
    }

    [Fact]
    public void Termination_Resignation_3Months_August_Should_Match_Realistic_Low_Payout()
    {
        var input = new CalculatorInput(
            Amount: 1850m,
            SecondaryAmount: 30m,
            Months: 3,
            TerminationMonth: 8,
            AdmissionMonth: 6,
            TerminationReason: TerminationReason.Resignation,
            CompletedNoticePeriod: false);

        var result = service.Calculate("rescisao-clt", input);

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(item =>
            item.Label == "Meses considerados no 13º" && item.DisplayText == "3");
        result.Value.EstimatedNetAmount.Amount.Should().BeApproximately(902.30m, 0.01m);
    }

    [Fact]
    public void Termination_Should_Adjust_Months_When_Dates_Contradict_Manual_Input()
    {
        var input = new CalculatorInput(
            Amount: 1850m,
            SecondaryAmount: 30m,
            Months: 11,
            AdmissionMonth: 9,
            TerminationMonth: 11,
            TerminationReason: TerminationReason.Resignation,
            CompletedNoticePeriod: false);

        var result = service.Calculate("rescisao-clt", input);

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(item =>
            item.Label == "Meses considerados no 13º" && item.DisplayText == "3");
        result.Value.EstimatedNetAmount.Amount.Should().BeApproximately(902.30m, 0.01m);
        result.Value.Explanation.Should().Contain("ajustado de 11 para 3");
    }

    [Fact]
    public void Termination_Resignation_10Months_August_Should_Use_Eight_Thirteenth_Avos()
    {
        var input = new CalculatorInput(
            Amount: 1850m,
            SecondaryAmount: 30m,
            Months: 10,
            TerminationMonth: 8,
            TerminationReason: TerminationReason.Resignation,
            CompletedNoticePeriod: false);

        var result = service.Calculate("rescisao-clt", input);

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(item =>
            item.Label == "Meses considerados no 13º" && item.DisplayText == "8");
        result.Value.EstimatedNetAmount.Amount.Should().BeApproximately(3054.21m, 0.01m);
        result.Value.Explanation.Should().Contain("ano anterior");
    }

    [Fact]
    public void Termination_Resignation_Jan_To_Nov_Should_Match_Eleven_Months()
    {
        var input = new CalculatorInput(
            Amount: 1850m,
            SecondaryAmount: 30m,
            Months: 11,
            AdmissionMonth: 1,
            TerminationMonth: 11,
            TerminationReason: TerminationReason.Resignation,
            CompletedNoticePeriod: false);

        var result = service.Calculate("rescisao-clt", input);

        result.IsSuccess.Should().BeTrue();
        result.Value!.EstimatedNetAmount.Amount.Should().BeApproximately(3686.45m, 0.01m);
    }

    [Fact]
    public void Termination_Resignation_10Months_Left_In_February_Should_Match_Low_Payout()
    {
        var input = new CalculatorInput(
            Amount: 1850m,
            SecondaryAmount: 12m,
            Months: 10,
            TerminationMonth: 2,
            TransportDiscount: 200m,
            TerminationReason: TerminationReason.Resignation,
            CompletedNoticePeriod: false);

        var result = service.Calculate("rescisao-clt", input);

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(item =>
            item.Label == "Meses considerados no 13º" && item.DisplayText == "2");
        result.Value.EstimatedNetAmount.Amount.Should().BeApproximately(975.26m, 0.01m);
    }

    [Fact]
    public void Termination_Short_Tenure_Should_Match_Realistic_Low_Payout()
    {
        var input = new CalculatorInput(
            Amount: 1850m,
            SecondaryAmount: 30m,
            Months: 3,
            MonthsWorkedInYear: 3,
            TerminationReason: TerminationReason.Resignation,
            CompletedNoticePeriod: false);

        var result = service.Calculate("rescisao-clt", input);

        result.IsSuccess.Should().BeTrue();
        result.Value!.EstimatedNetAmount.Amount.Should().BeApproximately(902.30m, 0.01m);
    }

    [Fact]
    public void Termination_Should_Include_Unpaid_Vacation_When_Flagged_And_12_Months()
    {
        var input = new CalculatorInput(
            Amount: 3000m,
            SecondaryAmount: 20m,
            Months: 12,
            TerminationReason: TerminationReason.Resignation,
            CompletedNoticePeriod: true,
            HasUnpaidVacation: true);

        var result = service.Calculate("rescisao-clt", input);

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(item => item.Label == "Férias vencidas + 1/3");
        result.Value.LineItems.Should().Contain(item =>
            item.Label == "Férias proporcionais + 1/3" && item.Type == CalculationLineType.Information);
    }

    [Fact]
    public void Termination_Dismissal_24Months_Should_Estimate_Fgts_On_Full_Tenure()
    {
        var input = BaseInput(TerminationReason.DismissalWithoutCause) with { Months = 24 };
        var result = service.Calculate("rescisao-clt", input);

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(item =>
            item.Label == "Saldo FGTS estimado (8% × meses)" && item.Amount.Amount == 9600m);
    }

    [Fact]
    public void Termination_MutualAgreement_Should_Use_20Percent_Fgts_And_Half_Notice()
    {
        var input = BaseInput(TerminationReason.MutualAgreement);

        var result = service.Calculate("rescisao-clt", input);

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(item => item.Label == "Multa FGTS (20%)");
        result.Value.LineItems.Should().Contain(item => item.Label.StartsWith("Aviso prévio indenizado 50%"));
        result.Value.Explanation.Should().Contain("484-A");
    }

    [Fact]
    public void Termination_ForCause_Should_Only_Pay_Salary_Balance()
    {
        var input = new CalculatorInput(
            Amount: 4000m,
            SecondaryAmount: 10m,
            Months: 18,
            TerminationReason: TerminationReason.DismissalForCause,
            CompletedNoticePeriod: true);

        var result = service.Calculate("rescisao-clt", input);

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(item => item.Label == "Saldo de salário");
        result.Value.LineItems.Should().NotContain(item =>
            item.Label == "13º proporcional" && item.Type == CalculationLineType.Income);
        result.Value.EstimatedNetAmount.Amount.Should().BeApproximately(1233.33m, 0.01m);
    }

    [Fact]
    public void Termination_VacationTakenInPeriod_Should_Exclude_Proportional_Vacation()
    {
        var withVacation = new CalculatorInput(
            Amount: 3000m,
            SecondaryAmount: 20m,
            Months: 12,
            TerminationReason: TerminationReason.Resignation,
            CompletedNoticePeriod: true);

        var vacationTaken = withVacation with { VacationTakenInCurrentPeriod = true };

        var baseline = service.Calculate("rescisao-clt", withVacation);
        var taken = service.Calculate("rescisao-clt", vacationTaken);

        taken.Value!.EstimatedNetAmount.Amount.Should()
            .BeLessThan(baseline.Value!.EstimatedNetAmount.Amount);
    }

    [Fact]
    public void Termination_PartialVacationMonths_Should_Reduce_Proportional_Vacation()
    {
        var input = new CalculatorInput(
            Amount: 3000m,
            SecondaryAmount: 20m,
            Months: 12,
            MonthsSinceLastVacation: 4,
            TerminationReason: TerminationReason.Resignation,
            CompletedNoticePeriod: true);

        var result = service.Calculate("rescisao-clt", input);

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(item =>
            item.Label == "Férias proporcionais + 1/3" && item.Type == CalculationLineType.Income);
        result.Value.LineItems.Single(item => item.Label == "Férias proporcionais + 1/3").Amount.Amount
            .Should().BeApproximately(1333.33m, 1m);
    }
}
