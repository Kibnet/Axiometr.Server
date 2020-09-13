#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.5-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Axiometr.Server/Axiometr.Server.csproj", "Axiometr.Server/"]
COPY ["Axiometr.Server.ServiceInterface/Axiometr.Server.ServiceInterface.csproj", "Axiometr.Server.ServiceInterface/"]
COPY ["Axiometr.Server.ServiceModel/Axiometr.Server.ServiceModel.csproj", "Axiometr.Server.ServiceModel/"]
COPY ["Axiometr.Server.Domain/Axiometr.Server.Domain.csproj", "Axiometr.Server.Domain/"]
RUN dotnet restore "Axiometr.Server/Axiometr.Server.csproj"
COPY . .
WORKDIR "/src/Axiometr.Server"
RUN dotnet build "Axiometr.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Axiometr.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Axiometr.Server.dll"]
