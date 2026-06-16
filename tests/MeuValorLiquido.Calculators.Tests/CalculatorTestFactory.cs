namespace MeuValorLiquido.Calculators.Tests;
internal static class CalculatorTestFactory
{
    public static CalculationEngine CreateEngine()
    {
        var inss = new InssCalculator();
        var irrf = new IrrfCalculator();
        var netSalary = new NetSalaryCalculator(inss, irrf);
        var cltPj = new CltPjComparisonCalculator(netSalary, inss, irrf);
        return new CalculationEngine(inss, irrf, new TerminationTaxCalculator(inss, irrf), netSalary, cltPj);
    }

    public static CalculatorApplicationService CreateService() =>
        new(new InMemoryCalculatorCatalogService(), new CalculatorInputValidator(), CreateEngine());
}
