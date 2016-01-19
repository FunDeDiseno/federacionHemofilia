@echo off 
cd %~dp0 
 
 
SETLOCAL 
SET NUGET_VERSION=latest 
SET CACHED_NUGET=%LocalAppData%\NuGet\nuget.%NUGET_VERSION%.exe 
SET BUILDCMD_KOREBUILD_VERSION= 
SET BUILDCMD_DNX_VERSION= 
 
 
IF NOT EXIST %NUGET_PATH% ( 
     IF NOT EXIST %CACHED_NUGET% ( 
         echo Downloading latest version of NuGet.exe... 
         IF NOT EXIST %LocalAppData%\NuGet (  
             md %LocalAppData%\NuGet 
         ) 
@powershell -NoProfile -ExecutionPolicy unrestricted -Command "$ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest 'https://dist.nuget.org/win-x86-commandline/%NUGET_VERSION%/nuget.exe' -OutFile '%CACHED_NUGET%'" 
) 
     copy %CACHED_NUGET% %NUGET_PATH% > nul 
) 

 
:copynuget 
IF EXIST .nuget\nuget.exe goto restore 
md .nuget 
copy %CACHED_NUGET% .nuget\nuget.exe > nul 
 
:getdnx 
IF "%BUILDCMD_DNX_VERSION%"=="" ( 
   SET BUILDCMD_DNX_VERSION=1.0.0-rc1-update1 
) 
IF "%SKIP_DNX_INSTALL%"=="" ( 
   CALL packages\KoreBuild\build\dnvm install %BUILDCMD_DNX_VERSION% -runtime CoreCLR -arch x86 -alias default 
   CALL packages\KoreBuild\build\dnvm install default -runtime CoreCLR -arch x64 
   CALL packages\KoreBuild\build\dnvm install default -runtime CLR -arch x64 
   CALL packages\KoreBuild\build\dnvm install default -runtime CLR -arch x86 -alias default 
) ELSE ( 
   CALL packages\KoreBuild\build\dnvm use default -runtime CLR -arch x86 
) 
