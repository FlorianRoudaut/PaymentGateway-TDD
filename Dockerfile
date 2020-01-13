FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY . .

RUN dotnet restore "./PiggyBankApi/PiggyBankApi.csproj"
RUN dotnet restore "./PaymentGateway/PaymentGateway.csproj"
RUN dotnet build "./PaymentGateway/PaymentGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./PaymentGateway/PaymentGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentGateway.dll"]