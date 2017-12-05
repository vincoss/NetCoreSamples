Based on
https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/

### Steps

Run database migrations from 'Package Manager Console'

Install-Package Microsoft.EntityFrameworkCore.Tools
Add-Migration Initial
Update-Database

Check local database in this location. By default, LocalDB database creates "*.mdf" files in the C:/Users/<user> directory.

# Or with command line

dotnet ef migrations add Initial
dotnet ef database update