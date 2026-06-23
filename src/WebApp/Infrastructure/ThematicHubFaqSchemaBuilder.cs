namespace MeuValorLiquido.WebApp.Infrastructure;

public static partial class ThematicHubFaqSchemaBuilder
{
    public static IReadOnlyList<FaqItem> Build(IReadOnlyList<PopularQuestionDefinition> faqs)
    {
        var items = new List<FaqItem>();
        foreach (var faq in faqs)
        {
            items.Add(new FaqItem(faq.Title, StripHtml(faq.AnswerHtml)));
            items.AddRange(faq.FaqItems.Select(f => new FaqItem(f.Question, StripHtml(f.AnswerHtml))));
        }

        return items;
    }

    private static string StripHtml(string html) =>
        HtmlTagRegex().Replace(html, " ").Replace("&nbsp;", " ", StringComparison.Ordinal).Trim();

    [GeneratedRegex("<[^>]+>")]
    private static partial Regex HtmlTagRegex();
}
