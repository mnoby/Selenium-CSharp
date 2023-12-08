FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .
RUN dotnet restore
RUN dotnet build -c Release
CMD ["dotnet", "test", "--filter", "UnitTest",  "--logger:trx"]
