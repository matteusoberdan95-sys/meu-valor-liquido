using MeuValorLiquido.Core.Abstractions;
using MeuValorLiquido.WebApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeuValorLiquido.WebApp.Data;

public sealed class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<CalculatorCategoryEntity> CalculatorCategories => Set<CalculatorCategoryEntity>();

    public DbSet<CalculatorCatalogEntity> CalculatorCatalog => Set<CalculatorCatalogEntity>();

    public DbSet<FaqItemEntity> FaqItems => Set<FaqItemEntity>();

    public DbSet<BlogPostEntity> BlogPosts => Set<BlogPostEntity>();

    public DbSet<ContactMessageEntity> ContactMessages => Set<ContactMessageEntity>();

    public DbSet<NewsletterSubscriberEntity> NewsletterSubscribers => Set<NewsletterSubscriberEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalculatorCategoryEntity>(entity =>
        {
            entity.ToTable("calculator_categories");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(80).IsRequired();
        });

        modelBuilder.Entity<CalculatorCatalogEntity>(entity =>
        {
            entity.ToTable("calculator_catalog");
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.Slug).IsUnique();
            entity.Property(x => x.Slug).HasMaxLength(80).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Summary).HasMaxLength(500).IsRequired();
            entity.Property(x => x.SeoTitle).HasMaxLength(160).IsRequired();
            entity.Property(x => x.SeoDescription).HasMaxLength(320).IsRequired();
            entity.HasOne(x => x.Category).WithMany(x => x.Calculators).HasForeignKey(x => x.CategoryId);
        });

        modelBuilder.Entity<FaqItemEntity>(entity =>
        {
            entity.ToTable("faq_items");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.CalculatorSlug).HasMaxLength(80).IsRequired();
            entity.Property(x => x.Question).HasMaxLength(300).IsRequired();
            entity.Property(x => x.Answer).HasMaxLength(2000).IsRequired();
        });

        modelBuilder.Entity<BlogPostEntity>(entity =>
        {
            entity.ToTable("blog_posts");
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.Slug).IsUnique();
            entity.Property(x => x.Slug).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Title).HasMaxLength(160).IsRequired();
            entity.Property(x => x.Category).HasMaxLength(40);
            entity.Property(x => x.RelatedCalculatorSlug).HasMaxLength(80);
        });

        modelBuilder.Entity<ContactMessageEntity>(entity =>
        {
            entity.ToTable("contact_messages");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Email).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(120).IsRequired();
        });

        modelBuilder.Entity<NewsletterSubscriberEntity>(entity =>
        {
            entity.ToTable("newsletter_subscribers");
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.Email).IsUnique();
            entity.Property(x => x.Email).HasMaxLength(200).IsRequired();
        });
    }
}
