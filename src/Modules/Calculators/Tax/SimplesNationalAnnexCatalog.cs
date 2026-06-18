namespace MeuValorLiquido.Modules.Calculators.Tax;

public enum SimplesAnnex
{
    [Display(Name = "Anexo I — Comércio")]
    AnnexOne,

    [Display(Name = "Anexo II — Indústria")]
    AnnexTwo,

    [Display(Name = "Anexo III — Serviços (locação, academias etc.)")]
    AnnexThree,

    [Display(Name = "Anexo IV — Serviços com folha (fator R)")]
    AnnexFour,

    [Display(Name = "Anexo V — Serviços intelectuais")]
    AnnexFive
}

/// <summary>Alíquotas nominais de referência na 1ª faixa do Simples Nacional (educativo; faixa real varia com RBT12).</summary>
public static class SimplesNationalAnnexCatalog
{
    public static decimal GetSuggestedRatePercent(SimplesAnnex annex) => annex switch
    {
        SimplesAnnex.AnnexOne => 4m,
        SimplesAnnex.AnnexTwo => 4.5m,
        SimplesAnnex.AnnexThree => 6m,
        SimplesAnnex.AnnexFour => 4.5m,
        SimplesAnnex.AnnexFive => 15.5m,
        _ => 6m
    };

    public static string GetDescription(SimplesAnnex annex) => annex switch
    {
        SimplesAnnex.AnnexOne => "Comércio em geral, varejo e revenda.",
        SimplesAnnex.AnnexTwo => "Indústria e transformação de produtos.",
        SimplesAnnex.AnnexThree => "Serviços como locação, academias e agências.",
        SimplesAnnex.AnnexFour => "Serviços com folha relevante (fator R ≥ 28%).",
        SimplesAnnex.AnnexFive => "Serviços intelectuais, TI, engenharia e consultoria.",
        _ => "Referência educativa do Simples Nacional."
    };
}
