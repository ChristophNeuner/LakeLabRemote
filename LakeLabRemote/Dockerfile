FROM microsoft/aspnetcore:2.0
COPY bin/Release/PublishOutput /app
WORKDIR /app
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "LakeLabRemote.dll"]