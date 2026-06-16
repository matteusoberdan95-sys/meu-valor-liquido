namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record LocalPanelSaveContext(
    string Slug,
    string CalculatorName,
    string Summary,
    string NetAmount);

public static class LocalPanelSaveContextBuilder
{
    public static LocalPanelSaveContext FromCalculation(
        CalculatorDefinition definition,
        CalculationResult result,
        CalculatorInput input)
    {
        var summary = BuildSummary(definition.Slug, result, input);
        return new(
            definition.Slug,
            definition.Name,
            summary,
            result.EstimatedNetAmount.ToString());
    }

    public static LocalPanelSaveContext FromSalaryBand(int gross, decimal net) =>
        new(
            "salario-liquido-faixa",
            "Salário líquido por valor",
            $"Bruto {Money.From(gross)}",
            Money.From(net).ToString());

    private static string BuildSummary(string slug, CalculationResult result, CalculatorInput input)
    {
        if (slug.Equals("proposta-salarial", StringComparison.OrdinalIgnoreCase))
        {
            return $"Atual {Money.From(input.Amount)} → proposta {Money.From(input.SecondaryAmount)}";
        }

        if (slug.Equals("pj-vs-clt", StringComparison.OrdinalIgnoreCase))
        {
            return $"CLT {Money.From(input.Amount)} (Simples {input.Rate:0.#}%)";
        }

        if (slug.Equals("salario-bruto-necessario", StringComparison.OrdinalIgnoreCase))
        {
            return $"Meta líquida {Money.From(input.Amount)}";
        }

        return $"Bruto {result.GrossAmount}";
    }
}
