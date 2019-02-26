# Based on 
https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/?view=aspnetcore-2.2

# Scaffolding
Right click on the Students folder and Add New Scaffolding item

# Migrations

-Drop the database
Drop-Database

-Create an initial migration and update the DB
Add-Migration InitialCreate
Update-Database

-The data model snapshot
Remove-Migration

- ColumnFirstName update
Add-Migration ColumnFirstName
Update-Database

-Complex migration (upgrade)
Add-Migration ComplexDataModel
Update-Database

-Complex migratio (emtpy)
Drop-Database
Update-Database