﻿#Based on
https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-2.2&tabs=visual-studio
https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-model?view=aspnetcore-3.0&tabs=visual-studio

# Scaffold

Right click on Controllers folder then Add -> New Scaffolded item

# Initial database migration (Package Manager Console)

Add-Migration Initial
Update-Database

# Add Rating
Add-Migration Rating
Update-Database

Check local database in this location. By default, LocalDB database creates "*.mdf" files in the C:/Users/<user> directory.

# Or with command line

dotnet ef migrations add Initial
dotnet ef database update

# See local database

From the View menu, open SQL Server Object Explorer (SSOX).