namespace MeuValorLiquido.Modules.Calculators;

public sealed record FinancingPriceResult(decimal Payment, decimal TotalPaid, decimal TotalInterest);

public sealed record FinancingSacResult(
    decimal FirstPayment,
    decimal LastPayment,
    decimal TotalPaid,
    decimal TotalInterest);

public static class FinancingCalculator
{
    public static FinancingPriceResult CalculatePrice(decimal principal, int months, decimal monthlyRate)
    {
        if (months < 1)
        {
            months = 1;
        }

        if (monthlyRate <= 0m)
        {
            var evenPayment = principal / months;
            return new FinancingPriceResult(evenPayment, principal, 0m);
        }

        var factor = (decimal)Math.Pow((double)(1m + monthlyRate), -months);
        var payment = principal * monthlyRate / (1m - factor);
        var total = payment * months;
        return new FinancingPriceResult(payment, total, total - principal);
    }

    public static FinancingSacResult CalculateSac(decimal principal, int months, decimal monthlyRate)
    {
        if (months < 1)
        {
            months = 1;
        }

        var amortization = principal / months;
        var balance = principal;
        decimal totalPaid = 0m;
        decimal firstPayment = 0m;
        decimal lastPayment = 0m;

        for (var month = 1; month <= months; month++)
        {
            var interest = balance * monthlyRate;
            var payment = amortization + interest;
            totalPaid += payment;

            if (month == 1)
            {
                firstPayment = payment;
            }

            lastPayment = payment;
            balance -= amortization;
        }

        return new FinancingSacResult(firstPayment, lastPayment, totalPaid, totalPaid - principal);
    }
}
