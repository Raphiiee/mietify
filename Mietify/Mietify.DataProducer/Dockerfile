#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Mietify.DataProducer/Mietify.DataProducer.csproj", "Mietify.DataProducer/"]
COPY ["Mietify.Util/Mietify.Util.csproj", "Mietify.Util/"]
RUN dotnet restore "Mietify.DataProducer/Mietify.DataProducer.csproj"
COPY . .
WORKDIR "/src/Mietify.DataProducer"
RUN dotnet build "Mietify.DataProducer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mietify.DataProducer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mietify.DataProducer.dll"]