#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Servises/Servises.csproj", "Servises/"]
COPY ["BL/BL.csproj", "BL/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Dal/Dal.csproj", "Dal/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["Tools/Tools.csproj", "Tools/"]
RUN dotnet restore "Servises/Servises.csproj"
COPY . .
WORKDIR "/src/Servises"
RUN dotnet build "Servises.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Servises.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Servises.dll"]