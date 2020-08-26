## Resourcess
https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows
https://docs.microsoft.com/en-us/aspnet/core/security/key-vault-configuration?view=aspnetcore-3.1#secret-storage-in-the-production-environment-with-azure-key-vault
https://docs.microsoft.com/fi-fi/azure/key-vault/general/service-to-service-authentication


## App Dev connection string
dotnet user-secrets set "ConnectionStrings:Northwind:Dev" "Server=.;Database=myDataBase;Trusted_Connection=True;"

## Start
dotnet Fundamentals_AzureKeyValut_Samples.dll environment=development