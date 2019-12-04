FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Adboard.API/Adboard.API.csproj", "Adboard.API/"]
COPY ["BusinessLogicLayer/BusinessLogicLayer.Abstraction/BusinessLogicLayer.Abstraction.csproj", "BusinessLogicLayer/BusinessLogicLayer.Abstraction/"]
COPY ["Adboard.Contracts/Adboard.Contracts.csproj", "Adboard.Contracts/"]
COPY ["Infrastructure/Infrastructure.DependencyInjection/Infrastructure.DependencyInjection.csproj", "Infrastructure/Infrastructure.DependencyInjection/"]
COPY ["DataAccessLayer/DataAccessLayer.Abstraction/DataAccessLayer.Abstraction.csproj", "DataAccessLayer/DataAccessLayer.Abstraction/"]
COPY ["DataAccessLayer/DataAccessLayer.Models/DataAccessLayer.Models.csproj", "DataAccessLayer/DataAccessLayer.Models/"]
COPY ["DataAccessLayer/DataAccessLayer.Repositories/DataAccessLayer.Repositories.csproj", "DataAccessLayer/DataAccessLayer.Repositories/"]
COPY ["DataAccessLayer/DataAccessLayer.EntityFramework/DataAccessLayer.EntityFramework.csproj", "DataAccessLayer/DataAccessLayer.EntityFramework/"]
COPY ["BusinessLogicLayer/BusinessLogicLayer.Implementation/BusinessLogicLayer.Implementation.csproj", "BusinessLogicLayer/BusinessLogicLayer.Implementation/"]
RUN dotnet restore "Adboard.API/Adboard.API.csproj"
COPY . .
WORKDIR "/src/Adboard.API"
RUN dotnet build "Adboard.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Adboard.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adboard.API.dll"]
