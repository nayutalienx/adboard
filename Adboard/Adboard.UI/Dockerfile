FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Adboard.UI/Adboard.UI.csproj", "Adboard.UI/"]
COPY ["Adboard.Contracts/Adboard.Contracts.csproj", "Adboard.Contracts/"]
RUN dotnet restore "Adboard.UI/Adboard.UI.csproj"
COPY . .
WORKDIR "/src/Adboard.UI"
RUN dotnet build "Adboard.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Adboard.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adboard.UI.dll"]
