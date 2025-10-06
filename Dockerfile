# ---- Build Stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy csproj and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build and publish the app
RUN dotnet publish -c Release -o /app/publish

# ---- Runtime Stage ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy build output to runtime container
COPY --from=build /app/publish .

# Expose port 8080 (Render expects apps to listen on this port)
EXPOSE 8080

# Set environment variable for ASP.NET Core
ENV ASPNETCORE_URLS=http://+:8080

# Run the app
ENTRYPOINT ["dotnet", "API.dll"]
