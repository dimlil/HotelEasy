FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["HotelEasy.Web/HotelEasy.Web.csproj", "HotelEasy.Web/"]
RUN dotnet restore "HotelEasy.Web/HotelEasy.Web.csproj"
COPY . .
WORKDIR /src/HotelEasy.Web
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HotelEasy.Web.dll"]
