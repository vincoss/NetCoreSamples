# Based on
https://damienbod.com/2018/10/12/odata-with-asp-net-core/


# Scaffolding
https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/existing-db
Reverse the models from existing database
Scaffold-DbContext "Server=BNELTP009\MSSQLSERVER2016;Database=AdventureWorks2014;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

# Install OData NuGet package
Microsoft.AspNetCore.OData