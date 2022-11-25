FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MyGymApp/MyGymApp.csproj", "MyGymApp/"]
RUN dotnet restore "MyGymApp/MyGymApp.csproj"
COPY . .
WORKDIR "/src/MyGymApp"
RUN dotnet build "MyGymApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyGymApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyGymApp.dll"]
