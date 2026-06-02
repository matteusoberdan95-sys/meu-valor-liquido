namespace MeuValorLiquido.Shared.Formatting
{
    public static class CurrencyFormatter
    {
        private static readonly CultureInfo BrazilianCulture = CultureInfo.GetCultureInfo("pt-BR");

        public static string Format(decimal amount)
        {
            return amount.ToString("C", BrazilianCulture);
        }
    }
}

namespace MeuValorLiquido.Shared.Helpers
{
    public static partial class SlugHelper
    {
        public static string Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            var normalized = value.Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder();

            foreach (var character in normalized)
            {
                var category = CharUnicodeInfo.GetUnicodeCategory(character);
                if (category != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(character);
                }
            }

            var slug = builder
                .ToString()
                .Normalize(NormalizationForm.FormC)
                .ToLowerInvariant();

            slug = NonAlphaNumericRegex().Replace(slug, "-");
            return DuplicateDashRegex().Replace(slug, "-").Trim('-');
        }

        [GeneratedRegex("[^a-z0-9]+")]
        private static partial Regex NonAlphaNumericRegex();

        [GeneratedRegex("-+")]
        private static partial Regex DuplicateDashRegex();
    }
}

namespace MeuValorLiquido.Shared.Seo
{
    public sealed record SeoMetadata(
        string Title,
        string Description,
        string? CanonicalPath = null,
        string Robots = "index,follow");
}

namespace MeuValorLiquido.Shared.Pagination
{
    public sealed record PageRequest(int PageNumber = 1, int PageSize = 20)
    {
        public int Skip => (Math.Max(PageNumber, 1) - 1) * Math.Max(PageSize, 1);
    }

    public sealed record PageResult<T>(
        IReadOnlyList<T> Items,
        int PageNumber,
        int PageSize,
        int TotalCount);
}

namespace MeuValorLiquido.Shared.Validation
{
    public sealed record ValidationIssue(string Field, string Message);

    public sealed record ValidationSummary(IReadOnlyList<ValidationIssue> Issues)
    {
        public bool IsValid => Issues.Count == 0;

        public static ValidationSummary Empty { get; } = new(Array.Empty<ValidationIssue>());
    }
}

namespace MeuValorLiquido.Shared.Web
{
    public static class SecurityHeaderNames
    {
        public const string ContentTypeOptions = "X-Content-Type-Options";
        public const string FrameOptions = "X-Frame-Options";
        public const string ReferrerPolicy = "Referrer-Policy";
        public const string ContentSecurityPolicy = "Content-Security-Policy";
    }
}
