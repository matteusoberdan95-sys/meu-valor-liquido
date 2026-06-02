using System.Threading.RateLimiting;
using MeuValorLiquido.Core.Abstractions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console();
});

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

builder.Services.AddCalculatorsModule();
builder.Services.AddSingleton<IContentService, InMemoryContentService>();
builder.Services.AddSingleton<IAdSlotProvider, PlaceholderAdSlotProvider>();
builder.Services.AddSingleton<INewsletterService, InMemoryNewsletterService>();
builder.Services.AddSingleton<IEmailSender, LocalEmailSender>();
builder.Services.AddScoped<IContactService, ContactService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSecurityHeaders();

app.UseRouting();
app.UseRateLimiter();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();

public partial class Program;

internal sealed class LocalEmailSender : IEmailSender
{
    private readonly ILogger<LocalEmailSender> logger;

    public LocalEmailSender(ILogger<LocalEmailSender> logger)
    {
        this.logger = logger;
    }

    public Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Mock email sent to {Recipient} with subject {Subject}", message.To, message.Subject);
        return Task.CompletedTask;
    }
}

internal static class SecurityHeadersExtensions
{
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            context.Response.Headers.TryAdd("X-Content-Type-Options", "nosniff");
            context.Response.Headers.TryAdd("X-Frame-Options", "DENY");
            context.Response.Headers.TryAdd("Referrer-Policy", "strict-origin-when-cross-origin");
            context.Response.Headers.TryAdd("Content-Security-Policy", "default-src 'self'; style-src 'self' 'unsafe-inline'; script-src 'self'; img-src 'self' data:");
            await next();
        });
    }
}
