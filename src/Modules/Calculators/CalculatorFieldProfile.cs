namespace MeuValorLiquido.Modules.Calculators;

public sealed record CalculatorFieldProfile(
    bool ShowAmount = true,
    bool ShowSecondaryAmount = false,
    bool ShowMonths = false,
    bool ShowRate = false,
    bool ShowHours = false,
    bool ShowDependents = false,
    bool ShowTransportDiscount = false,
    bool ShowOtherDiscounts = false,
    bool ShowTerminationReason = false,
    bool ShowNoticePeriod = false,
    bool ShowHasUnpaidVacation = false,
    bool ShowVacationTakenInPeriod = false,
    bool ShowMonthsSinceLastVacation = false,
    bool ShowCompleteYears = false,
    bool ShowFgtsBalance = false,
    bool ShowThirteenthMonths = false,
    bool ShowTerminationMonth = false,
    bool ShowAdmissionMonth = false,
    bool ShowAdmissionInPriorYear = false,
    bool ShowWeeklyWorkHours = false,
    bool ShowOvertimeShiftType = false,
    bool SecondaryAmountIsDays = false,
    bool AmountIsCurrency = true,
    string AmountLabel = "Valor principal",
    string SecondaryAmountLabel = "Valor secundário",
    string MonthsLabel = "Meses",
    string RateLabel = "Taxa (%)",
    string HoursLabel = "Horas",
    string DependentsLabel = "Dependentes",
    string TransportDiscountLabel = "Descontos extras",
    string OtherDiscountsLabel = "Outros descontos",
    string TerminationReasonLabel = "Tipo de desligamento",
    string NoticePeriodLabel = "Cumpriu aviso prévio de 30 dias?",
    string HasUnpaidVacationLabel = "Possui férias vencidas (não gozadas)?",
    string VacationTakenInPeriodLabel = "Já gozou férias neste período aquisitivo?",
    string MonthsSinceLastVacationLabel = "Meses desde a última férias (proporcional parcial)",
    string CompleteYearsLabel = "Anos completos na empresa",
    string FgtsBalanceLabel = "Saldo FGTS (opcional — para multa)",
    string ThirteenthMonthsLabel = "Meses trabalhados no ano da saída (13º)",
    string TerminationMonthLabel = "Mês da rescisão",
    string AdmissionMonthLabel = "Mês de admissão",
    string AdmissionInPriorYearLabel = "Admissão no ano anterior à rescisão",
    string WeeklyWorkHoursLabel = "Jornada semanal (horas)",
    string OvertimeShiftTypeLabel = "Tipo de hora extra",
    bool ShowSalaryBasis = false,
    bool ShowMeiActivity = false,
    bool ShowFgtsTerminationReason = false,
    string SalaryBasisLabel = "Tipo de valor informado",
    string MeiActivityLabel = "Atividade MEI",
    string FgtsTerminationReasonLabel = "Tipo de desligamento (para multa)");

public interface ICalculatorFieldProfileProvider
{
    CalculatorFieldProfile GetProfile(string slug);
}

