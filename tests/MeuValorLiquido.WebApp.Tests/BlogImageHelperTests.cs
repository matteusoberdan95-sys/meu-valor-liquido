namespace MeuValorLiquido.WebApp.Tests;

public class BlogImageHelperTests
{
    [Theory]
    [InlineData("o-que-e-salario-liquido", "/images/blog/o-que-e-salario-liquido.webp")]
    [InlineData("como-calcular-ferias", "/images/blog/como-calcular-ferias.webp")]
    public void GetPublicPath_Should_Follow_Slug_Convention(string slug, string expected)
    {
        BlogImageHelper.GetPublicPath(slug).Should().Be(expected);
    }

    [Fact]
    public void GetAltText_Should_Include_Title_And_Brand()
    {
        var alt = BlogImageHelper.GetAltText("O que é salário líquido?", "Trabalhista");

        alt.Should().Contain("O que é salário líquido?");
        alt.Should().Contain("Meu Valor Líquido");
        alt.Should().Contain("Trabalhista");
    }

    [Fact]
    public void Exists_Should_Be_False_When_File_Missing()
    {
        var env = new TestWebHostEnvironment(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N")));
        Directory.CreateDirectory(Path.Combine(env.WebRootPath, BlogImageHelper.RelativeFolder));

        BlogImageHelper.Exists(env, "slug-inexistente").Should().BeFalse();
    }

    [Fact]
    public void Exists_Should_Be_True_When_Webp_Present()
    {
        var env = new TestWebHostEnvironment(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N")));
        var folder = Path.Combine(env.WebRootPath, BlogImageHelper.RelativeFolder);
        Directory.CreateDirectory(folder);
        var slug = "o-que-e-salario-liquido";
        File.WriteAllBytes(Path.Combine(folder, $"{slug}{BlogImageHelper.FileExtension}"), [0x00]);

        BlogImageHelper.Exists(env, slug).Should().BeTrue();
    }

    private sealed class TestWebHostEnvironment : IWebHostEnvironment
    {
        public TestWebHostEnvironment(string webRoot)
        {
            Directory.CreateDirectory(webRoot);
            WebRootPath = webRoot;
            ContentRootPath = webRoot;
            var provider = new PhysicalFileProvider(webRoot);
            WebRootFileProvider = provider;
            ContentRootFileProvider = provider;
        }

        public string ApplicationName { get; set; } = "Test";
        public IFileProvider WebRootFileProvider { get; set; }
        public string WebRootPath { get; set; }
        public string EnvironmentName { get; set; } = "Testing";
        public string ContentRootPath { get; set; }
        public IFileProvider ContentRootFileProvider { get; set; }
    }
}
