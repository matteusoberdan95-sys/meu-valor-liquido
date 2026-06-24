namespace MeuValorLiquido.WebApp.Infrastructure;
public sealed record SalaryBandFaqItem(string Question, string Answer);

public sealed record SalaryBandPageContent(
    int GrossAmount,
    int Dependents,
    string Title,
    string Description,
    string IntroHtml,
    string EditorialHtml,
    string ContextHtml,
    string TipsHtml,
    IReadOnlyList<SalaryBandFaqItem> FaqItems);

public static class SalaryBandContentBuilder
{
    public static SalaryBandPageContent Build(int gross, NetSalaryBreakdown breakdown, int dependents = 0)
    {
        var grossMoney = Money.From(gross);
        var netMoney = Money.From(breakdown.Net);
        var inssMoney = Money.From(breakdown.Inss);
        var irrfMoney = Money.From(breakdown.Irrf);
        var totalDiscounts = breakdown.Inss + breakdown.Irrf;
        var netPercent = gross > 0 ? breakdown.Net / gross * 100m : 0m;
        var inssBracket = DescribePrimaryInssBracket(gross);
        var irrfStatus = DescribeIrrfStatus(breakdown, dependents);
        var minWageRatio = gross / BrTaxTables2026.MinimumWage;

        var dependentPhrase = ProgrammaticDependentsCatalog.SeoPhrase(dependents);
        var title = dependents == 0
            ? $"Salário de {grossMoney}: quanto sobra líquido em {BrTaxTables2026.Year}?"
            : $"Salário de {grossMoney} {dependentPhrase}: quanto sobra líquido em {BrTaxTables2026.Year}?";
        var description =
            $"Veja quanto sobra de {grossMoney} bruto ({dependentPhrase}) após INSS ({inssMoney}) e IRRF ({irrfMoney}). " +
            $"Líquido estimado: {netMoney}. Tabelas {BrTaxTables2026.Year}, conteúdo educativo.";

        var intro =
            $"<p>Com salário bruto de <strong>{grossMoney}</strong> {dependentPhrase}, a estimativa de líquido é <strong>{netMoney}</strong> " +
            $"({netPercent:0.#}% do bruto), considerando INSS progressivo e IRRF com redução legal de {BrTaxTables2026.Year}. " +
            $"O desconto previdenciário estimado é {inssMoney}, na faixa {inssBracket}.</p>";

        var context = BuildContextParagraph(gross, minWageRatio, irrfStatus, breakdown, dependents);
        var editorial = SalaryBandEditorialCatalog.BuildEditorialHtml(gross, breakdown, SalaryBandCatalog.GetAll());
        var tips = BuildTipsParagraph(gross, dependents);

        var faq = new List<SalaryBandFaqItem>
        {
            new(
                $"Quanto é descontado de INSS em {grossMoney}?",
                $"Neste cenário, o INSS estimado é {inssMoney}. O cálculo é progressivo por faixas até o teto de {Money.From(BrTaxTables2026.InssCeiling)}."),
            new(
                $"Quanto de IRRF é descontado em {grossMoney}?",
                irrfStatus.FaqAnswer),
            new(
                $"Qual o salário líquido de {grossMoney} em {BrTaxTables2026.Year}?",
                $"Aproximadamente {netMoney}, após {inssMoney} de INSS e {irrfMoney} de IRRF ({dependentPhrase}, sem outros descontos)."),
            new(
                "Como personalizar dependentes e vale-transporte?",
                "Use a <a href=\"/calculadoras/salario-liquido\">calculadora de salário líquido</a> " +
                "ou informe o líquido desejado na <a href=\"/calculadoras/salario-bruto-necessario\">calculadora inversa</a>.")
        };

        var extraFaq = SalaryBandEditorialCatalog.BuildExtraFaq(gross, breakdown, dependents);
        if (extraFaq is not null)
        {
            faq.Add(extraFaq);
        }

        if (dependents > 0)
        {
            faq.Add(new SalaryBandFaqItem(
                $"Como {dependents} dependente(s) alteram o IRRF em {grossMoney}?",
                $"Cada dependente reduz a base do IRRF em {Money.From(BrTaxTables2026.DependentDeduction)} em {BrTaxTables2026.Year}. " +
                $"Neste cenário, o IRRF estimado é {irrfMoney}. Compare com a versão " +
                $"<a href=\"{SalaryBandCatalog.SlugPath(gross)}\">sem dependentes</a>."));
        }

        return new SalaryBandPageContent(gross, dependents, title, description, intro, editorial, context, tips, faq);
    }

