# DotNet Source - filtered

FROM alpine AS dotnet-source
ARG CONFIGURATION=Release
WORKDIR /app
COPY Source ./Source/
RUN rm -rf Source/ManagementUI

# DotNet Build
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS dotnet-build
ARG CONFIGURATION=Release
WORKDIR /app

COPY default.props ./
COPY versions.props ./
COPY Runtime.sln ./
COPY --from=dotnet-source /app/Source ./Source/

COPY Specifications ./Specifications/

WORKDIR /app/Source/Server
RUN dotnet restore
RUN dotnet publish -c ${CONFIGURATION} -o out

# # Web Build
# FROM node:13.12 AS web-build
# WORKDIR /app

# COPY tslint.json ./
# COPY tsconfig.settings.json ./
# COPY Source/ManagementUI ./Source/ManagementUI

# WORKDIR /app/Source/ManagementUI

# RUN yarn
# RUN yarn build

# Runtime Image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ARG CONFIGURATION=Release

RUN echo Configuration = $CONFIGURATION

RUN if [ "$CONFIGURATION" = "Debug" ] ; then apt-get update && \
    apt-get install -y --no-install-recommends unzip procps && \
    rm -rf /var/lib/apt/lists/* \
    ; fi

RUN if [ "$CONFIGURATION" = "debug" ] ; then curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l ~/vsdbg ; fi

WORKDIR /app
COPY --from=dotnet-build /app/Source/Server/out ./
COPY --from=dotnet-build /app/Source/Server/.dolittle ./.dolittle
#COPY --from=web-build /app/Source/ManagementUI/wwwroot ./wwwroot

EXPOSE 81
EXPOSE 9700

ENTRYPOINT ["dotnet", "Dolittle.Runtime.Server.dll"]