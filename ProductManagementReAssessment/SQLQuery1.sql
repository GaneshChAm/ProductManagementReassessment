Create Database Assessment

use Assessment

Create Table ProductsManagement
(
ProductID int identity primary key, 
ProductName varchar(50),
ProductDescription varchar(1000),
Quantity int,
Price decimal
)

Drop table ProductsManagement

Select * from ProductsManagement