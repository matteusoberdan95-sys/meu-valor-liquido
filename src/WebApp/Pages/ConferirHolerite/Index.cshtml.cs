namespace MeuValorLiquido.WebApp.Pages.ConferirHolerite;

public class IndexModel : PageModel
{
    private readonly IPayslipValidationService payslipValidationService;

    public IndexModel(IPayslipValidationService payslipValidationService)
    {
        this.payslipValidationService = payslipValidationService;
    }

    [BindProperty]
    public PayslipValidationFormInput Input { get; set; } = new();

    public PayslipValidationResult? Result { get; private set; }

    public void OnGet()
    {
        ApplySeo();
    }

    public IActionResult OnPost()
    {
        ApplySeo();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Input.GrossSalary <= 0m)
        {
            ModelState.AddModelError($"{nameof(Input)}.{nameof(PayslipValidationFormInput.GrossSalary)}", "Informe um salário bruto maior que zero.");
            return Page();
        }

        Result = payslipValidationService.Validate(Input.ToValidationInput());
        return Page();
    }

    private void ApplySeo()
    {
        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Conferir holerite: valide INSS e IRRF 2026",
                "Cole os valores do seu holerite e compare INSS, IRRF e líquido com as tabelas de 2026. Ferramenta educativa para conferência antes de falar com o RH.",
                "/conferir-holerite"));
    }
}

public sealed class PayslipValidationFormInput
{
    [Display(Name = "Salário bruto")]
    public decimal GrossSalary { get; set; }

    [Display(Name = "Dependentes")]
    public int Dependents { get; set; }

    [Display(Name = "INSS descontado (holerite)")]
    public decimal ReportedInss { get; set; }

    [Display(Name = "IRRF descontado (holerite)")]
    public decimal ReportedIrrf { get; set; }

    [Display(Name = "Salário líquido (holerite)")]
    public decimal? ReportedNet { get; set; }

    [Display(Name = "Vale-transporte")]
    public decimal TransportDiscount { get; set; }

    [Display(Name = "Vale-refeição/alimentação")]
    public decimal MealVoucherDiscount { get; set; }

    [Display(Name = "Plano de saúde")]
    public decimal HealthPlanDiscount { get; set; }

    [Display(Name = "Pensão alimentícia (valor)")]
    public decimal AlimonyAmount { get; set; }

    [Display(Name = "Pensão alimentícia (%)")]
    public decimal AlimonyPercent { get; set; }

    [Display(Name = "Outros descontos")]
    public decimal OtherDiscounts { get; set; }

    public bool IncludeNetCheck => ReportedNet is > 0m;

    public PayslipValidationInput ToValidationInput() =>
        new(
            GrossSalary,
            Dependents,
            ReportedInss,
            ReportedIrrf,
            TransportDiscount,
            MealVoucherDiscount,
            HealthPlanDiscount,
            AlimonyAmount,
            AlimonyPercent,
            OtherDiscounts,
            IncludeNetCheck ? ReportedNet : null);
}
