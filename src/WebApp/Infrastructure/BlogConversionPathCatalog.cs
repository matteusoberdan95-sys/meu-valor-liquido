namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record BlogConversionPath(
    string CalculatorSlug,
    string CalculatorTitle,
    string CalculatorHref,
    string Title,
    string Description,
    string PrimaryCtaLabel,
    string HubHref,
    string HubLabel,
    string FaqHref,
    string FaqLabel);

public static class BlogConversionPathCatalog
{
    public static BlogConversionPath? Build(BlogPost post, CalculatorDefinition? calculator)
    {
        if (string.IsNullOrWhiteSpace(post.RelatedCalculatorSlug))
        {
            return null;
        }

        var calculatorSlug = post.RelatedCalculatorSlug;
        var calculatorTitle = calculator?.Name ?? "calculadora relacionada";
        var template = Resolve(calculatorSlug, calculatorTitle);

        return new BlogConversionPath(
            calculatorSlug,
            calculatorTitle,
            $"/calculadoras/{calculatorSlug}",
            template.Title,
            template.Description,
            template.PrimaryCtaLabel,
            template.HubHref,
            template.HubLabel,
            template.FaqHref,
            template.FaqLabel);
    }

    private static BlogConversionTemplate Resolve(string slug, string calculatorTitle) =>
        slug.ToLowerInvariant() switch
        {
            "salario-liquido" => new(
                "Veja o impacto no seu holerite",
                "Use os dados do artigo para simular bruto, INSS, IRRF, benefícios e descontos em uma única tela.",
                "Simular salário líquido",
                "/negociar-salario",
                "Guia para negociar salário",
                "/duvidas/como-calcular-salario-liquido",
                "Como calcular salário líquido"),

            "proposta-salarial" => new(
                "Compare a proposta pelo líquido",
                "Veja se o aumento bruto realmente melhora o que entra no banco depois dos descontos obrigatórios.",
                "Comparar proposta",
                "/negociar-salario",
                "Guia de negociação",
                "/duvidas/proposta-salarial-como-negociar",
                "Como negociar proposta"),

            "rescisao-clt" => new(
                "Simule antes de assinar a rescisão",
                "Informe motivo, datas e salário para comparar sua estimativa com o TRCT entregue pela empresa.",
                "Simular rescisão",
                "/desligamento",
                "Jornada de desligamento",
                "/duvidas/rescisao-pedido-demissao-o-que-recebo",
                "O que recebo na rescisão"),

            "fgts" => new(
                "Confira FGTS, multa e saldo",
                "Estime depósitos, multa e valores informativos antes de comparar com o extrato oficial.",
                "Calcular FGTS",
                "/desligamento",
                "Guia de desligamento",
                "/duvidas/multa-fgts-40-porcento",
                "Multa de 40% do FGTS"),

            "seguro-desemprego" => new(
                "Estime parcelas do seguro-desemprego",
                "Use média salarial e tempo de trabalho para entender valor provável, quantidade de parcelas e elegibilidade.",
                "Simular seguro-desemprego",
                "/desligamento",
                "Jornada de desligamento",
                "/duvidas/seguro-desemprego-quando-tem-direito",
                "Quem tem direito"),

            "ferias" => new(
                "Planeje férias pelo líquido",
                "Compare férias integrais, proporcionais e abono para entender o valor que realmente cai na conta.",
                "Calcular férias",
                "/desligamento",
                "Guia de verbas CLT",
                "/duvidas/ferias-proporcionais-como-funciona",
                "Férias proporcionais"),

            "decimo-terceiro" => new(
                "Confira parcelas e descontos do 13º",
                "Simule 1ª e 2ª parcela, adiantamentos e descontos para evitar surpresa no fim do ano.",
                "Calcular 13º",
                "/desligamento",
                "Guia de verbas CLT",
                "/duvidas/decimo-terceiro-quem-tem-direito",
                "Quem tem direito ao 13º"),

            "pj-vs-clt" => new(
                "Compare CLT e PJ com custos reais",
                "Transforme salário, nota, impostos e benefícios em uma comparação mais honesta do que sobra no mês.",
                "Comparar CLT vs PJ",
                "/virar-pj",
                "Guia para virar PJ",
                "/duvidas/pj-ou-clt-qual-compensa",
                "PJ ou CLT: qual compensa"),

            "simulador-mei" => new(
                "Confira se o MEI fecha a conta",
                "Simule DAS, faturamento e limites antes de comparar renda PJ com salário CLT.",
                "Simular MEI",
                "/virar-pj",
                "Guia para virar PJ",
                "/duvidas/mei-pode-trabalhar-como-clt",
                "MEI pode ser CLT"),

            "inss" => new(
                "Veja o desconto progressivo do INSS",
                "Informe o salário bruto e confira faixas, teto e desconto estimado com tabelas atualizadas.",
                "Calcular INSS",
                "/como-calculamos",
                "Metodologia 2026",
                "/duvidas/quanto-desconta-inss-2026",
                "Quanto desconta de INSS"),

            "irrf" => new(
                "Entenda o IRRF no salário",
                "Simule base de cálculo, deduções e imposto retido antes de conferir o holerite.",
                "Calcular IRRF",
                "/como-calculamos",
                "Metodologia 2026",
                "/duvidas/irrf-quem-paga-e-como-calcular",
                "Quem paga IRRF"),

            _ => new(
                "Transforme a leitura em simulação",
                $"Abra {calculatorTitle} para conferir o cenário com seus próprios dados e comparar com holerite, proposta ou contrato.",
                $"Abrir {calculatorTitle}",
                "/calculadoras",
                "Ver calculadoras",
                "/duvidas",
                "Central de dúvidas")
        };

    private sealed record BlogConversionTemplate(
        string Title,
        string Description,
        string PrimaryCtaLabel,
        string HubHref,
        string HubLabel,
        string FaqHref,
        string FaqLabel);
}
