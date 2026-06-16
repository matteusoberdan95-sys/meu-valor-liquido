var builder = WebApplication.CreateBuilder(args);

var brazilianCulture = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = brazilianCulture;
CultureInfo.DefaultThreadCurrentUICulture = brazilianCulture;

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console();
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var useInMemoryDatabase = builder.Configuration.GetValue<bool>("Database:UseInMemory")
    || builder.Environment.IsEnvironment("Testing");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (useInMemoryDatabase)
    {
        options.UseInMemoryDatabase("meu-valor-liquido");
    }
    else
    {
        options.UseNpgsql(connectionString);
    }
});

builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>("database");

builder.Services.Configure<MailOptions>(builder.Configuration.GetSection("Mail"));
builder.Services.AddResponseCompression();

builder.Services.AddRazorPages();
builder.Services.AddProblemDetails();
builder.Services.AddAntiforgery();
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("form-policy", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            context.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));
});

builder.Services.AddSingleton<CalculatorShareLinkBuilder>();
builder.Services.AddSingleton<CalculatorResultPdfGenerator>();
builder.Services.AddCalculatorsModule();
builder.Services.AddScoped<ICalculatorCatalogService, EfCalculatorCatalogService>();
builder.Services.AddScoped<IContentService, EfContentService>();
builder.Services.AddSingleton<IAdSlotProvider, PlaceholderAdSlotProvider>();
builder.Services.AddScoped<INewsletterService, EfNewsletterService>();
builder.Services.AddScoped<IContactService, EfContactService>();
builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>();
builder.Services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());

var app = builder.Build();

QuestPDF.Settings.License = LicenseType.Community;

await app.InitializeDatabaseAsync();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseSecurityHeaders();

app.UseRouting();
app.UseRateLimiter();

app.UseAuthorization();

app.MapHealthChecks("/health");
CalculatorPdfEndpoints.Map(app);
app.MapGet("/calculadora-salario-bruto", () => Results.Redirect("/calculadoras/salario-bruto-necessario", permanent: true));
app.MapGet("/quanto-preciso-ganhar-para-receber-liquido", () => Results.Redirect("/calculadoras/salario-bruto-necessario", permanent: false));
app.MapGet("/proposta-salarial", () => Results.Redirect("/calculadoras/proposta-salarial", permanent: false));
app.MapGet("/comparar-proposta-salarial", () => Results.Redirect("/calculadoras/proposta-salarial", permanent: false));
app.MapGet("/clt-vs-pj", () => Results.Redirect("/clt-pj", permanent: false));
app.MapGet("/sitemap.xml", async (AppDbContext db) =>
{
    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
    var baseUrl = builder.Configuration["Site:BaseUrl"] ?? "https://meuvalorliquido.com.br";

    var urls = new List<XElement>
    {
        CreateUrl(ns, $"{baseUrl}/"),
        CreateUrl(ns, $"{baseUrl}/calculadoras"),
        CreateUrl(ns, $"{baseUrl}/sobre"),
        CreateUrl(ns, $"{baseUrl}/contato"),
        CreateUrl(ns, $"{baseUrl}/blog"),
        CreateUrl(ns, $"{baseUrl}/newsletter"),
        CreateUrl(ns, $"{baseUrl}/mapa-do-site"),
        CreateUrl(ns, $"{baseUrl}/salario-liquido"),
        CreateUrl(ns, $"{baseUrl}/clt-pj"),
        CreateUrl(ns, $"{baseUrl}/politica-de-privacidade"),
        CreateUrl(ns, $"{baseUrl}/termos-de-uso"),
        CreateUrl(ns, $"{baseUrl}/aviso-legal")
    };

    var calculators = await db.CalculatorCatalog.AsNoTracking().Where(x => x.IsActive).ToListAsync();
    urls.AddRange(calculators.Select(c => CreateUrl(ns, $"{baseUrl}/calculadoras/{c.Slug}")));
    urls.AddRange(SalaryBandCatalog.GetAll().Select(b => CreateUrl(ns, $"{baseUrl}{SalaryBandCatalog.SlugPath(b)}")));
    urls.AddRange(CltPjBandCatalog.GetAll().Select(b => CreateUrl(ns, $"{baseUrl}{CltPjBandCatalog.SlugPath(b)}")));

    var posts = await db.BlogPosts.AsNoTracking().Where(x => x.IsPublished).ToListAsync();
    urls.AddRange(posts.Select(p => CreateUrl(ns, $"{baseUrl}/blog/{p.Slug}")));

    var document = new XDocument(new XElement(ns + "urlset", urls));
    return Results.Content(document.ToString(), "application/xml");
});

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();

static XElement CreateUrl(XNamespace ns, string location) =>
    new(ns + "url", new XElement(ns + "loc", location));

public partial class Program;

internal static class SecurityHeadersExtensions
{
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            context.Response.Headers.TryAdd("X-Content-Type-Options", "nosniff");
            context.Response.Headers.TryAdd("X-Frame-Options", "DENY");
            context.Response.Headers.TryAdd("Referrer-Policy", "strict-origin-when-cross-origin");
            context.Response.Headers.TryAdd("Content-Security-Policy", "default-src 'self'; style-src 'self' 'unsafe-inline' https://fonts.googleapis.com; font-src 'self' https://fonts.gstatic.com; script-src 'self'; img-src 'self' data:");
            await next();
        });
    }
}
