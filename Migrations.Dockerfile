FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /app
COPY ["./Hotel.Server/Hotel.Server.csproj", "./"]

RUN dotnet tool install --global dotnet-ef

RUN dotnet restore "./Hotel.Server.csproj"
COPY . .
WORKDIR "/app/."

CMD dotnet ef database update
