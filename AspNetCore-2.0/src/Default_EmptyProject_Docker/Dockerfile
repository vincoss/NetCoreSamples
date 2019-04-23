FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Default_EmptyProject_Docker/Default_EmptyProject_Docker.csproj", "src/Default_EmptyProject_Docker/"]
RUN dotnet restore "src/Default_EmptyProject_Docker/Default_EmptyProject_Docker.csproj"
COPY . .
WORKDIR "/src/src/Default_EmptyProject_Docker"
RUN dotnet build "Default_EmptyProject_Docker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Default_EmptyProject_Docker.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Default_EmptyProject_Docker.dll"]