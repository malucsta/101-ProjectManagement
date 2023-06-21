#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ProjectManagement.API/ProjectManagement.API.csproj", "ProjectManagement.API/"]
COPY ["ProjectManagement.Core/ProjectManagement.Core.csproj", "ProjectManagement.Core/"]
COPY ["ProjectManagement.Infra/ProjectManagement.Infra.csproj", "ProjectManagement.Infra/"]
RUN dotnet restore "ProjectManagement.API/ProjectManagement.API.csproj"
COPY . .
WORKDIR "/src/ProjectManagement.API"
RUN dotnet build "ProjectManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjectManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectManagement.API.dll"]