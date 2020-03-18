FROM mcr.microsoft.com/mssql/server:2017-latest
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=SA_pa55w0rd
COPY ./SpamReportsManagementSystem.Database /var/opt/SpamReportsManagementSystem.Database
RUN apt-get update \
    && apt-get install unzip dotnet-sdk-2.2 -y --no-install-recommends \
    && wget -q -O /var/opt/sqlpackage.zip https://go.microsoft.com/fwlink/?linkid=2069122 \
    && unzip -qq /var/opt/sqlpackage.zip -d /var/opt/sqlpackage \
    && rm /var/opt/sqlpackage.zip \
    && chmod +x /var/opt/sqlpackage/sqlpackage \
    && mv /var/opt/SpamReportsManagementSystem.Database/bin/Debug/master.dacpac /var/opt/SpamReportsManagementSystem.Database/bin/Debug/MASTER.DACPAC \
    && mv /var/opt/SpamReportsManagementSystem.Database/bin/Debug/msdb.dacpac /var/opt/SpamReportsManagementSystem.Database/bin/Debug/MSDB.DACPAC \
    && (/opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" \
    && /var/opt/sqlpackage/sqlpackage /a:Publish /tdn:reports \
         /pr:/var/opt/SpamReportsManagementSystem.Database/docker.publish.xml /sf:/var/opt/SpamReportsManagementSystem.Database/bin/Debug/SpamReportsManagementSystem.Database.dacpac \
         /p:IncludeCompositeObjects=true /tu:sa /tp:$SA_PASSWORD \
    && (/opt/mssql-tools/bin/sqlcmd -S localhost -d reports -U SA -P $SA_PASSWORD -Q 'ALTER DATABASE reports SET RECOVERY SIMPLE') \
    && pkill sqlservr \
    && apt-get remove dotnet-sdk-2.2 -y \
    && apt-get autoremove -y \
    && rm -rf /usr/share/dotnet \
    && rm -rf /var/lib/apt/lists/* \
    && rm -rf /var/opt/<project_dir> \
    && rm -rf /var/opt/sqlpackage