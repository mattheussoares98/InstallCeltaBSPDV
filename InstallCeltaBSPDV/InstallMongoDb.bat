@echo off
:: 1. Run the silent MSI installation without allowing Windows Installer to reboot automatically
echo Installing MongoDB 7.0...
msiexec.exe /l*v mdbinstall.log /qb /norestart /i C:\Install\PDV\Database\mongodb.msi SHOULD_INSTALL_COMPASS="0" ADDLOCAL="ServerService" REBOOT=ReallySuppress

if %ERRORLEVEL% NEQ 3010 if %ERRORLEVEL% NEQ 0 (
    echo MongoDB installer finished with exit code %ERRORLEVEL%.
    exit /b %ERRORLEVEL%
)

:: 2. Wait for installation to finish and create the remote configuration file
echo Writing remote configuration rules...
set "CFG_FILE=C:\Program Files\MongoDB\Server\7.0\bin\mongod.cfg"

(
echo storage:
echo   dbPath: C:\Program Files\MongoDB\Server\7.0\data
echo systemLog:
echo   destination: file
echo   logAppend: true
echo   path: C:\Temp\mongod.log
echo net:
echo   port: 27017
echo   bindIp: 0.0.0.0,Your-Server-Public-IP
) > "%CFG_FILE%"

:: 3. Restart the Windows service to apply the remote ip rules
echo Restarting MongoDB Service...
net stop MongoDB
net start MongoDB

echo Done! MongoDB is now configured for remote connections.
