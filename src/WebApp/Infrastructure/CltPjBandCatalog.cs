namespace MeuValorLiquido.WebApp.Infrastructure;

/// <summary>URLs indexáveis CLT x PJ (reutiliza faixas de salário bruto).</summary>
public static class CltPjBandCatalog
{
    public static IReadOnlyList<int> GetAll() => SalaryBandCatalog.GetAll();

    public static bool IsValid(int cltGross) => SalaryBandCatalog.IsValid(cltGross);

    public static string SlugPath(int cltGross) => $"/clt-pj/{cltGross}-clt-equivale-a-quanto-pj";

    public static string FormatCurrency(int amount) => SalaryBandCatalog.FormatCurrency(amount);
}
