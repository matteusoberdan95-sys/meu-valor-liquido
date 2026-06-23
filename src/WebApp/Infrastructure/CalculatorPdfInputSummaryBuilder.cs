namespace MeuValorLiquido.WebApp.Infrastructure;

using System.ComponentModel.DataAnnotations;
using System.Reflection;

public sealed class CalculatorPdfInputSummaryBuilder(ICalculatorFieldProfileProvider fieldProfileProvider)
{
    public IReadOnlyList<PdfInputField> Build(string slug, CalculatorInput input)
    {
        var profile = fieldProfileProvider.GetProfile(slug);
        var fields = new List<PdfInputField>();

        if (profile.ShowAmount && input.Amount > 0m)
        {
            fields.Add(new PdfInputField(profile.AmountLabel, FormatMoney(input.Amount)));
        }

        if (profile.ShowSecondaryAmount && input.SecondaryAmount > 0m)
        {
            var value = profile.SecondaryAmountIsDays
                ? $"{input.SecondaryAmount:0.#} dias"
                : FormatMoney(input.SecondaryAmount);
            fields.Add(new PdfInputField(profile.SecondaryAmountLabel, value));
        }

        if (profile.ShowDependents && input.Dependents > 0)
        {
            fields.Add(new PdfInputField(profile.DependentsLabel, input.Dependents.ToString(CultureInfo.InvariantCulture)));
        }

        if (profile.ShowTransportDiscount && input.TransportDiscount > 0m)
        {
            fields.Add(new PdfInputField(profile.TransportDiscountLabel, FormatMoney(input.TransportDiscount)));
        }

        if (profile.ShowMealVoucherDiscount && input.MealVoucherDiscount > 0m)
        {
            fields.Add(new PdfInputField(profile.MealVoucherDiscountLabel, FormatMoney(input.MealVoucherDiscount)));
        }

        if (profile.ShowHealthPlanDiscount && input.HealthPlanDiscount > 0m)
        {
            fields.Add(new PdfInputField(profile.HealthPlanDiscountLabel, FormatMoney(input.HealthPlanDiscount)));
        }

        if (profile.ShowAlimonyAmount && input.AlimonyAmount > 0m)
        {
            fields.Add(new PdfInputField(profile.AlimonyAmountLabel, FormatMoney(input.AlimonyAmount)));
        }

        if (profile.ShowAlimonyPercent && input.AlimonyPercent > 0m)
        {
            fields.Add(new PdfInputField(profile.AlimonyPercentLabel, $"{input.AlimonyPercent:0.#}%"));
        }

        if (profile.ShowOtherDiscounts && input.OtherDiscounts > 0m)
        {
            fields.Add(new PdfInputField(profile.OtherDiscountsLabel, FormatMoney(input.OtherDiscounts)));
        }

        if (profile.ShowTerminationReason)
        {
            fields.Add(new PdfInputField(profile.TerminationReasonLabel, EnumDisplay(input.TerminationReason)));
        }

        if (profile.ShowNoticePeriodOptions)
        {
            fields.Add(new PdfInputField(profile.NoticePeriodOptionLabel, EnumDisplay(input.NoticePeriod)));
        }
        else if (profile.ShowNoticePeriod)
        {
            fields.Add(new PdfInputField(
                profile.NoticePeriodLabel,
                input.CompletedNoticePeriod ? "Sim" : "Não"));
        }

        if (profile.ShowAdmissionDate && input.AdmissionDate is not null)
        {
            fields.Add(new PdfInputField(profile.AdmissionDateLabel, FormatDate(input.AdmissionDate.Value)));
        }

        if (profile.ShowTerminationDate && input.TerminationDate is not null)
        {
            fields.Add(new PdfInputField(profile.TerminationDateLabel, FormatDate(input.TerminationDate.Value)));
        }

        if (profile.ShowFgtsBalance && input.FgtsBalance > 0m)
        {
            fields.Add(new PdfInputField(profile.FgtsBalanceLabel, FormatMoney(input.FgtsBalance)));
        }

        if (profile.ShowThirteenthAdvance && input.ThirteenthAdvancePaid > 0m)
        {
            fields.Add(new PdfInputField(profile.ThirteenthAdvancePaidLabel, FormatMoney(input.ThirteenthAdvancePaid)));
        }

        if (profile.ShowSalaryAverageSupplement && input.SalaryAverageSupplement > 0m)
        {
            fields.Add(new PdfInputField(profile.SalaryAverageSupplementLabel, FormatMoney(input.SalaryAverageSupplement)));
        }

        if (profile.ShowMonths && input.Months > 0)
        {
            fields.Add(new PdfInputField(profile.MonthsLabel, input.Months.ToString(CultureInfo.InvariantCulture)));
        }

        if (profile.ShowRate && input.Rate > 0m)
        {
            fields.Add(new PdfInputField(profile.RateLabel, $"{input.Rate:0.##}%"));
        }

        if (profile.ShowHours && input.Hours > 0m)
        {
            fields.Add(new PdfInputField(profile.HoursLabel, $"{input.Hours:0.##} h"));
        }

        if (profile.ShowSimplesAnnex)
        {
            fields.Add(new PdfInputField(profile.SimplesAnnexLabel, EnumDisplay(input.SimplesAnnex)));
        }

        if (profile.ShowProLaborePercent && input.ProLaborePercent > 0m)
        {
            fields.Add(new PdfInputField(profile.ProLaborePercentLabel, $"{input.ProLaborePercent:0.#}%"));
        }

        if (profile.ShowMeiAnnualAccumulated && input.MeiAnnualAccumulated > 0m)
        {
            fields.Add(new PdfInputField(profile.MeiAnnualAccumulatedLabel, FormatMoney(input.MeiAnnualAccumulated)));
        }

        if (profile.ShowMeiActivity)
        {
            fields.Add(new PdfInputField(profile.MeiActivityLabel, EnumDisplay(input.MeiActivity)));
        }

        if (profile.ShowFinancingAmortization)
        {
            fields.Add(new PdfInputField(profile.FinancingAmortizationLabel, EnumDisplay(input.FinancingAmortization)));
        }

        if (profile.ShowVacationOptions && input.SellVacationAllowance)
        {
            fields.Add(new PdfInputField(profile.SellVacationAllowanceLabel, "Sim"));
        }

        if (profile.ShowVacationOptions && input.DoubleVacationPayment)
        {
            fields.Add(new PdfInputField(profile.DoubleVacationPaymentLabel, "Sim"));
        }

        if (profile.ShowHasUnpaidVacation && input.HasUnpaidVacation)
        {
            fields.Add(new PdfInputField(profile.HasUnpaidVacationLabel, "Sim"));
        }

        return fields;
    }

    private static string FormatMoney(decimal amount) => Money.From(amount).ToString();

    private static string FormatDate(DateOnly date) =>
        date.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"));

    private static string EnumDisplay<T>(T value) where T : struct, Enum
    {
        var field = typeof(T).GetField(value.ToString()!);
        var display = field?.GetCustomAttribute<DisplayAttribute>();
        return display?.Name ?? value.ToString()!;
    }
}
