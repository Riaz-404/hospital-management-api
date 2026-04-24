# Build the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["HospitalManagementApi.API/HospitalManagementApi.API.csproj", "HospitalManagementApi.API/"]
COPY ["HospitalManagementApi.Application/HospitalManagementApi.Application.csproj", "HospitalManagementApi.Application/"]
COPY ["HospitalManagementApi.Domain/HospitalManagementApi.Domain.csproj", "HospitalManagementApi.Domain/"]
COPY ["HospitalManagementApi.Infrastructure/HospitalManagementApi.Infrastructure.csproj", "HospitalManagementApi.Infrastructure/"]

RUN dotnet restore "HospitalManagementApi.API/HospitalManagementApi.API.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/HospitalManagementApi.API"
RUN dotnet build "HospitalManagementApi.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "HospitalManagementApi.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose port 8080
EXPOSE 8080

# Set environment to Production
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

# Set the entry point
ENTRYPOINT ["dotnet", "HospitalManagementApi.API.dll"]