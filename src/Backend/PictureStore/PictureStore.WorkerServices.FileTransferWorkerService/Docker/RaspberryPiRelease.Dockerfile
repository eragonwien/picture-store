#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim-arm32v7 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PictureStore.WorkerServices.FileTransferWorkerService/PictureStore.WorkerServices.FileTransferWorkerService.csproj", "PictureStore.WorkerServices.FileTransferWorkerService/"]
RUN dotnet restore "PictureStore.WorkerServices.FileTransferWorkerService/PictureStore.WorkerServices.FileTransferWorkerService.csproj"
COPY . .
WORKDIR "/src/PictureStore.WorkerServices.FileTransferWorkerService"
RUN dotnet build "PictureStore.WorkerServices.FileTransferWorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PictureStore.WorkerServices.FileTransferWorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PictureStore.WorkerServices.FileTransferWorkerService.dll"]