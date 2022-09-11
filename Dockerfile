FROM mcr.microsoft.com/dotnet/sdk:6.0

# Copy csproj and restore as distinct layers
WORKDIR '/home/alexandre_entrega'
COPY *.csproj '/home/alexandre_entrega'
RUN dotnet restore

# Copy everything else and build
COPY '.' '/home/alexandre_entrega'

ENTRYPOINT [ "dotnet", "run" ]
