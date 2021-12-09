#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Dx29.TASearch/Dx29.TASearch.csproj", "Dx29.TASearch/"]
COPY ["Dx29.Data/Dx29.Data.csproj", "Dx29.Data/"]
COPY ["Dx29/Dx29.csproj", "Dx29/"]
RUN dotnet restore "Dx29.TASearch/Dx29.TASearch.csproj"
COPY . .
WORKDIR "/src/Dx29.TASearch"
RUN dotnet build "Dx29.TASearch.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dx29.TASearch.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dx29.TASearch.dll"]
