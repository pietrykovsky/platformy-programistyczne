FROM mcr.microsoft.com/dotnet/sdk:8.0 as build

# Copy the entire WebApp solution directory
COPY Lab3 /Lab3

WORKDIR /Lab3

# Restore NuGet packages
RUN dotnet restore

# Build the solution
RUN dotnet publish -c Debug -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=build /app ./
EXPOSE 80

ENTRYPOINT ["dotnet", "WebApp.dll"]
