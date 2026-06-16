namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record CltPjPageContent(
    int CltGross,
    string Title,
    string Description,
    string IntroHtml,
    string ContextHtml,
    string TipsHtml,
    IReadOnlyList<SalaryBandFaqItem> FaqItems);

public static class CltPjContentBuilder
{
    public static CltPjPageContent Build(int cltGross, CltPjComparisonBreakdown comparison)
    {
        var clt = comparison.Clt;
        var pj = comparison.Pj;
        var grossMoney = Money.From(cltGross);
        var cltNet = Money.From(clt.Net);
        var pjNet = Money.From(pj.Net);
        var equivalent = Money.From(comparison.EquivalentPjRevenue);
        var diff = comparison.NetDifference;

        var title = $"{grossMoney} CLT equivale a quanto PJ em {BrTaxTables2026.Year}?";
        var description =
            $"Salário CLT de {grossMoney} (líquido {cltNet}) equivale a cerca de {equivalent} de faturamento PJ. " +
            $"Comparativo educativo com Simples {comparison.SimplesRatePercent:0.#}% e pró-labore de {comparison.ProLaboreSharePercent:0.#}%.";

        var intro =
            $"<p>Para receber <strong>{cltNet}</strong> líquido como CLT com salário bruto de <strong>{grossMoney}</strong>, " +
            $"a estimativa de faturamento PJ equivalente é <strong>{equivalent}</strong> por mês. " +
            $"No cenário informado (Simples {comparison.SimplesRatePercent:0.#}%, pró-labore {comparison.ProLaboreSharePercent:0.#}%), " +
            $"o líquido pessoal na PJ seria <strong>{pjNet}</strong>.</p>";

        var context =
            $"<p>O CLT desconta INSS ({Money.From(clt.Inss)}) e IRRF ({Money.From(clt.Irrf)}) sobre o salário. " +
            $"Na PJ, o Simples ({Money.From(pj.SimplesTax)}) incide sobre o faturamento; o que entra no bolso vem do pró-labore " +
            $"({Money.From(pj.ProLabore)}) após INSS ({Money.From(pj.Inss)}) e IRRF ({Money.From(pj.Irrf)}). " +
            $"A diferença de líquido neste cenário é {Money.From(diff)} (PJ − CLT).</p>";

        var tips =
            "<p>Benefícios CLT como férias, 13º, FGTS e seguro-desemprego não entram nesta comparação mensal. " +
            "Ajuste dependentes, descontos CLT, alíquota do Simples e despesas na " +
            "<a href=\"/calculadoras/pj-vs-clt\">calculadora PJ vs CLT</a> " +
            "ou compare com <a href=\"/calculadoras/simulador-mei\">MEI</a> se for o seu caso.</p>";

        var faq = new List<SalaryBandFaqItem>
        {
            new(
                $"Quanto faturar como PJ para equivaler a {grossMoney} CLT?",
                $"Neste cenário simplificado, cerca de {equivalent} de faturamento mensal, com pró-labore de {comparison.ProLaboreSharePercent:0.#}% e Simples de {comparison.SimplesRatePercent:0.#}%."),
            new(
                $"Qual o líquido CLT de {grossMoney}?",
                $"Aproximadamente {cltNet}, após INSS e IRRF estimados (sem dependentes nem outros descontos, salvo os informados na calculadora completa)."),
            new(
                "O PJ sempre ganha mais que o CLT?",
                "Não. Depende do faturamento, impostos, despesas e do valor dos benefícios trabalhistas. Use o comparativo como ponto de partida, não como decisão automática."),
            new(
                "Como personalizar a simulação?",
                "Abra a <a href=\"/calculadoras/pj-vs-clt\">calculadora PJ vs CLT</a> com faturamento, Simples, dependentes e despesas reais.")
        };

        return new CltPjPageContent(cltGross, title, description, intro, context, tips, faq);
    }
}
