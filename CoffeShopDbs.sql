create database CoffeeShopDbs
go 
use CoffeeShopDbs
go

CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY, -- Tự tăng và khóa chính
    Name NVARCHAR(255) NULL,
    Detail NVARCHAR(MAX) NULL,
    ImageUrl NVARCHAR(255) NULL,
    Price DECIMAL(18, 2) NOT NULL,
    IsTrendingProduct BIT NOT NULL
);