namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed class SitemapXmlCache
{
    private string _xml = string.Empty;

    public string Xml => _xml;

    public bool IsReady => _xml.Length > 0;

    public async Task RefreshAsync(IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        _xml = await SitemapGenerator.BuildXmlAsync(db, configuration);
    }
}
