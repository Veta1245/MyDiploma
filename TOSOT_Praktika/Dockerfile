FROM mcr.microsoft.com/windows/servercore:ltsc2019
RUN powershell -Command \
    Install-WindowsFeature NET-Framework-45-ASPNET,NET-Framework-Features,NET-Framework-Core
COPY ["TOSOT_Praktikaaaa/TOSOT_Praktika/bin/Debug/TOSOT_Praktika.exe", "D:/TOSOT_Praktika/"]
WORKDIR D:/TOSOT_Praktika
CMD ["TOSOT_Praktika.exe"]