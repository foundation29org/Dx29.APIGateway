#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Dx29.BioEntities/Dx29.BioEntities.csproj", "Dx29.BioEntities/"]
COPY ["Dx29/Dx29.csproj", "Dx29/"]
RUN dotnet restore "Dx29.BioEntities/Dx29.BioEntities.csproj"
COPY . .
WORKDIR "/src/Dx29.BioEntities"
RUN dotnet build "Dx29.BioEntities.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dx29.BioEntities.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dx29.BioEntities.dll"]
