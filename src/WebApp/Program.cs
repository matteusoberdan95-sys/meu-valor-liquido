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

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownIPNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services.Configure<MailOptions>(builder.Configuration.GetSection("Mail"));
builder.Services.Configure<MetricsOptions>(builder.Configuration.GetSection(MetricsOptions.SectionName));
builder.Services.Configure<AdsOptions>(builder.Configuration.GetSection(AdsOptions.SectionName));
builder.Services.AddMemoryCache();
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});
builder.Services.AddOutputCache(options =>
{
    options.AddPolicy("sitemap", policy => policy.Expire(PerformanceCacheDurations.Sitemap));
});

builder.Services.AddSingleton<SitemapXmlCache>();
builder.Services.AddRazorPages();
builder.Services.AddProblemDetails();
if (builder.Environment.IsEnvironment("Testing"))
{
    var dataProtectionKeys = new DirectoryInfo(
        Path.Combine(AppContext.BaseDirectory, "data-protection-keys"));
    builder.Services.AddDataProtection()
        .PersistKeysToFileSystem(dataProtectionKeys);
}

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
    options.AddPolicy("metrics-policy", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            context.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 120,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));
});

builder.Services.AddSingleton<CalculatorShareLinkBuilder>();
builder.Services.AddSingleton<CalculatorJourneyLinkBuilder>();
builder.Services.AddSingleton<CalculatorResultPdfGenerator>();
builder.Services.AddCalculatorsModule();
builder.Services.AddScoped<IBlogHeroImageService, BlogHeroImageService>();
builder.Services.AddScoped<EfCalculatorCatalogService>();
builder.Services.AddScoped<ICalculatorCatalogService>(sp =>
    new CachedCalculatorCatalogService(
        sp.GetRequiredService<EfCalculatorCatalogService>(),
        sp.GetRequiredService<IMemoryCache>()));
builder.Services.AddScoped<EfContentService>();
builder.Services.AddScoped<IContentService>(sp =>
    new CachedContentService(
        sp.GetRequiredService<EfContentService>(),
        sp.GetRequiredService<IMemoryCache>()));
builder.Services.AddSingleton<IAdSlotProvider, ConfigurableAdSlotProvider>();
builder.Services.AddScoped<INewsletterService, EfNewsletterService>();
builder.Services.AddScoped<IContactService, EfContactService>();
builder.Services.AddScoped<IProductMetricsService, EfProductMetricsService>();
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

app.UseForwardedHeaders();
app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseStaticAssetCacheHeaders();
app.UseSecurityHeaders();

app.UseRouting();
app.UseStatusCodePagesWithReExecute("/NotFound", "?statusCode={0}");
app.UseRateLimiter();
app.UseOutputCache();

app.UseAuthorization();

app.MapHealthChecks("/health");
CalculatorPdfEndpoints.Map(app);
ProductMetricsEndpoints.Map(app);
app.MapGet("/calculadora-salario-bruto", () => Results.Redirect("/calculadoras/salario-bruto-necessario", permanent: true));
app.MapGet("/quanto-preciso-ganhar-para-receber-liquido", () => Results.Redirect("/calculadoras/salario-bruto-necessario", permanent: false));
app.MapGet("/proposta-salarial", () => Results.Redirect("/calculadoras/proposta-salarial", permanent: false));
app.MapGet("/comparar-proposta-salarial", () => Results.Redirect("/calculadoras/proposta-salarial", permanent: false));
app.MapGet("/clt-vs-pj", () => Results.Redirect("/clt-pj", permanent: false));
app.MapGet("/painel", () => Results.Redirect("/meu-painel", permanent: false));
app.MapGet("/incorporar", () => Results.Redirect("/widget", permanent: false));
app.MapGet("/widget/{slug}", (string slug) =>
    EmbedWidgetCatalog.IsEmbeddable(slug)
        ? Results.Redirect($"/calculadoras/{slug}?embed=1", permanent: false)
        : Results.NotFound());

app.MapMethods("/sitemap.xml", [HttpMethods.Get, HttpMethods.Head], (HttpRequest request, HttpResponse response, SitemapXmlCache cache) =>
{
    if (!cache.IsReady)
    {
        return Results.StatusCode(StatusCodes.Status503ServiceUnavailable);
    }

    var xml = cache.Xml;
    const string contentType = "application/xml; charset=utf-8";

    if (HttpMethods.IsHead(request.Method))
    {
        response.ContentType = contentType;
        response.ContentLength = Encoding.UTF8.GetByteCount(xml);
        return Results.Empty;
    }

    response.Headers.CacheControl = $"public,max-age={(int)PerformanceCacheDurations.Sitemap.TotalSeconds}";
    return Results.Content(xml, contentType);
});

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();

public partial class Program;

internal static class SecurityHeadersExtensions
{
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            var configuration = context.RequestServices.GetRequiredService<IConfiguration>();
            var adsEnabled = configuration.GetSection(AdsOptions.SectionName).Get<AdsOptions>()?.IsActive == true;

            context.Response.OnStarting(() =>
            {
                context.Response.Headers.TryAdd("X-Content-Type-Options", "nosniff");
                context.Response.Headers.TryAdd("Referrer-Policy", "strict-origin-when-cross-origin");

                if (EmbedFramePolicy.AllowsEmbedding(context.Request.Path, context.Request.Query))
                {
                    context.Response.Headers.Remove("X-Frame-Options");
                    context.Response.Headers["Content-Security-Policy"] = EmbedFramePolicy.BuildEmbedContentSecurityPolicy();
                }
                else
                {
                    context.Response.Headers.TryAdd("X-Frame-Options", "DENY");
                    context.Response.Headers.TryAdd(
                        "Content-Security-Policy",
                        AdsContentSecurityPolicy.Build(adsEnabled));
                }

                return Task.CompletedTask;
            });

            await next();
        });
    }
}
