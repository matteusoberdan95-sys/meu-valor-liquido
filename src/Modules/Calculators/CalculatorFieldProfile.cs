namespace MeuValorLiquido.Modules.Calculators;

public sealed record CalculatorFieldProfile(
    bool ShowAmount = true,
    bool ShowSecondaryAmount = false,
    bool ShowMonths = false,
    bool ShowRate = false,
    bool ShowHours = false,
    bool ShowDependents = false,
    bool ShowTransportDiscount = false,
    string AmountLabel = "Valor principal",
    string SecondaryAmountLabel = "Valor secundário",
    string MonthsLabel = "Meses",
    string RateLabel = "Taxa (%)",
    string HoursLabel = "Horas",
    string DependentsLabel = "Dependentes",
    string TransportDiscountLabel = "Descontos extras");

public interface ICalculatorFieldProfileProvider
{
    CalculatorFieldProfile GetProfile(string slug);
}

public sealed class CalculatorFieldProfileProvider : ICalculatorFieldProfileProvider
{
    private static readonly Dictionary<string, CalculatorFieldProfile> Profiles = new(StringComparer.OrdinalIgnoreCase)
    {
        ["salario-liquido"] = new(AmountLabel: "Salário bruto", ShowDependents: true, ShowTransportDiscount: true, TransportDiscountLabel: "Vale-transporte e outros descontos"),
        ["ferias"] = new(AmountLabel: "Salário base", ShowDependents: true),
        ["decimo-terceiro"] = new(AmountLabel: "Salário base", ShowMonths: true, MonthsLabel: "Meses trabalhados no ano", ShowDependents: true),
        ["rescisao-clt"] = new(AmountLabel: "Último salário", ShowMonths: true, MonthsLabel: "Meses na empresa", ShowSecondaryAmount: true, SecondaryAmountLabel: "Dias trabalhados no mês"),
        ["hora-extra"] = new(AmountLabel: "Valor da hora", ShowHours: true, ShowRate: true, RateLabel: "Adicional (%)"),
        ["inss"] = new(AmountLabel: "Salário de contribuição"),
        ["irrf"] = new(AmountLabel: "Base de cálculo", ShowDependents: true),
        ["pj-vs-clt"] = new(AmountLabel: "Salário bruto CLT", ShowSecondaryAmount: true, SecondaryAmountLabel: "Faturamento PJ", ShowDependents: true),
        ["juros-compostos"] = new(AmountLabel: "Valor inicial", ShowMonths: true, ShowRate: true, RateLabel: "Taxa mensal (%)"),
        ["financiamento"] = new(AmountLabel: "Valor financiado", ShowMonths: true, MonthsLabel: "Prazo (meses)", ShowRate: true, RateLabel: "Taxa mensal (%)")
    };

    public CalculatorFieldProfile GetProfile(string slug)
    {
        return Profiles.TryGetValue(slug, out var profile)
            ? profile
            : new CalculatorFieldProfile();
    }
}
