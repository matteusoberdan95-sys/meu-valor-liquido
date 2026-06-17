namespace MeuValorLiquido.WebApp.Infrastructure;
/// <summary>Valores brutos indexáveis para páginas programáticas de salário líquido.</summary>
public static class SalaryBandCatalog
{
    public const int MinGross = 1621;
    public const int MaxGross = 20000;

    private static readonly int[] Bands =
    [
        1621, 1800, 2000, 2500, 3000, 3500, 4000, 4500, 5000,
        5500, 6000, 7000, 8000, 9000, 10000, 12000, 15000, 20000
    ];

    public static IReadOnlyList<int> GetAll() => Bands;

    public static bool IsValid(int gross) => Bands.Contains(gross);

    public static int ResolveNearestBand(decimal gross)
    {
        var rounded = (int)decimal.Round(gross, 0, MidpointRounding.AwayFromZero);
        if (IsValid(rounded))
        {
            return rounded;
        }

        return Bands
            .OrderBy(band => Math.Abs(band - rounded))
            .First();
    }

    public static string FormatCurrency(int amount) =>
        amount.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));

    public static string SlugPath(int gross) => $"/salario-liquido/{gross}";
}
