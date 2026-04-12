FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY LearningApp.API/*.csproj ./LearningApp.API/
RUN dotnet restore LearningApp.API/LearningApp.API.csproj

COPY LearningApp.API/ ./LearningApp.API/
RUN dotnet publish LearningApp.API/LearningApp.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "LearningApp.API.dll"]
