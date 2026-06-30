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

    public PayslipDiagnosisView? Diagnosis { get; private set; }

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
        Diagnosis = PayslipDiagnosisView.From(Result);
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

public sealed record PayslipDiagnosisView(
    string Tone,
    string Icon,
    string Title,
    string Summary,
    IReadOnlyList<string> RhChecklist)
{
    public static PayslipDiagnosisView From(PayslipValidationResult result)
    {
        var mismatches = result.Checks
            .Where(check => check.Status == PayslipValidationStatus.Mismatch)
            .ToList();

        if (mismatches.Count == 0)
        {
            return new PayslipDiagnosisView(
                "ok",
                "verified",
                "Seu holerite parece correto",
                "INSS, IRRF e líquido informado estão dentro da tolerância da simulação.",
                [
                    "Guarde o holerite do mês junto com esta conferência.",
                    "Confira se os descontos recorrentes aparecem com os mesmos nomes nos próximos meses.",
                    "Use a calculadora de salário líquido quando houver aumento, férias ou mudança de benefícios."
                ]);
        }

        var maxDifference = mismatches.Max(check => Math.Abs(check.Difference));
        var hasRelevantDifference = maxDifference > 10m;

        return new PayslipDiagnosisView(
            hasRelevantDifference ? "danger" : "warn",
            hasRelevantDifference ? "priority_high" : "report",
            hasRelevantDifference ? "Diferença relevante no holerite" : "Pequena diferença para conferir",
            hasRelevantDifference
                ? "Há pelo menos uma linha com diferença acima de R$ 10,00. Revise os dados informados e leve os pontos ao RH se o valor estiver igual ao holerite."
                : "A diferença passou da tolerância de R$ 1,00, mas ainda parece pequena. Pode ser arredondamento, rubrica não informada ou base de cálculo diferente.",
            BuildChecklist(mismatches));
    }

    private static IReadOnlyList<string> BuildChecklist(IReadOnlyList<PayslipValidationCheck> mismatches)
    {
        var checklist = new List<string>
        {
            "Confirme se o salário bruto usado aqui é o mesmo do holerite.",
            "Veja se há desconto de plano, sindicato, consignado, pensão ou adiantamento não informado."
        };

        if (mismatches.Any(check => check.Key == "inss"))
        {
            checklist.Add("Peça ao RH a memória de cálculo do INSS progressivo.");
        }

        if (mismatches.Any(check => check.Key == "irrf"))
        {
            checklist.Add("Confira dependentes, base de IRRF após INSS e deduções cadastradas.");
        }

        if (mismatches.Any(check => check.Key == "net"))
        {
            checklist.Add("Compare o líquido com todos os proventos e descontos do mês, não só INSS e IRRF.");
        }

        checklist.Add("Se a diferença continuar, envie os valores ao RH e peça esclarecimento por escrito.");
        return checklist;
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
