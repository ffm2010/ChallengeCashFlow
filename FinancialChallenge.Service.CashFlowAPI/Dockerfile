FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /usr/app

COPY ./ ./

ENTRYPOINT [ "dotnet", "FinancialChallenge.Service.CashFlowAPI.dll" ]