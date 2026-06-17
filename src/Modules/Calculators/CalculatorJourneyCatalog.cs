namespace MeuValorLiquido.Modules.Calculators;

public enum CalculatorJourneyStepKind
{
    Calculator,
    Faq,
    SalaryBand
}

public sealed record CalculatorJourneyStepDefinition(
    CalculatorJourneyStepKind Kind,
    string Target,
    string Teaser);

public sealed record CalculatorJourneyDefinition(
    string Id,
    string Title,
    string Introduction,
    string EntrySlug,
    IReadOnlyList<CalculatorJourneyStepDefinition> Steps);

public static class CalculatorJourneyCatalog
{
    public const string PropostaRecebida = "proposta-recebida";
    public const string SaidaEmpresa = "saida-empresa";
    public const string LiquidoDesejado = "liquido-desejado";

    private static readonly IReadOnlyList<CalculatorJourneyDefinition> All =
    [
        new(
            PropostaRecebida,
            "Proposta recebida",
            "Você comparou bruto e líquido. O próximo passo é validar o holerite completo e, se fizer sentido, o modelo PJ.",
            "proposta-salarial",
            [
                new(CalculatorJourneyStepKind.Calculator, "proposta-salarial", "Compare salário atual e proposto pelo que entra no bolso."),
                new(CalculatorJourneyStepKind.Calculator, "salario-liquido", "Monte o extrato do bruto proposto com os mesmos descontos do holerite."),
                new(CalculatorJourneyStepKind.Calculator, "pj-vs-clt", "Se a negociação permitir PJ, compare líquido pessoal e custo total.")
            ]),
        new(
            SaidaEmpresa,
            "Saída da empresa",
            "A rescisão é só parte do pacote. Estime FGTS e revise direitos que costumam surgir após o desligamento.",
            "rescisao-clt",
            [
                new(CalculatorJourneyStepKind.Calculator, "rescisao-clt", "Simule verbas rescisórias conforme o motivo do desligamento."),
                new(CalculatorJourneyStepKind.Calculator, "fgts", "Estime depósitos, saldo e multa rescisória para o seu tipo de saída."),
                new(CalculatorJourneyStepKind.Faq, "multa-fgts-40-porcento", "Entenda quando há multa de 40% e como isso se relaciona ao saque do FGTS."),
                new(CalculatorJourneyStepKind.Faq, "seguro-desemprego-quando-tem-direito", "Saiba quando pode solicitar o seguro-desemprego após demissão sem justa causa.")
            ]),
        new(
            LiquidoDesejado,
            "Líquido desejado",
            "Você definiu uma meta de bolso. Confira o extrato do bruto encontrado e compare com faixas salariais comuns.",
            "salario-bruto-necessario",
            [
                new(CalculatorJourneyStepKind.Calculator, "salario-bruto-necessario", "Descubra o bruto necessário para o líquido informado."),
                new(CalculatorJourneyStepKind.Calculator, "salario-liquido", "Valide o extrato a partir do bruto estimado e dos mesmos descontos."),
                new(CalculatorJourneyStepKind.SalaryBand, "nearest", "Veja uma página de referência para um bruto próximo do valor encontrado.")
            ])
    ];

    private static readonly Dictionary<string, CalculatorJourneyDefinition> ById =
        All.ToDictionary(journey => journey.Id, StringComparer.OrdinalIgnoreCase);

    private static readonly Dictionary<string, CalculatorJourneyDefinition> ByEntrySlug =
        All.ToDictionary(journey => journey.EntrySlug, StringComparer.OrdinalIgnoreCase);

    public static CalculatorJourneyDefinition? TryGet(string? journeyId) =>
        string.IsNullOrWhiteSpace(journeyId) || !ById.TryGetValue(journeyId, out var journey)
            ? null
            : journey;

    public static CalculatorJourneyDefinition? TryGetByEntrySlug(string slug) =>
        ByEntrySlug.TryGetValue(slug, out var journey) ? journey : null;

    public static IReadOnlyList<CalculatorJourneyStepDefinition> GetRemainingSteps(
        CalculatorJourneyDefinition journey,
        string currentSlug)
    {
        var currentIndex = -1;
        for (var i = 0; i < journey.Steps.Count; i++)
        {
            var step = journey.Steps[i];
            if (step.Kind != CalculatorJourneyStepKind.Calculator
                || !step.Target.Equals(currentSlug, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            currentIndex = i;
            break;
        }

        return currentIndex < 0
            ? []
            : journey.Steps.Skip(currentIndex + 1).ToList();
    }
}
