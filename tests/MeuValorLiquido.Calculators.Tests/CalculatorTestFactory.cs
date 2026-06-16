using MeuValorLiquido.Modules.Calculators;
using MeuValorLiquido.Modules.Calculators.Tax;

namespace MeuValorLiquido.Calculators.Tests;

internal static class CalculatorTestFactory
{
    public static CalculationEngine CreateEngine()
    {
        var inss = new InssCalculator();
        var irrf = new IrrfCalculator();
        return new CalculationEngine(inss, irrf, new TerminationTaxCalculator(inss, irrf));
    }

    public static CalculatorApplicationService CreateService() =>
        new(new InMemoryCalculatorCatalogService(), new CalculatorInputValidator(), CreateEngine());
}
