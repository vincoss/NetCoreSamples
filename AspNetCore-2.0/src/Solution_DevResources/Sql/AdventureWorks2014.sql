SELECT
ProductID,p.Name,ProductNumber,MakeFlag,FinishedGoodsFlag,ISNULL(Color, ''),SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,ISNULL(Size, ''), 
ISNULL(s.Name, '') AS ProductSubcategory, 
ISNULL(c.Name, '') AS ProductCategory
FROM Production.Product p
LEFT JOIN Production.ProductSubcategory s ON s.ProductSubcategoryID = p.ProductSubcategoryID
LEFT JOIN Production.ProductCategory c ON c.ProductCategoryID = s.ProductCategoryID
