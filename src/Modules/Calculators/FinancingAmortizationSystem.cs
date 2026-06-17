namespace MeuValorLiquido.Modules.Calculators;

public enum FinancingAmortizationSystem
{
    [Display(Name = "Price (parcela fixa)")]
    Price,

    [Display(Name = "SAC (parcelas decrescentes)")]
    Sac,

    [Display(Name = "Comparar Price x SAC")]
    Compare
}
