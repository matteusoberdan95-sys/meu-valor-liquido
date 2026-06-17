namespace MeuValorLiquido.WebApp.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class NotFoundModel : PageModel
{
    [FromQuery(Name = "statusCode")]
    public int? ReceivedStatusCode { get; set; }

    public int DisplayStatusCode => ReceivedStatusCode is > 0 ? ReceivedStatusCode.Value : 404;

    public bool IsNotFound => DisplayStatusCode == 404;

    public string BadgeText => IsNotFound
        ? "Erro 404 • Página não encontrada"
        : $"Erro {DisplayStatusCode}";

    public string Headline => IsNotFound
        ? "Ops, o seu cálculo saiu da rota."
        : "Não foi possível carregar esta página.";

    public string Lead => IsNotFound
        ? "Parece que o valor líquido que você procura não está aqui. A página pode ter sido removida ou o link está quebrado."
        : "O servidor retornou um código de erro. Tente novamente ou use os atalhos abaixo.";

    public void OnGet()
    {
        Response.StatusCode = DisplayStatusCode;
    }
}
