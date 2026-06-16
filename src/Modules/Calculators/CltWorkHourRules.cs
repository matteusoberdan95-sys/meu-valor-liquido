namespace MeuValorLiquido.Modules.Calculators;
public enum SalaryConversionBasis
{
    [Display(Name = "Salário mensal")]
    Monthly,

    [Display(Name = "Salário diário")]
    Daily,

    [Display(Name = "Salário por hora")]
    Hourly
}

public enum OvertimeShiftType
{
    [Display(Name = "Dia útil (mín. 50%)")]
    Weekday,

    [Display(Name = "Domingo ou feriado (mín. 100%)")]
    SundayOrHoliday,

    [Display(Name = "Noturna em dia útil (+20% noturno)")]
    NightWeekday
}

public static class CltWorkHourRules
{
    public static decimal GetMonthlyHourDivisor(int weeklyWorkHours) => weeklyWorkHours switch
    {
        40 => 200m,
        36 => 180m,
        30 => 150m,
        44 or 0 => 220m,
        _ => weeklyWorkHours * 5m
    };

    public static (decimal Monthly, decimal Daily, decimal Hourly) ConvertSalary(
        decimal amount,
        SalaryConversionBasis basis,
        decimal monthlyHourDivisor)
    {
        var divisor = monthlyHourDivisor > 0m ? monthlyHourDivisor : 220m;
        return basis switch
        {
            SalaryConversionBasis.Monthly => (amount, amount / 30m, amount / divisor),
            SalaryConversionBasis.Daily => (amount * 30m, amount, amount * 30m / divisor),
            SalaryConversionBasis.Hourly => (amount * divisor, amount * divisor / 30m, amount),
            _ => (amount, amount / 30m, amount / divisor)
        };
    }

    public static decimal ResolveOvertimeAdditionalPercent(decimal conventionRate, OvertimeShiftType shiftType)
    {
        var rate = conventionRate <= 0m ? 50m : conventionRate;
        return shiftType switch
        {
            OvertimeShiftType.SundayOrHoliday => Math.Max(rate, 100m),
            OvertimeShiftType.NightWeekday => rate + 20m,
            _ => rate
        };
    }
}
