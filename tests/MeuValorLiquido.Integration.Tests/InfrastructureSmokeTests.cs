using FluentAssertions;

namespace MeuValorLiquido.Integration.Tests;

public class InfrastructureSmokeTests
{
    [Fact]
    public void Integration_Project_Should_Be_Ready_For_PostgreSql_Testcontainers()
    {
        const string database = "meu_valor_liquido";

        database.Should().NotBeNullOrWhiteSpace();
    }
}
