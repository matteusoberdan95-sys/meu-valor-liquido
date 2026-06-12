using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MeuValorLiquido.WebApp.Tests;

public class RescisaoCalculatorPageTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public RescisaoCalculatorPageTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Rescisao_Page_Should_Show_Termination_Reason_Field()
    {
        var html = await client.GetStringAsync("/calculadoras/rescisao-clt");

        html.Should().Contain("Tipo de desligamento");
        html.Should().Contain("valora-choice-group");
        html.Should().Contain("Demitido sem justa causa");
        html.Should().Contain("Pediu demiss");
        html.Should().Contain("type=\"radio\"");
    }
}
