#Based on
https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/windows-service?view=aspnetcore-3.0

#Publish a Framework-dependent Deployment (FDD)
dotnet publish --configuration Release --output c:\Temp\svcFDD

#Publish a Self-contained Deployment (SCD)
dotnet publish --configuration Release --runtime win7-x64 --output c:\Temp\svcSCD

#Install windows service (PowerShell)
New-Service -Name Host_InWindowsService-Development -BinaryPathName "C:\_Data\GitHub\NetCoreSamples\AspNetCore-2.0\src\Host_InWindowsService\bin\Release\netcoreapp2.2\Host_InWindowsService.exe" -Credential Domain\UserName -Description "Host_InWindowsService-Development" -DisplayName "Host_InWindowsService-Development" -StartupType Automatic