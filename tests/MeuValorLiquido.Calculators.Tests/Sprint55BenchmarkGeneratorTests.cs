namespace MeuValorLiquido.Calculators.Tests;

public sealed class Sprint55BenchmarkGeneratorTests
{
    [Fact(Skip = "Gerador manual de benchmarks — remover Skip para imprimir valores")]
    public void Print_New_Rescisao_Benchmark_Values()
    {
        var service = CalculatorTestFactory.CreateService();
        var scenarios = new (string Name, CalculatorInput Input)[]
        {
            ("aposentadoria-36-meses", new CalculatorInput(3500m, SecondaryAmount: 20m, Months: 36, CompleteYears: 3, TerminationReason: TerminationReason.Retirement)),
            ("falecimento-empregador", new CalculatorInput(2800m, SecondaryAmount: 12m, Months: 18, TerminationReason: TerminationReason.EmployerDeath)),
            ("contrato-prazo-determinado", new CalculatorInput(2500m, SecondaryAmount: 8m, Months: 8, TerminationReason: TerminationReason.FixedTermContractEnd)),
            ("experiencia-no-prazo", new CalculatorInput(2000m, SecondaryAmount: 10m, Months: 2, TerminationReason: TerminationReason.ProbationContractCompleted)),
            ("pedido-demissao-com-aviso", new CalculatorInput(3200m, SecondaryAmount: 15m, Months: 14, TerminationReason: TerminationReason.Resignation, NoticePeriod: NoticePeriodOption.Worked)),
            ("demissao-24-meses-5000", new CalculatorInput(5000m, SecondaryAmount: 18m, Months: 24, CompleteYears: 2, TerminationReason: TerminationReason.DismissalWithoutCause)),
            ("demissao-com-adiantamento-13", new CalculatorInput(4000m, SecondaryAmount: 15m, Months: 10, TerminationMonth: 10, ThirteenthAdvancePaid: 2000m, TerminationReason: TerminationReason.DismissalWithoutCause)),
            ("demissao-com-media-he", new CalculatorInput(3000m, SalaryAverageSupplement: 600m, SecondaryAmount: 15m, Months: 12, TerminationReason: TerminationReason.DismissalWithoutCause)),
            ("rescisao-datas-regra-15-dias", new CalculatorInput(4000m, AdmissionDate: new DateOnly(2024, 3, 20), TerminationDate: new DateOnly(2026, 6, 5), TerminationReason: TerminationReason.Resignation)),
            ("acordo-484a-12-meses-saldo", new CalculatorInput(3000m, SecondaryAmount: 12m, Months: 12, FgtsBalance: 3500m, TerminationReason: TerminationReason.MutualAgreement))
        };

        foreach (var (name, input) in scenarios)
        {
            var result = service.Calculate("rescisao-clt", input);
            result.IsSuccess.Should().BeTrue(result.Error.Message);
            var r = result.Value;
            Console.WriteLine($"{name}: gross={r.GrossAmount.Amount:F2} net={r.EstimatedNetAmount.Amount:F2}");
        }
    }
}
