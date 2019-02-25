FROM microsoft/aspnetcore:2.1
COPY bin/Release/PublishOutput /app
WORKDIR /app
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "LakeLabRemote.dll"]