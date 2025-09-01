# Compilando código
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder
WORKDIR /source
COPY . ./

RUN dotnet restore API/API.csproj
RUN dotnet publish API/API.csproj -c Release -o /source/publish

# Installer image
FROM amd64/buildpack-deps:bullseye-curl AS installer
 
# Retrieve ASP.NET Core
RUN aspnetcore_version=8.0.6 \
    && curl -fSL --output aspnetcore.tar.gz https://dotnetcli.azureedge.net/dotnet/aspnetcore/Runtime/$aspnetcore_version/aspnetcore-runtime-$aspnetcore_version-linux-x64.tar.gz \
    && aspnetcore_sha512='16cd54c431d80710a06037f8ea593e04764a80cbaad75e1db4225fbe3e7fce4c4d279f40757b9811e1c092436d2a1ca3be64c74cb190ebf78418a9865992ad12' \
    && echo "$aspnetcore_sha512  aspnetcore.tar.gz" | sha512sum -c - \
    && tar -oxzf aspnetcore.tar.gz ./shared/Microsoft.AspNetCore.App \
    && rm aspnetcore.tar.gz

# ASP.NET Core image
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final

EXPOSE 80
EXPOSE 443

#COPY produccion.key /usr/local/share/ca-certificates
#COPY star_igssgt_org.crt /usr/local/share/ca-certificates
#COPY CertIGSS.pfx /usr/local/share/ca-certificates
RUN update-ca-certificates

#ENV ASPNETCORE_URLS="https://+;http://+"
#ENV ASPNETCORE_HTTPS_PORT=8005
#ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/usr/local/share/ca-certificates/CertIGSS.pfx
#ENV ASPNETCORE_Kestrel__Certificates__Default__Password="IGSS25"

ENV DOTNET_VERSION=8.0

COPY --from=installer ["/shared/Microsoft.AspNetCore.App", "/usr/share/dotnet/shared/Microsoft.AspNetCore.App"]

#REPORTING SERVICES
RUN apt-get update && apt-get install -y --no-install-recommends gss-ntlmssp cifs-utils
#Copiando el codigo compilado 
WORKDIR /api
COPY --from=builder /source/publish /api
ENTRYPOINT ["dotnet", "API.dll"]