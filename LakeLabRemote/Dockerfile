FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ["LakeLabRemote/LakeLabRemote.csproj", "LakeLabRemote/"]
COPY ["LakeLabModelsLib/LakeLabLib.csproj", "LakeLabModelsLib/"]
RUN dotnet restore "LakeLabRemote/LakeLabRemote.csproj"
COPY . .
WORKDIR "/src/LakeLabRemote"
RUN dotnet build "LakeLabRemote.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "LakeLabRemote.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LakeLabRemote.dll"]