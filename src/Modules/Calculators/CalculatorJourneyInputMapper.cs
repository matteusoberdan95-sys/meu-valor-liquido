namespace MeuValorLiquido.Modules.Calculators;

public static class CalculatorJourneyInputMapper
{
    public static CalculatorInput? MapForCalculatorStep(
        string journeyId,
        string targetCalculatorSlug,
        CalculatorInput sourceInput,
        CalculationResult? sourceResult)
    {
        if (journeyId.Equals(CalculatorJourneyCatalog.PropostaRecebida, StringComparison.OrdinalIgnoreCase))
        {
            return MapPropostaRecebida(targetCalculatorSlug, sourceInput);
        }

        if (journeyId.Equals(CalculatorJourneyCatalog.LiquidoDesejado, StringComparison.OrdinalIgnoreCase))
        {
            return MapLiquidoDesejado(targetCalculatorSlug, sourceInput, sourceResult);
        }

        if (journeyId.Equals(CalculatorJourneyCatalog.SaidaEmpresa, StringComparison.OrdinalIgnoreCase))
        {
            return MapSaidaEmpresa(targetCalculatorSlug, sourceInput);
        }

        return null;
    }

    private static CalculatorInput? MapPropostaRecebida(string targetCalculatorSlug, CalculatorInput sourceInput)
    {
        var proposedGross = sourceInput.SecondaryAmount > 0m
            ? sourceInput.SecondaryAmount
            : sourceInput.Amount;

        return targetCalculatorSlug.ToLowerInvariant() switch
        {
            "salario-liquido" => sourceInput with { Amount = proposedGross },
            "pj-vs-clt" => sourceInput with { Amount = proposedGross, SecondaryAmount = 0m },
            _ => null
        };
    }

    private static CalculatorInput? MapLiquidoDesejado(
        string targetCalculatorSlug,
        CalculatorInput sourceInput,
        CalculationResult? sourceResult)
    {
        if (!targetCalculatorSlug.Equals("salario-liquido", StringComparison.OrdinalIgnoreCase)
            || sourceResult is null)
        {
            return null;
        }

        return sourceInput with { Amount = sourceResult.GrossAmount.Amount };
    }

    private static CalculatorInput? MapSaidaEmpresa(string targetCalculatorSlug, CalculatorInput sourceInput)
    {
        if (!targetCalculatorSlug.Equals("fgts", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        var months = sourceInput.Months;
        if (months <= 0 && sourceInput.AdmissionDate is not null && sourceInput.TerminationDate is not null)
        {
            months = TerminationDateHelper.CalculateTenureMonths(
                sourceInput.AdmissionDate.Value,
                sourceInput.TerminationDate.Value);
        }

        if (months <= 0)
        {
            months = 12;
        }

        return sourceInput with { Months = Math.Clamp(months, 1, 600) };
    }
}
