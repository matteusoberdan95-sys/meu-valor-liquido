namespace MeuValorLiquido.Modules.Calculators.Tax;

public static class BrTaxTables2026
{
    public const int Year = 2026;
    public const decimal DependentDeduction = 189.59m;
    public const decimal InssCeiling = 8157.41m;

    public static readonly InssBracket[] InssBrackets =
    [
        new(0m, 1518.00m, 0.075m),
        new(1518.01m, 2793.88m, 0.09m),
        new(2793.89m, 4190.83m, 0.12m),
        new(4190.84m, InssCeiling, 0.14m)
    ];

    public static readonly IrrfBracket[] IrrfBrackets =
    [
        new(0m, 2259.20m, 0m, 0m),
        new(2259.21m, 2826.65m, 0.075m, 169.44m),
        new(2826.66m, 3751.05m, 0.15m, 381.44m),
        new(3751.06m, 4664.68m, 0.225m, 662.77m),
        new(4664.69m, decimal.MaxValue, 0.275m, 896.00m)
    ];
}

public readonly record struct InssBracket(decimal From, decimal To, decimal Rate);

public readonly record struct IrrfBracket(decimal From, decimal To, decimal Rate, decimal Deduction);

public interface IInssCalculator
{
    decimal Calculate(decimal grossSalary);
}

public interface IIrrfCalculator
{
    decimal Calculate(decimal taxableBasis, int dependents);
}

public sealed class InssCalculator : IInssCalculator
{
    public decimal Calculate(decimal grossSalary)
    {
        var total = 0m;
        foreach (var bracket in BrTaxTables2026.InssBrackets)
        {
            if (grossSalary <= bracket.From)
            {
                break;
            }

            var taxable = Math.Min(grossSalary, bracket.To) - bracket.From;
            if (taxable > 0)
            {
                total += taxable * bracket.Rate;
            }
        }

        return decimal.Round(total, 2, MidpointRounding.AwayFromZero);
    }
}

public sealed class IrrfCalculator : IIrrfCalculator
{
    public decimal Calculate(decimal taxableBasis, int dependents)
    {
        var basis = Math.Max(0m, taxableBasis - dependents * BrTaxTables2026.DependentDeduction);
        foreach (var bracket in BrTaxTables2026.IrrfBrackets)
        {
            if (basis >= bracket.From && basis <= bracket.To)
            {
                return decimal.Round(Math.Max(0m, basis * bracket.Rate - bracket.Deduction), 2, MidpointRounding.AwayFromZero);
            }
        }

        return 0m;
    }
}
