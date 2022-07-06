cls
rem set MSBuildSDKsPath=C:\Program Files\dotnet\sdk\6.0.202\Sdks
rem dotnet tool install -g nbgv
rem goto end

set builderPath="C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\msbuild.exe"
set outputPath=C:\Temp\AnyRig-x86

if exist "%outputPath%\" rd /q /s "%outputPath%"

%builderPath% ..\AnyRigService\AnyRigService.csproj -t:restore
if not %ERRORLEVEL%==0 goto fail
%builderPath% ..\AnyRigService\AnyRigService.csproj /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=win-x86-self-Profile /p:OutputPath=%outputPath%/service
if not %ERRORLEVEL%==0 goto fail

%builderPath% ..\WinAnyRigConfig\WinAnyRigConfig.csproj -t:restore
if not %ERRORLEVEL%==0 goto fail
%builderPath% ..\WinAnyRigConfig\WinAnyRigConfig.csproj /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=win-x86-self-Profile /p:OutputPath=%outputPath%/gui
if not %ERRORLEVEL%==0 goto fail

%builderPath% ..\AnyRigConfig\AnyRigConfig.csproj -t:restore
if not %ERRORLEVEL%==0 goto fail
%builderPath% ..\AnyRigConfig\AnyRigConfig.csproj /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=win-x86-self-Profile /p:OutputPath=%outputPath%/gui
if not %ERRORLEVEL%==0 goto fail

start /d "%outputPath%\gui\" WinAnyRigConfig.exe
start /d "%outputPath%\gui\" AnyRigConfig.exe
start /d "%outputPath%\service\" AnyRigService.exe

"C:\Program Files (x86)\Inno Setup 6\iscc" "AnyRig-x86.iss"
if not %ERRORLEVEL%==0 goto fail

start /d "C:\Temp\InnoSetup\AnyRig\" AnyRigSetup.exe

goto end

:fail
@echo:
@echo !!!!!!!!!!!!!!!!!!
@echo !!!!!! FAIL !!!!!!
@echo !!!!!!!!!!!!!!!!!!
pause
exit /b 1

:end

@echo:
@echo ------------------
@echo =       OK       =
@echo ------------------
exit /b 0