public sealed class CalculatorFieldProfileProvider : ICalculatorFieldProfileProvider
{
    private static readonly Dictionary<string, CalculatorFieldProfile> Profiles = new(StringComparer.OrdinalIgnoreCase)
    {
        ["salario-liquido"] = new(AmountLabel: "Salário bruto", ShowDependents: true, ShowTransportDiscount: true, TransportDiscountLabel: "Vale-transporte e outros descontos"),
        ["salario-bruto-necessario"] = new(
            AmountLabel: "Salário líquido desejado",
            ShowDependents: true,
            ShowTransportDiscount: true,
            TransportDiscountLabel: "Desconto vale-transporte",
            ShowSecondaryAmount: true,
            SecondaryAmountLabel: "Desconto vale-refeição/alimentação",
            ShowOtherDiscounts: true,
            OtherDiscountsLabel: "Outros descontos"),
        ["ferias"] = new(AmountLabel: "Salário base", ShowDependents: true),
        ["decimo-terceiro"] = new(AmountLabel: "Salário base", ShowMonths: true, MonthsLabel: "Meses trabalhados no ano", ShowDependents: true),
        ["rescisao-clt"] = new(
            AmountLabel: "Último salário",
            ShowMonths: true,
            MonthsLabel: "Tempo total na empresa (meses)",
            ShowAdmissionMonth: true,
            ShowAdmissionInPriorYear: true,
            ShowThirteenthMonths: true,
            ShowTerminationMonth: true,
            ShowSecondaryAmount: true,
            SecondaryAmountLabel: "Dias trabalhados no mês",
            SecondaryAmountIsDays: true,
            ShowTerminationReason: true,
            TerminationReasonLabel: "Tipo de desligamento",
            ShowNoticePeriod: true,
            NoticePeriodLabel: "Cumpriu aviso prévio de 30 dias?",
            ShowDependents: true,
            ShowHasUnpaidVacation: true,
            ShowVacationTakenInPeriod: true,
            ShowMonthsSinceLastVacation: true,
            ShowCompleteYears: true,
            ShowFgtsBalance: true,
            ShowTransportDiscount: true,
            TransportDiscountLabel: "Outros descontos (empréstimo, VT etc.)"),
        ["hora-extra"] = new(
            AmountLabel: "Valor da hora",
            ShowSecondaryAmount: true,
            SecondaryAmountLabel: "Salário mensal bruto (opcional)",
            ShowHours: true,
            ShowRate: true,
            RateLabel: "Adicional CCT (%) — ex.: 50, 70, 100",
            ShowWeeklyWorkHours: true,
            ShowOvertimeShiftType: true),
        ["inss"] = new(AmountLabel: "Salário de contribuição"),
        ["irrf"] = new(AmountLabel: "Base de cálculo (após INSS)", ShowDependents: true),
        ["pj-vs-clt"] = new(AmountLabel: "Salário bruto CLT", ShowSecondaryAmount: true, SecondaryAmountLabel: "Faturamento PJ", ShowDependents: true),
        ["juros-compostos"] = new(AmountLabel: "Valor inicial", ShowMonths: true, ShowRate: true, RateLabel: "Taxa mensal (%)"),
        ["financiamento"] = new(AmountLabel: "Valor financiado", ShowMonths: true, MonthsLabel: "Prazo (meses)", ShowRate: true, RateLabel: "Taxa mensal (%)"),
        ["fgts"] = new(
            AmountLabel: "Salário bruto",
            ShowMonths: true,
            MonthsLabel: "Meses trabalhados",
            ShowFgtsBalance: true,
            FgtsBalanceLabel: "Saldo FGTS atual (opcional)",
            ShowFgtsTerminationReason: true),
        ["simulador-mei"] = new(AmountLabel: "Faturamento mensal estimado", ShowMeiActivity: true),
        ["custo-funcionario"] = new(
            AmountLabel: "Salário bruto",
            ShowSecondaryAmount: true,
            SecondaryAmountLabel: "Benefícios mensais (VT, plano etc.)"),
        ["multa-atraso"] = new(
            AmountLabel: "Valor em atraso",
            ShowSecondaryAmount: true,
            SecondaryAmountLabel: "Dias de atraso",
            SecondaryAmountIsDays: true,
            ShowRate: true,
            RateLabel: "Juros ao mês (%)",
            ShowHours: true,
            HoursLabel: "Multa (%)"),
        ["conversor-salario"] = new(
            AmountLabel: "Valor a converter",
            ShowSalaryBasis: true,
            ShowWeeklyWorkHours: true,
            AmountIsCurrency: true)
    };

    public CalculatorFieldProfile GetProfile(string slug)
    {
        return Profiles.TryGetValue(slug, out var profile)
            ? profile
            : new CalculatorFieldProfile();
    }
}
