#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim-arm32v7 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PictureStore.Web.Api/PictureStore.Web.Api.csproj", "PictureStore.Web.Api/"]
RUN dotnet restore "PictureStore.Web.Api/PictureStore.Web.Api.csproj"
COPY . .
WORKDIR "/src/PictureStore.Web.Api"
RUN dotnet build "PictureStore.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PictureStore.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PictureStore.Web.Api.dll"]