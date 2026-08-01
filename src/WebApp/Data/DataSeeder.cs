namespace MeuValorLiquido.WebApp.Data;
public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext db, CancellationToken cancellationToken = default)
    {
        if (await db.CalculatorCatalog.AnyAsync(cancellationToken))
        {
            await SeedMissingCalculatorsAsync(db, cancellationToken);
            await SyncEducationalContentAsync(db, cancellationToken);
            await SeedBlogPostsAsync(db, cancellationToken);
            return;
        }

        var categories = new Dictionary<string, CalculatorCategoryEntity>(StringComparer.OrdinalIgnoreCase);
        var sort = 0;
        foreach (var definition in CalculatorSeedData.GetDefinitions())
        {
            if (!categories.ContainsKey(definition.Category))
            {
                categories[definition.Category] = new CalculatorCategoryEntity
                {
                    Name = definition.Category,
                    SortOrder = sort++
                };
            }
        }

        db.CalculatorCategories.AddRange(categories.Values);
        await db.SaveChangesAsync(cancellationToken);

        var calculatorSort = 0;
        foreach (var definition in CalculatorSeedData.GetDefinitions())
        {
            var category = categories[definition.Category];
            var entity = new CalculatorCatalogEntity
            {
                Slug = definition.Slug,
                Name = definition.Name,
                CategoryId = category.Id,
                Summary = definition.Summary,
                SeoTitle = definition.SeoTitle,
                SeoDescription = definition.SeoDescription,
                EducationalContent = GetEducationalContent(definition.Slug),
                SortOrder = calculatorSort++,
                IsActive = true
            };

            db.CalculatorCatalog.Add(entity);
            await db.SaveChangesAsync(cancellationToken);

            var faqSort = 0;
            foreach (var faq in definition.FaqItems)
            {
                var exists = await db.FaqItems.AnyAsync(
                    x => x.CalculatorSlug == definition.Slug && x.Question == faq.Question,
                    cancellationToken);
                if (exists)
                {
                    continue;
                }

                db.FaqItems.Add(new FaqItemEntity
                {
                    CalculatorSlug = definition.Slug,
                    Question = faq.Question,
                    Answer = faq.Answer,
                    SortOrder = faqSort++
                });
            }
        }

        await db.SaveChangesAsync(cancellationToken);
        await SeedBlogPostsAsync(db, cancellationToken);
    }

    private static async Task SeedMissingCalculatorsAsync(AppDbContext db, CancellationToken cancellationToken)
    {
        var existingSlugs = await db.CalculatorCatalog
            .Select(x => x.Slug)
            .ToListAsync(cancellationToken);

        var missing = CalculatorSeedData.GetDefinitions()
            .Where(d => !existingSlugs.Contains(d.Slug, StringComparer.OrdinalIgnoreCase))
            .ToList();

        if (missing.Count == 0)
        {
            return;
        }

        var categories = await db.CalculatorCategories.ToListAsync(cancellationToken);
        var maxSort = await db.CalculatorCatalog.MaxAsync(x => (int?)x.SortOrder, cancellationToken) ?? -1;

        foreach (var definition in missing)
        {
            var category = categories.FirstOrDefault(c =>
                string.Equals(c.Name, definition.Category, StringComparison.OrdinalIgnoreCase));

            if (category is null)
            {
                category = new CalculatorCategoryEntity
                {
                    Name = definition.Category,
                    SortOrder = categories.Count
                };
                db.CalculatorCategories.Add(category);
                await db.SaveChangesAsync(cancellationToken);
                categories.Add(category);
            }

            db.CalculatorCatalog.Add(new CalculatorCatalogEntity
            {
                Slug = definition.Slug,
                Name = definition.Name,
                CategoryId = category.Id,
                Summary = definition.Summary,
                SeoTitle = definition.SeoTitle,
                SeoDescription = definition.SeoDescription,
                EducationalContent = GetEducationalContent(definition.Slug),
                SortOrder = ++maxSort,
                IsActive = true
            });

            var faqSort = 0;
            foreach (var faq in definition.FaqItems)
            {
                db.FaqItems.Add(new FaqItemEntity
                {
                    CalculatorSlug = definition.Slug,
                    Question = faq.Question,
                    Answer = faq.Answer,
                    SortOrder = faqSort++
                });
            }
        }

        await db.SaveChangesAsync(cancellationToken);
    }

    public static async Task SeedBlogPostsAsync(AppDbContext db, CancellationToken cancellationToken = default)
    {
        foreach (var article in BlogArticleSeedData.GetAll())
        {
            var exists = await db.BlogPosts.AnyAsync(x => x.Slug == article.Slug, cancellationToken);
            if (!exists)
            {
                db.BlogPosts.Add(MapToEntity(article));
                continue;
            }

            var existing = await db.BlogPosts.FirstAsync(x => x.Slug == article.Slug, cancellationToken);
            existing.Title = article.Title;
            existing.Summary = article.Summary;
            existing.Content = article.Content;
            existing.Category = article.Category;
            existing.RelatedCalculatorSlug = article.RelatedCalculatorSlug;
            existing.PublishedAt = article.PublishedAt;
            existing.Author = article.Author;
        }

        await db.SaveChangesAsync(cancellationToken);
    }

    private static BlogPostEntity MapToEntity(BlogArticleSeed article) => new()
    {
        Slug = article.Slug,
        Title = article.Title,
        Summary = article.Summary,
        Content = article.Content,
        PublishedAt = article.PublishedAt,
        Category = article.Category,
        RelatedCalculatorSlug = article.RelatedCalculatorSlug,
        Author = article.Author,
        IsPublished = true
    };

    private static async Task SyncEducationalContentAsync(AppDbContext db, CancellationToken cancellationToken)
    {
        var bySlug = CalculatorSeedData.GetDefinitions()
            .ToDictionary(d => d.Slug, d => GetEducationalContent(d.Slug), StringComparer.OrdinalIgnoreCase);
        var entities = await db.CalculatorCatalog.ToListAsync(cancellationToken);
        var changed = false;

        foreach (var entity in entities)
        {
            if (!bySlug.TryGetValue(entity.Slug, out var content)
                || string.Equals(entity.EducationalContent, content, StringComparison.Ordinal))
            {
                continue;
            }

            entity.EducationalContent = content;
            changed = true;
        }

        if (changed)
        {
            await db.SaveChangesAsync(cancellationToken);
        }
    }

    private static string GetEducationalContent(string slug) => slug switch
    {
        "salario-liquido" =>
            "Informe o salário bruto, dependentes e descontos opcionais. O resultado mostra INSS, IRRF e o líquido estimado com base nas tabelas de 2026. Para o caminho inverso, use a <a href=\"/calculadoras/salario-bruto-necessario\">calculadora de salário bruto necessário</a>. Consulte também as <a href=\"/salario-liquido\">páginas por valor bruto</a>.",
        "salario-bruto-necessario" =>
            "Informe o salário líquido desejado, dependentes e descontos (vale-transporte, vale-refeição e outros). A ferramenta estima o bruto necessário com as tabelas de INSS e IRRF 2026. Compare com a <a href=\"/calculadoras/salario-liquido\">calculadora de salário líquido</a> ou simule um aumento na <a href=\"/calculadoras/proposta-salarial\">proposta salarial</a>.",
        "proposta-salarial" =>
            "Informe o salário bruto atual e o valor proposto, com os mesmos dependentes e descontos nos dois cenários. O resultado mostra ganho líquido mensal e anual e o percentual real no bolso. Veja também <a href=\"/calculadoras/salario-bruto-necessario\">quanto bruto pedir para um líquido alvo</a>.",
        "ferias" =>
            "Informe o salário base, avos ou dias de férias e opções como abono. A calculadora estima férias com o terço constitucional e descontos tributários. Combine com a <a href=\"/calculadoras/salario-liquido\">calculadora de salário líquido</a> para ver o impacto no mês.",
        "rescisao-clt" =>
            "Informe salário, tempo de empresa, dias trabalhados no mês e tipo de desligamento. No pedido de demissão sem aviso prévio, desconta-se até 30 dias de salário. A multa FGTS de 40% entra apenas na demissão sem justa causa.",
        "decimo-terceiro" =>
            "Informe o salário base, avos do ano e, se houver, o adiantamento da primeira parcela. A estimativa separa o benefício bruto dos descontos que costumam cair na parcela final.",
        "hora-extra" =>
            "Informe o valor da hora ou o salário mensal, a quantidade de horas e o adicional. A ferramenta estima o valor das horas extras e o reflexo de DSR conforme o turno selecionado.",
        "inss" =>
            "Informe o salário de contribuição. O INSS é calculado de forma progressiva por faixas, respeitando o teto vigente em 2026.",
        "irrf" =>
            "Informe a base de cálculo ou o salário bruto, conforme o modo escolhido, e os dependentes. A estimativa usa a tabela e a redução legal de IRRF de 2026.",
        "fgts" =>
            "Informe salário e meses trabalhados para ver depósitos de 8%. Opcionalmente informe saldo atual e tipo de desligamento para estimar multa rescisória.",
        "seguro-desemprego" =>
            "Informe os últimos salários brutos, meses com carteira e o motivo do desligamento. A estimativa usa a tabela do MTE 2026 para parcela e quantidade; pedido de demissão em geral zera o direito.",
        "vale-transporte-hibrido" =>
            "Informe salário base, custo de ida e volta por dia, dias presenciais e, se quiser, o desconto atual do holerite. A ferramenta compara o custo do período com o limite educativo de 6% do salário base. Para ver o impacto final no bolso, use também a <a href=\"/calculadoras/salario-liquido\">calculadora de salário líquido</a>.",
        "simulador-mei" =>
            "Informe o faturamento mensal estimado e a atividade. O DAS MEI é fixo conforme comércio, serviços ou ambos, e o limite anual de referência é R$ 81.000.",
        "custo-funcionario" =>
            "Informe o salário bruto e os benefícios mensais. A estimativa soma FGTS, INSS patronal aproximado, provisões de 13º e férias e um RAT/SAT simplificado para o custo total da empresa.",
        "multa-atraso" =>
            "Informe o valor em atraso, os dias e os percentuais de multa e juros. O padrão educativo é 2% de multa + 1% ao mês proporcional aos dias — ajuste conforme o contrato.",
        "conversor-salario" =>
            "Informe o valor e a base (mensal, diário ou hora). A conversão usa divisor 30 para o dia e divisor de jornada (220h na jornada de 44h) para a hora.",
        "pj-vs-clt" =>
            "Informe salário bruto CLT, faturamento PJ (opcional), dependentes, alíquota do Simples e despesas fixas. " +
            "Veja o faturamento PJ equivalente ao líquido CLT e explore páginas por valor em <a href=\"/clt-pj\">CLT x PJ</a>.",
        "juros-compostos" =>
            "Informe capital inicial, aporte mensal, taxa mensal e prazo. A projeção capitaliza mês a mês e separa o total investido dos juros estimados.",
        "financiamento" =>
            "Informe o valor financiado, a taxa mensal, o prazo e o sistema Price ou SAC. Compare parcela e juros totais; a taxa digitada não é necessariamente o CET.",
        _ => "Use os campos indicados para obter uma estimativa educativa. Consulte um profissional para decisões formais."
    };
}
