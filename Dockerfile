# --- Build aşaması ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Projeleri kopyala
COPY ["VehicleRenting.sln", "./"]
COPY ["VehicleRenting.UI/VehicleRenting.UI.csproj", "VehicleRenting.UI/"]
COPY ["VehicleRenting.BLL/VehicleRenting.BLL.csproj", "VehicleRenting.BLL/"]
COPY ["VehicleRenting.DAL/VehicleRenting.DAL.csproj", "VehicleRenting.DAL/"]
COPY ["VehicleRenting.Entities/VehicleRenting.Entities.csproj", "VehicleRenting.Entities/"]

RUN dotnet restore "VehicleRenting.UI/VehicleRenting.UI.csproj"

# Tüm dosyaları kopyala
COPY . .

# Build + Publish
WORKDIR "/src/VehicleRenting.UI"
RUN dotnet publish -c Release -o /app/publish

# --- Runtime aşaması ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Render, 10000 portunu kullanıyor
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "VehicleRenting.UI.dll"]
