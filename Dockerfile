# Stage de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier le fichier csproj et restaurer les dépendances
COPY api.csproj .
RUN dotnet restore

# Copier tout le reste et construire l'application
COPY . .
RUN dotnet build "api.csproj" -c Release -o /app/build

# Stage de publication
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS publish
WORKDIR /app
COPY --from=build /app/build .

# Définir la commande de démarrage
ENTRYPOINT ["dotnet", "api.dll"]
