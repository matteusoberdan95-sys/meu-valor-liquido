namespace MeuValorLiquido.WebApp.Infrastructure;

public static class EmbedCodeBuilder
{
    public static string BuildIframeHtml(string baseUrl, EmbedWidgetDefinition widget)
    {
        var src = $"{baseUrl.TrimEnd('/')}{EmbedWidgetCatalog.WidgetPath(widget.Slug)}";
        return $"""
            <iframe
              src="{src}"
              width="100%"
              height="{widget.RecommendedHeight}"
              style="border:0;border-radius:12px;max-width:100%;"
              title="{widget.Name} — Meu Valor Líquido"
              loading="lazy"
              referrerpolicy="strict-origin-when-cross-origin"></iframe>
            <p style="font-size:12px;margin:8px 0 0;color:#64748b;">
              <a href="{baseUrl.TrimEnd('/')}/calculadoras/{widget.Slug}" target="_blank" rel="noopener noreferrer">Calculadora por Meu Valor Líquido</a>
            </p>
            """;
    }
}
