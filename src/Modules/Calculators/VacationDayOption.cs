using System.ComponentModel.DataAnnotations;

namespace MeuValorLiquido.Modules.Calculators;

public enum VacationDayOption
{
    [Display(Name = "Automático (proporcional aos meses)")]
    Automatic = 0,

    [Display(Name = "30 dias (férias integrais)")]
    Full30 = 30,

    [Display(Name = "20 dias (acordo ou redução)")]
    Reduced20 = 20
}
