FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UsersTest/UsersTest.csproj", "UsersTest/"]
RUN dotnet restore "UsersTest/UsersTest.csproj"
COPY . .
WORKDIR "/src/UsersTest"
RUN dotnet build "UsersTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UsersTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UsersTest.dll"]
