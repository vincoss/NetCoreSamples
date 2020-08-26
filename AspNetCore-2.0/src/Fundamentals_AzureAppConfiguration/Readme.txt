## Resourcess
https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows
https://docs.microsoft.com/en-us/azure/azure-app-configuration/quickstart-aspnet-core-app?tabs=core3x

## Add secreets (Connect to azure)
dotnet user-secrets set ConnectionStrings:AppConfig "Get from Azure - Access Keys - Read-Only keys - Connection String"

## App Dev connectin string
dotnet user-secrets set "ConnectionStrings--Northwind--Dev" "Server=.;Database=myDataBase;Trusted_Connection=True;"

## Run
Fundamentals_AzureAppConfiguration environment=development