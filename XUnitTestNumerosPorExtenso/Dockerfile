#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["XUnitTestNumerosPorExtenso/XUnitTestNumerosPorExtenso.csproj", "XUnitTestNumerosPorExtenso/"]
COPY ["NumerosPorExtenso/NumerosPorExtenso.csproj", "NumerosPorExtenso/"]
RUN dotnet restore "XUnitTestNumerosPorExtenso/XUnitTestNumerosPorExtenso.csproj"
COPY . .
WORKDIR "/src/XUnitTestNumerosPorExtenso"
RUN dotnet build "XUnitTestNumerosPorExtenso.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "XUnitTestNumerosPorExtenso.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XUnitTestNumerosPorExtenso.dll"]