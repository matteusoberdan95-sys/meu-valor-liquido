namespace MeuValorLiquido.WebApp.Data.Entities;
public class CalculatorCategoryEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    public ICollection<CalculatorCatalogEntity> Calculators { get; set; } = [];
}

public class CalculatorCatalogEntity
{
    public int Id { get; set; }

    public string Slug { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public CalculatorCategoryEntity Category { get; set; } = null!;

    public string Summary { get; set; } = string.Empty;

    public string SeoTitle { get; set; } = string.Empty;

    public string SeoDescription { get; set; } = string.Empty;

    public string? EducationalContent { get; set; }

    public int SortOrder { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<FaqItemEntity> FaqItems { get; set; } = [];
}

public class FaqItemEntity
{
    public int Id { get; set; }

    public string CalculatorSlug { get; set; } = string.Empty;

    public string Question { get; set; } = string.Empty;

    public string Answer { get; set; } = string.Empty;

    public int SortOrder { get; set; }
}

public class BlogPostEntity
{
    public int Id { get; set; }

    public string Slug { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Summary { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public DateOnly PublishedAt { get; set; }

    public string? Category { get; set; }

    public string? RelatedCalculatorSlug { get; set; }

    public bool IsPublished { get; set; } = true;
}

public class ContactMessageEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; }
}

public class NewsletterSubscriberEntity
{
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public DateTimeOffset SubscribedAt { get; set; }

    public bool IsConfirmed { get; set; }
}

public class AggregatedMetricEntity
{
    public int Id { get; set; }

    public DateOnly MetricDate { get; set; }

    public string EventType { get; set; } = string.Empty;

    public string Dimension { get; set; } = string.Empty;

    public long Count { get; set; }
}
