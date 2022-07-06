; https://jrsoftware.org/ishelp/
#define ApplicationVersion GetStringFileInfo('C:\Temp\AnyRig-x86\gui\WinAnyRigConfig.exe', 'ProductVersion')

[Setup]
AppName=AnyRig
AppVersion={#ApplicationVersion}
WizardStyle=modern
DefaultDirName={autopf}\IW1QLH\AnyRig
DefaultGroupName=AnyRig
UninstallDisplayIcon={app}\gui\WinAnyRigConfig.exe
Compression=lzma2
SolidCompression=yes
OutputDir=C:\Temp\InnoSetup\AnyRig
OutputBaseFilename=AnyRigSetup
SetupIconFile=C:\Program Files (x86)\Inno Setup 6\SetupClassicIcon.ico
PrivilegesRequired=admin
LicenseFile=license.txt
AppPublisher=IW1QLH
AppPublisherURL=http://www.iw1qlh.net/

[Components]
Name: "main"; Description: "Main files"; Types: full compact custom; Flags: fixed
Name: "anyrigservice"; Description: "Create AnyRig CAT service"; Types: full;

[Files]
Source: "C:\Temp\AnyRig-x86\service\*"; DestDir: "{app}\service"; Flags: ignoreversion recursesubdirs
Source: "C:\Temp\AnyRig-x86\gui\*"; DestDir: "{app}\gui"; Flags: ignoreversion recursesubdirs

[Icons]
Name: "{group}\AnyRig Config (main)"; Filename: "{app}\gui\WinAnyRigConfig.exe"
Name: "{group}\AnyRig Config (terminal)"; Filename: "{app}\gui\AnyRigConfig.exe";
Name: "{group}\Uninstall"; Filename: "{uninstallexe}"

[CustomMessages]
LaunchProgram=Start "AnyRig Config" after finishing installation

[run]
Filename: {sys}\sc.exe; Parameters: "create AnyRigService DisplayName= ""AnyRig CAT control"" start= auto binPath= ""{app}\service\AnyRigService.exe""" ; Components: anyrigservice; Flags: runhidden runascurrentuser
Filename: {sys}\sc.exe; Parameters: "description AnyRigService ""Provides connection to Rig"""; Components: anyrigservice; Flags: runhidden runascurrentuser
Filename: {sys}\sc.exe; Parameters: "start AnyRigService"; Components: anyrigservice; Flags: runhidden runascurrentuser
Filename: {app}\gui\WinAnyRigConfig.exe; Description: {cm:LaunchProgram,WinAnyRigConfig}; Flags: nowait postinstall skipifsilent

[UninstallRun]
Filename: {sys}\sc.exe; Parameters: "stop AnyRigService" ; RunOnceId: "StopService"; Flags: runhidden runascurrentuser
Filename: {sys}\sc.exe; Parameters: "delete AnyRigService" ; RunOnceId: "DeleteService"; Flags: runhidden runascurrentuser
