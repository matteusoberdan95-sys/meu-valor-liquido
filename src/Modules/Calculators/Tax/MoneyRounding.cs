namespace MeuValorLiquido.Modules.Calculators.Tax;

/// <summary>
/// Política explícita de arredondamento monetário (duas casas, AwayFromZero).
/// Valores monetários usam <see cref="decimal"/> — nunca <c>double</c>.
/// </summary>
public static class MoneyRounding
{
    public const int Scale = 2;

    public const MidpointRounding Mode = MidpointRounding.AwayFromZero;

    public static decimal Round(decimal amount) =>
        decimal.Round(amount, Scale, Mode);
}
