namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint59WebTests : IClassFixture<WebApplicationFactory<Program>>
{
  private readonly HttpClient client;

  public Sprint59WebTests(WebApplicationFactory<Program> factory)
  {
    client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
  }

  [Fact]
  public async Task PjVsClt_Page_Should_Show_Four_Step_Wizard()
  {
    var html = await client.GetStringAsync("/calculadoras/pj-vs-clt");

    html.Should().Contain("data-pj-step=\"4\"");
    html.Should().Contain("3. Benefícios");
    html.Should().Contain("Input_SimplesAnnex");
    html.Should().Contain("Input_ProLaborePercent");
    html.Should().Contain("/calculadoras/custo-funcionario");
    html.Should().Contain("/calculadoras/simulador-mei");
  }

  [Fact]
  public async Task Mei_Page_Should_Show_Annual_Accumulated_Field()
  {
    var html = await client.GetStringAsync("/calculadoras/simulador-mei");

    html.Should().Contain("Input_MeiAnnualAccumulated");
    html.Should().Contain("acumulado no ano");
  }

  [Fact]
  public async Task PjOuClt_Faq_Should_Link_To_Calculators()
  {
    var html = await client.GetStringAsync("/duvidas/pj-ou-clt-qual-compensa");

    html.Should().Contain("/calculadoras/pj-vs-clt");
    html.Should().Contain("/calculadoras/simulador-mei");
    html.Should().Contain("CLT valem quanto");
  }
}
