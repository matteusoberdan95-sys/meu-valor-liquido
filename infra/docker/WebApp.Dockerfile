FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY MeuValorLiquido.slnx .
COPY src/Core/MeuValorLiquido.Core.csproj src/Core/
COPY src/Shared/MeuValorLiquido.Shared.csproj src/Shared/
COPY src/Modules/Calculators/MeuValorLiquido.Modules.Calculators.csproj src/Modules/Calculators/
COPY src/Modules/Content/MeuValorLiquido.Modules.Content.csproj src/Modules/Content/
COPY src/Modules/Contact/MeuValorLiquido.Modules.Contact.csproj src/Modules/Contact/
COPY src/Modules/Newsletter/MeuValorLiquido.Modules.Newsletter.csproj src/Modules/Newsletter/
COPY src/Modules/Ads/MeuValorLiquido.Modules.Ads.csproj src/Modules/Ads/
COPY src/WebApp/MeuValorLiquido.WebApp.csproj src/WebApp/

RUN dotnet restore src/WebApp/MeuValorLiquido.WebApp.csproj

COPY src/ src/

RUN dotnet publish src/WebApp/MeuValorLiquido.WebApp.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "MeuValorLiquido.WebApp.dll"]
