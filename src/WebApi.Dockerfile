FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SpamReportsManagementSystem.WebApi/SpamReportsManagementSystem.WebApi.csproj", "SpamReportsManagementSystem.WebApi/"]
RUN dotnet restore "SpamReportsManagementSystem.WebApi/SpamReportsManagementSystem.WebApi.csproj"
COPY . .
WORKDIR "/src/SpamReportsManagementSystem.WebApi"
RUN dotnet build "SpamReportsManagementSystem.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SpamReportsManagementSystem.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpamReportsManagementSystem.WebApi.dll"]