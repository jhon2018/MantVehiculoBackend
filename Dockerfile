# Etapa base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos ambas carpetas necesarias
COPY Web_Api/ ./Web_Api/
COPY AccesoDatos/ ./AccesoDatos/

# Entramos al proyecto principal
WORKDIR /src/Web_Api

# Publicamos
RUN dotnet publish "Web_Api.csproj" -c Release -o /app/publish

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
CMD ["dotnet", "Web_Api.dll"]
