using FluentAssertions;
using MeuValorLiquido.Modules.Calculators;
using MeuValorLiquido.Modules.Calculators.Tax;
using MeuValorLiquido.WebApp.Infrastructure;

namespace MeuValorLiquido.WebApp.Tests;

public class SalaryBandTests
{
    [Fact]
    public void Catalog_Should_Contain_Popular_Bands()
    {
        var bands = SalaryBandCatalog.GetAll();
        bands.Should().Contain(3000);
        bands.Should().Contain(5000);
        bands.Should().HaveCountGreaterThan(10);
    }

    [Theory]
    [InlineData(3000, true)]
    [InlineData(3333, false)]
    public void IsValid_Should_Match_Catalog(int gross, bool expected)
    {
        SalaryBandCatalog.IsValid(gross).Should().Be(expected);
    }

    [Fact]
    public void ContentBuilder_3000_Should_Mention_Net_And_Inss()
    {
        var breakdown = new NetSalaryCalculator(new InssCalculator(), new IrrfCalculator())
            .Calculate(3000m, 0, 0m);
        var content = SalaryBandContentBuilder.Build(3000, breakdown);

        content.Title.Should().Contain("3.000");
        content.Description.Should().Contain("Líquido");
        content.IntroHtml.Should().Contain("INSS");
        content.FaqItems.Should().HaveCountGreaterThanOrEqualTo(4);
    }
}
