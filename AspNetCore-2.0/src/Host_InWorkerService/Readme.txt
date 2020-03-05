#Based on
https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/windows-service?view=aspnetcore-3.1&tabs=visual-studio
aspnet\AspNetCore.Docs\aspnetcore\host-and-deploy\windows-service\samples\3.x\WebAppServiceSample

#Publish a Framework-dependent Deployment (FDD)
dotnet publish --configuration Release --output c:\Temp\svcFDD

#Publish a Self-contained Deployment (SCD)
dotnet publish --configuration Release --runtime win-x64 --output c:\Temp\svcSCD

## Create new service user account
New-LocalUser -Name SvcRunUser

## Create and manage the Windows Service
$acl = Get-Acl "C:\_Data\GitHub\NetCoreSamples\AspNetCore-2.0\src\Host_InWorkerService\bin\Release\netcoreapp3.1"
$aclRuleArgs = "MachineName\SvcRunUser", "Read,Write,ReadAndExecute", "ContainerInherit,ObjectInherit", "None", "Allow"
$accessRule = New-Object System.Security.AccessControl.FileSystemAccessRule($aclRuleArgs)
$acl.SetAccessRule($accessRule)
$acl | Set-Acl "C:\_Data\GitHub\NetCoreSamples\AspNetCore-2.0\src\Host_InWorkerService\bin\Release\netcoreapp3.1\Host_InWorkerService.exe"

New-Service -Name Host_InWorkerService -BinaryPathName "C:\_Data\GitHub\NetCoreSamples\AspNetCore-2.0\src\Host_InWorkerService\bin\Release\netcoreapp3.1\Host_InWorkerService.exe" -Credential MachineName\SvcRunUser -Description "Host_InWorkerService" -DisplayName "Host_InWorkerService" -StartupType Automatic

## Start service
Start-Service -Name Host_InWorkerService

## Check service
Get-Service -Name Host_InWorkerService

#Stop service
Stop-Service -Name Host_InWorkerService

## Remove service
Remove-Service -Name Host_InWorkerService
SC DELETE Host_InWorkerService

