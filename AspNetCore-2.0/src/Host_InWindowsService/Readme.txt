#Based on
https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/windows-service?view=aspnetcore-3.0

#Publish a Framework-dependent Deployment (FDD)
dotnet publish --configuration Release --output c:\Temp\svcFDD

#Publish a Self-contained Deployment (SCD)
dotnet publish --configuration Release --runtime win7-x64 --output c:\Temp\svcSCD