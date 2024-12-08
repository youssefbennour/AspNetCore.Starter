FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/Starter/Starter.csproj", "Starter/"]
RUN dotnet restore "Starter/Starter.csproj"
COPY /src .
WORKDIR "/src/Starter"
RUN dotnet build "Starter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Starter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Starter.dll"]
