# Based on
https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/existing-db?view=aspnetcore-2.2

# Reverse engineer your model from existing database
Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=EfBlogging;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models