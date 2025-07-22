# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ECommerceApp.API/ECommerceApp.API.csproj", "ECommerceApp.API/"]
COPY ["ECommerceApp.Application/ECommerceApp.Application.csproj", "ECommerceApp.Application/"]
COPY ["ECommerceApp.Domain/ECommerceApp.Domain.csproj", "ECommerceApp.Domain/"]
COPY ["ECommerceApp.Infrastructure/ECommerceApp.Infrastructure.csproj", "ECommerceApp.Infrastructure/"]
RUN dotnet restore "ECommerceApp.API/ECommerceApp.API.csproj"

COPY . .

# Publish
WORKDIR "/src/ECommerceApp.API"
RUN dotnet publish "ECommerceApp.API.csproj" -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ECommerceApp.API.dll"]
