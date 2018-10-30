FROM scratch
COPY bin/Debug/netcoreapp2.0/linux-arm/publish /app
WORKDIR /app
RUN chmod 755 ./LakeLabPiApp
ENTRYPOINT ["LakeLabPiApp"]