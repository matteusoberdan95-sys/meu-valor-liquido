namespace MeuValorLiquido.WebApp.Infrastructure;
public sealed class MailOptions
{
    public string Host { get; set; } = "localhost";

    public int Port { get; set; } = 1025;

    public string FromAddress { get; set; } = "noreply@meuvalorliquido.local";

    public string FromName { get; set; } = "Meu Valor Líquido";
}

public sealed class SmtpEmailSender : IEmailSender
{
    private readonly MailOptions options;
    private readonly ILogger<SmtpEmailSender> logger;

    public SmtpEmailSender(IOptions<MailOptions> options, ILogger<SmtpEmailSender> logger)
    {
        this.options = options.Value;
        this.logger = logger;
    }

    public async Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default)
    {
        var mime = new MimeMessage();
        mime.From.Add(new MailboxAddress(options.FromName, options.FromAddress));
        mime.To.Add(MailboxAddress.Parse(message.To));
        mime.Subject = message.Subject;
        mime.Body = new TextPart("plain") { Text = message.Body };

        try
        {
            using var client = new SmtpClient();
            await client.ConnectAsync(options.Host, options.Port, MailKit.Security.SecureSocketOptions.None, cancellationToken);
            await client.SendAsync(mime, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
            logger.LogInformation("E-mail enviado para {RecipientMasked} via {Host}:{Port}", MaskEmail(message.To), options.Host, options.Port);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Falha ao enviar e-mail para {RecipientMasked}. Verifique se o Mailpit está ativo.", MaskEmail(message.To));
        }
    }

    /// <summary>Mascara e-mail nos logs para evitar PII (LGPD / boas práticas).</summary>
    public static string MaskEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return "[vazio]";
        }

        var at = email.IndexOf('@');
        if (at <= 0)
        {
            return "***";
        }

        return email[0] + "***" + email[at..];
    }
}
