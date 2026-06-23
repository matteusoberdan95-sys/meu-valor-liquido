namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record RescisaoChecklistItemDefinition(
    string Id,
    string Title,
    string Description,
    string? LinkUrl,
    string? LinkLabel);

public static class RescisaoChecklistCatalog
{
    public const string StorageKey = "mvl-rescisao-checklist-v1";

    private static readonly IReadOnlyList<RescisaoChecklistItemDefinition> All =
    [
        new(
            "simular-rescisao",
            "Simular verbas rescisórias",
            "Estime saldo de salário, aviso prévio, férias, 13º e descontos conforme o motivo do desligamento.",
            "/calculadoras/rescisao-clt",
            "Abrir calculadora de rescisão"),
        new(
            "conferir-trct",
            "Conferir TRCT linha a linha",
            "Compare cada verba do termo com a simulação antes de assinar. Dúvidas comuns sobre saldo e descontos.",
            "/blog/rescisao-clt-vs-trct",
            "Ler guia TRCT vs simulação"),
        new(
            "fgts-multa",
            "Estimar FGTS e multa rescisória",
            "Veja depósitos, saldo e se há multa de 40% ou 20% (acordo 484-A) no seu tipo de saída.",
            "/calculadoras/fgts",
            "Abrir calculadora de FGTS"),
        new(
            "seguro-desemprego",
            "Verificar seguro-desemprego",
            "Confira carência, número de parcelas e valor estimado com a tabela MTE 2026.",
            "/calculadoras/seguro-desemprego",
            "Abrir calculadora do seguro"),
        new(
            "multa-fgts-faq",
            "Entender multa do FGTS",
            "Saiba quando há 40%, quando é 20% e o que muda no saque da conta vinculada.",
            "/duvidas/multa-fgts-40-porcento",
            "Ver FAQ da multa"),
        new(
            "documentos",
            "Organizar documentos do desligamento",
            "Guarde TRCT, carta de demissão, extrato FGTS, comprovantes de pagamento e holerite final.",
            null,
            null),
        new(
            "prazos-pagamento",
            "Conferir prazos de pagamento",
            "Verbas rescisórias costumam ser pagas em até 10 dias após o término. Anote datas e valores recebidos.",
            "/duvidas/rescisao-pedido-demissao-o-que-recebo",
            "Ver o que receber na demissão"),
        new(
            "proximos-passos",
            "Planejar renda nos próximos meses",
            "Some rescisão, FGTS, seguro-desemprego (se houver) e despesas fixas para montar um colchão.",
            "/calculadoras/salario-liquido",
            "Simular novo holerite")
    ];

    public static IReadOnlyList<RescisaoChecklistItemDefinition> GetAll() => All;
}