    private static string BuildContextParagraph(int gross, decimal minWageRatio, IrrfStatus irrf, NetSalaryBreakdown breakdown, int dependents)
    {
        var minWageText = minWageRatio switch
        {
            < 1.05m => "próximo ao salário mínimo nacional",
            < 2m => "acima do salário mínimo, típico de cargos de entrada",
            < 4m => "na faixa intermediária do mercado formal",
            < 8m => "compatível com posições de maior responsabilidade",
            _ => "em patamar elevado, com maior impacto de IRRF nas faixas superiores"
        };

        var taxableBase = gross - breakdown.Inss;
        return
            $"<p>Um bruto de {Money.From(gross)} equivale a cerca de {minWageRatio:0.#} salários mínimos de {BrTaxTables2026.Year} " +
            $"({Money.From(BrTaxTables2026.MinimumWage)}), perfil {minWageText}. " +
            $"A base de IRRF após INSS fica em {Money.From(taxableBase)} ({ProgrammaticDependentsCatalog.SeoPhrase(dependents)}); {irrf.Summary}</p>";
    }

    private static string BuildTipsParagraph(int gross, int dependents)
    {
        var nearby = SalaryBandCatalog.GetAll()
            .Where(b => b != gross)
            .OrderBy(b => Math.Abs(b - gross))
            .Take(3)
            .Select(b => $"<a href=\"{SalaryBandCatalog.SlugPath(b, dependents)}\">{SalaryBandCatalog.FormatCurrency(b)}</a>")
            .ToList();

        var nearbyText = nearby.Count > 0
            ? $"Compare também: {string.Join(", ", nearby)}."
            : string.Empty;

        var variantLinks = ProgrammaticDependentsCatalog.IndexedDependentCounts
            .Where(count => count != dependents)
            .Select(count =>
                $"<a href=\"{SalaryBandCatalog.SlugPath(gross, count)}\">{ProgrammaticDependentsCatalog.BreadcrumbLabel(count).ToLowerInvariant()}</a>")
            .ToList();
        var variantText = variantLinks.Count > 0
            ? $" Veja {SalaryBandCatalog.FormatCurrency(gross)} com {string.Join(" ou ", variantLinks)}."
            : string.Empty;

        return
            $"<p>Valores reais podem variar com vale-transporte, plano de saúde, empréstimo consignado ou dependentes adicionais no IR. " +
            $"{nearbyText}{variantText} Para simular seu caso completo, abra a " +
            $"<a href=\"/calculadoras/salario-liquido?valor={gross}\">calculadora interativa</a>.</p>";
    }

    private static string DescribePrimaryInssBracket(int gross)
    {
        var capped = Math.Min((decimal)gross, BrTaxTables2026.InssCeiling);
        foreach (var bracket in BrTaxTables2026.InssBrackets)
        {
            if (capped >= bracket.From && capped <= bracket.To)
            {
                return $"{bracket.Rate * 100:0.#}% (até {Money.From(bracket.To)})";
            }
        }

        return $"teto de {Money.From(BrTaxTables2026.InssCeiling)}";
    }

    private static IrrfStatus DescribeIrrfStatus(NetSalaryBreakdown breakdown, int dependents)
    {
        var dependentNote = dependents == 0
            ? "sem dependentes"
            : $"com {dependents} dependente(s)";
        var basis = breakdown.Gross - breakdown.Inss;
        if (breakdown.Irrf == 0m)
        {
            if (basis <= 5000m)
            {
                return new IrrfStatus(
                    "neste valor, o IRRF estimado é zero pela redução legal para bases até R$ 5.000.",
                    $"Neste cenário {dependentNote}, o IRRF estimado é R$ 0,00 pela redução da Lei 15.270/2025 para bases tributáveis até R$ 5.000.");
            }

            return new IrrfStatus(
                "o IRRF estimado é zero para esta base de cálculo.",
                $"O IRRF estimado é R$ 0,00 para a base considerada ({dependentNote}).");
        }

        return new IrrfStatus(
            $"o IRRF estimado é {Money.From(breakdown.Irrf)}.",
            $"O IRRF estimado é {Money.From(breakdown.Irrf)}, calculado sobre a base após INSS ({dependentNote}).");
    }

    private sealed record IrrfStatus(string Summary, string FaqAnswer);
}
