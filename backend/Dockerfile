FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY Platform.sln ./
COPY Platform.Core/*.csproj ./Platform.Core/
COPY Platform.Infrastructure/*.csproj ./Platform.Infrastructure/
COPY Platform.API/*.csproj ./Platform.API/
COPY Platform.Modules.Base/*.csproj ./Platform.Modules.Base/
COPY Platform.Modules.ProductManagement/*.csproj ./Platform.Modules.ProductManagement/

# Restore dependencies
RUN dotnet restore

# Copy source code
COPY . .

# Build
WORKDIR /src/Platform.API
RUN dotnet build -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Platform.API.dll"]
