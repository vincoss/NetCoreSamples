# Based on
https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/?view=aspnetcore-2.2

# Scaffolding with CLI

dotnet ef migrations add InitialCreate
dotnet ef database update

-drop the migration
dotnet ef migrations remove

- add migration
dotnet ef migrations add MaxLengthOnNames
dotnet ef migrations add ColumnFirstName
dotnet ef database update

- add migration
dotnet ef migrations add ComplexDataModel
dotnet ef database update

-add migration
dotnet ef migrations add Inheritance
dotnet ef database update