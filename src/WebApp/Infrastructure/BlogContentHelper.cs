namespace MeuValorLiquido.WebApp.Infrastructure;
public static class BlogContentHelper
{
    public static int EstimateReadingMinutes(string htmlContent)
    {
        var text = System.Text.RegularExpressions.Regex.Replace(htmlContent, "<[^>]+>", " ");
        var words = text.Split([' ', '\n', '\r', '\t'], StringSplitOptions.RemoveEmptyEntries).Length;
        return Math.Max(1, (int)Math.Ceiling(words / 200.0));
    }
}
