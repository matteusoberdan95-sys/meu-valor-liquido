namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record WeeklyNewsletterBlock(
    string Label,
    string ContentHtml,
    string? LinkUrl = null,
    string? LinkLabel = null);

public static class WeeklyNewsletterTemplateCatalog
{
    public const string CadenceLabel = "Curadoria semanal — toda terça-feira";

    public static IReadOnlyList<WeeklyNewsletterBlock> GetSampleIssue() =>
    [
        new(
            "Assunto sugerido",
            "Seu líquido em 2026: INSS, IRRF e o que mudou esta semana"),

        new(
            "Abertura",
            """
            <p>Olá! Esta é a <strong>curadoria semanal</strong> do Meu Valor Líquido — resumo educativo sobre salário, impostos e decisões de trabalho, sem spam.</p>
            """),

        new(
            "Calculadora em foco",
            """
            <p>Simule quanto sobra no bolso com as tabelas de INSS e IRRF de 2026 — informe bruto, dependentes e descontos opcionais.</p>
            """,
            "/calculadoras/salario-liquido",
            "Abrir calculadora"),

        new(
            "Leitura da semana",
            """
            <p>Confira como validar holerite antes de falar com o RH e evitar surpresas no líquido.</p>
            """,
            "/blog/como-conferir-holerite",
            "Ler artigo"),

        new(
            "Dica rápida",
            """
            <p>Negocie propostas pelo <strong>líquido real</strong>, não só pelo percentual no bruto — um aumento de 10% no bruto pode significar menos no bolso por causa do IRRF progressivo.</p>
            """,
            "/calculadoras/proposta-salarial",
            "Comparar proposta"),

        new(
            "Encerramento",
            """
            <p>Estimativas educativas. Confirme valores com holerite, contrato ou profissional habilitado.</p>
            <p><a href="/newsletter">Gerenciar inscrição</a> · <a href="/politica-de-privacidade">Privacidade</a></p>
            """)
    ];
}
