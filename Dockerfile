# Etapa base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos solo la carpeta del proyecto
COPY Web_Api/ ./Web_Api/
WORKDIR /src/Web_Api

# Publicamos el proyecto
RUN dotnet publish "Web_Api.csproj" -c Release -o /app/publish

# Etapa final: runtime
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
CMD ["dotnet", "Web_Api.dll"]
