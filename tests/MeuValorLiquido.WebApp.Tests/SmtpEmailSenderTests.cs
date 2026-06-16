namespace MeuValorLiquido.WebApp.Tests;
public class SmtpEmailSenderTests
{
    [Theory]
    [InlineData("usuario@exemplo.com", "u***@exemplo.com")]
    [InlineData("", "[vazio]")]
    [InlineData("invalido", "***")]
    public void MaskEmail_Should_Not_Expose_Full_Address(string input, string expected)
    {
        SmtpEmailSender.MaskEmail(input).Should().Be(expected);
    }
}
