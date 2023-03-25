#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Mietify.Consumer/Mietify.Consumer.csproj", "Mietify.Consumer/"]
COPY ["Mietify.DataProducer/Mietify.DataProducer.csproj", "Mietify.DataProducer/"]
COPY ["Mietify.Util/Mietify.Util.csproj", "Mietify.Util/"]
RUN dotnet restore "Mietify.Consumer/Mietify.Consumer.csproj"
COPY . .
WORKDIR "/src/Mietify.Consumer"
RUN dotnet build "Mietify.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mietify.Consumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mietify.Consumer.dll"]