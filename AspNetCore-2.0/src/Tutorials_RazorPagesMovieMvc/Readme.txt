Based on
https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/

# Scaffold

Right click on Controllers folder then Add -> New Scaffolded item

# Initial database migration (Package Manager Console)

Add-Migration Initial
Update-Database

# Add Rating
Add-Migration Rating

Check local database in this location. By default, LocalDB database creates "*.mdf" files in the C:/Users/<user> directory.

# Or with command line

dotnet ef migrations add Initial
dotnet ef database update