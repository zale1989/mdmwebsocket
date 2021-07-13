#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

EXPOSE 80
EXPOSE 443
EXPOSE 3008
EXPOSE 4040

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MDM.WebSocket/MDM.WebSocket.csproj", "MDM.WebSocket/"]
RUN dotnet restore "MDM.WebSocket/MDM.WebSocket.csproj"
COPY . .
WORKDIR "/src/MDM.WebSocket"
RUN dotnet build "MDM.WebSocket.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MDM.WebSocket.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MDM.WebSocket.dll"]