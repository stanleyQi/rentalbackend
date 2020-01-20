FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["rentalbackend/rentalbackend.csproj", "rentalbackend/"]
RUN dotnet restore "rentalbackend/rentalbackend.csproj"
COPY . .
WORKDIR "/src/rentalbackend"
RUN dotnet build "rentalbackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "rentalbackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "rentalbackend.dll"]