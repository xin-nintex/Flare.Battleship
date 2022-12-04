FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Flare.Battleship.UI.Console/Flare.Battleship.UI.Console.csproj", "Flare.Battleship.UI.Console/"]
RUN dotnet restore "Flare.Battleship.UI.Console/Flare.Battleship.UI.Console.csproj"
COPY . .
WORKDIR "/src/Flare.Battleship.UI.Console"
RUN dotnet build "Flare.Battleship.UI.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Flare.Battleship.UI.Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Flare.Battleship.UI.Console.dll"]
