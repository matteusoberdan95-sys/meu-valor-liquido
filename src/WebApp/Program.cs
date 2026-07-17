var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsEnvironment("Testing"))
{
    builder.WebHost.UseStaticWebAssets();
}

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
builder.Services.AddScoped<CalculatorJourneyLinkBuilder>();
builder.Services.AddScoped<ThematicHubLoader>();
builder.Services.AddSingleton<CalculatorPdfInputSummaryBuilder>();
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
app.UseCanonicalUrlRedirects();
app.UseSeoResponseHeaders();

app.UseRouting();
app.UseMiddleware<ProductMetricsHttpErrorMiddleware>();
app.UseStatusCodePagesWithReExecute("/NotFound", "?statusCode={0}");
app.UseRateLimiter();
app.UseOutputCache();

app.UseAuthorization();

app.MapHealthChecks("/health");
CalculatorPdfEndpoints.Map(app);
ProductMetricsEndpoints.Map(app);
app.MapGet("/calculadora-salario-bruto", () => Results.Redirect("/calculadoras/salario-bruto-necessario", permanent: true));
app.MapGet("/quanto-preciso-ganhar-para-receber-liquido", () => Results.Redirect("/calculadoras/salario-bruto-necessario", permanent: true));
app.MapGet("/proposta-salarial", () => Results.Redirect("/calculadoras/proposta-salarial", permanent: true));
app.MapGet("/comparar-proposta-salarial", () => Results.Redirect("/calculadoras/proposta-salarial", permanent: true));
app.MapGet("/clt-vs-pj", () => Results.Redirect("/clt-pj", permanent: true));
app.MapGet("/painel", () => Results.Redirect("/meu-painel", permanent: true));
app.MapGet("/incorporar", () => Results.Redirect("/widget", permanent: true));
app.MapGet("/duvidas/o-que-e-irrf", () => Results.Redirect("/duvidas/irrf-quem-paga-e-como-calcular", permanent: true));
app.MapGet("/widget/{slug}", (string slug) =>
    EmbedWidgetCatalog.IsEmbeddable(slug)
        ? Results.Redirect($"/calculadoras/{slug}?embed=1", permanent: true)
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

internal static class SeoHttpExtensions
{
    public static IApplicationBuilder UseCanonicalUrlRedirects(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            var request = context.Request;
            var path = request.Path.Value;
            var canRedirect = HttpMethods.IsGet(request.Method) || HttpMethods.IsHead(request.Method);
            var isInternalErrorPath = path is not null
                && (path.Equals("/Error", StringComparison.OrdinalIgnoreCase)
                    || path.Equals("/NotFound", StringComparison.OrdinalIgnoreCase));
            var isCanonicalSeoFile = path is not null
                && (path.TrimEnd('/').Equals("/sitemap.xml", StringComparison.OrdinalIgnoreCase)
                    || path.TrimEnd('/').Equals("/robots.txt", StringComparison.OrdinalIgnoreCase));

            if (canRedirect
                && !isInternalErrorPath
                && !string.IsNullOrEmpty(path)
                && path != "/"
                && (!Path.HasExtension(path) || isCanonicalSeoFile))
            {
                var normalizedPath = path.TrimEnd('/').ToLowerInvariant();
                if (!path.Equals(normalizedPath, StringComparison.Ordinal))
                {
                    context.Response.StatusCode = StatusCodes.Status301MovedPermanently;
                    context.Response.Headers.Location =
                        $"{request.PathBase}{new PathString(normalizedPath).ToUriComponent()}{request.QueryString}";
                    return;
                }
            }

            await next();
        });
    }

    public static IApplicationBuilder UseSeoResponseHeaders(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            context.Response.OnStarting(() =>
            {
                if (SeoRoutePolicyCatalog.RequiresNoIndexHeader(context.Request.Path))
                {
                    var isHtmlPage = SeoRoutePolicyCatalog.IsNoIndexPage(context.Request.Path);
                    context.Response.Headers["X-Robots-Tag"] = isHtmlPage ? "noindex" : "noindex, nofollow";
                }

                return Task.CompletedTask;
            });

            await next();
        });
    }
}

internal static class SecurityHeadersExtensions
{
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            var configuration = context.RequestServices.GetRequiredService<IConfiguration>();
            var adsOptions = configuration.GetSection(AdsOptions.SectionName).Get<AdsOptions>();
            var adsScriptAllowed = adsOptions is { IsActive: true } or { ShouldRenderVerificationScript: true };

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
                        AdsContentSecurityPolicy.Build(adsScriptAllowed));
                }

                return Task.CompletedTask;
            });

            await next();
        });
    }
}
