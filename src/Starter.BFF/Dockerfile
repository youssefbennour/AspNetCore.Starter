FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src
COPY ["src/Starter.BFF/Starter.BFF.csproj", "Starter.BFF/"]

RUN dotnet restore "Starter.BFF/Starter.BFF.csproj"

COPY src/ .

WORKDIR "/src/Starter.BFF"
RUN dotnet build "Starter.BFF.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Starter.BFF.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Starter.BFF.dll"]
