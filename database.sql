create database CoffeeShopDB1;
go
use CoffeeShopDB1;
go
-- Bảng Products
CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    Detail NVARCHAR(MAX),
    ImageUrl NVARCHAR(255),
    Price DECIMAL(18,2) NOT NULL,
    IsTrendingProduct BIT NOT NULL
);

-- Bảng Orders
CREATE TABLE Orders (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Email NVARCHAR(100),
    Phone NVARCHAR(20),
    Address NVARCHAR(255),
    OrderTotal DECIMAL(18,2) NOT NULL,
    OrderPlaced DATETIME NOT NULL
);

-- Bảng OrderDetails
CREATE TABLE OrderDetails (
    OrderDetailId INT PRIMARY KEY IDENTITY(1,1),
    ProductId INT NOT NULL,
    OrderId INT NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Products(Id),
    FOREIGN KEY (OrderId) REFERENCES Orders(Id)
);

-- Bảng ShoppingCartItems
CREATE TABLE ShoppingCartItems (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProductId INT,
    Qty INT NOT NULL,
    ShoppingCartId NVARCHAR(100),
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);
