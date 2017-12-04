Based on
https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/

### Steps

Run database migrations from 'Package Manager Console'

Add-Migration Initial
Update-Database

Check local database in this location. By default, LocalDB database creates "*.mdf" files in the C:/Users/<user> directory.

# Generate scaffolding templates

https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/model

run cmd and command below

dotnet aspnet-codegenerator razorpage -m Movie -dc MovieContext -udl -outDir Pages\Movies --referenceScriptLibraries

# Add migration for rating column from 'Package Manager Console'
Possible you have to Truncate TABLE Movies

Add-Migration Rating
Update-Database