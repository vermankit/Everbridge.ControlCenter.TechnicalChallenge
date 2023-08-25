#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Everbridge.ControlCenter.TechnicalChallenge/Everbridge.ControlCenter.TechnicalChallenge.csproj", "Everbridge.ControlCenter.TechnicalChallenge/"]
COPY ["DoorDatabase/DoorDatabase.csproj", "DoorDatabase/"]
RUN dotnet restore "Everbridge.ControlCenter.TechnicalChallenge/Everbridge.ControlCenter.TechnicalChallenge.csproj"
COPY . .
WORKDIR "/src/Everbridge.ControlCenter.TechnicalChallenge"
RUN dotnet build "Everbridge.ControlCenter.TechnicalChallenge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Everbridge.ControlCenter.TechnicalChallenge.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Everbridge.ControlCenter.TechnicalChallenge.dll"]