namespace MeuValorLiquido.WebApp.Infrastructure;

public static class AdsContentSecurityPolicy
{
    private const string BasePolicy =
        "default-src 'self'; " +
        "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com; " +
        "font-src 'self' https://fonts.gstatic.com; " +
        "script-src 'self'; " +
        "img-src 'self' data:";

    public static string Build(bool adsEnabled)
    {
        if (!adsEnabled)
        {
            return BasePolicy;
        }

        return
            "default-src 'self'; " +
            "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com; " +
            "font-src 'self' https://fonts.gstatic.com; " +
            "script-src 'self' https://pagead2.googlesyndication.com https://www.googletagservices.com https://www.google.com https://adservice.google.com; " +
            "img-src 'self' data: https: blob:; " +
            "frame-src https://googleads.g.doubleclick.net https://tpc.googlesyndication.com; " +
            "connect-src 'self' https://pagead2.googlesyndication.com https://googleads.g.doubleclick.net";
    }
}
