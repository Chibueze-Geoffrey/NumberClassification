# Use the official ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /NumberClassification

# Copy csproj files and restore as distinct layers
COPY ["NumberClassification.API/NumberClassification.API.csproj", "NumberClassification.API/"]
COPY ["NumberClassification.Domain/NumberClassification.Domain.csproj", "NumberClassification.Domain/"]
COPY ["NumberClassification.Application/NumberClassification.Application.csproj", "NumberClassification.Application/"]
COPY ["NumberClassification.Infrastructure/NumberClassification.Infrastructure.csproj", "NumberClassification.Infrastructure/"]
RUN dotnet restore "NumberClassification.API/NumberClassification.API.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/NumberClassification/NumberClassification.API"
RUN dotnet build "NumberClassification.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NumberClassification.API.csproj" -c Release -o /app/publish

# Use the base image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NumberClassification.API.dll"]
