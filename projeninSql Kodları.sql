use Erensoft;

CREATE TABLE Users(
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL,
    Role NVARCHAR(20) NOT NULL
);

INSERT INTO Users(UserName, Password, Role)
VALUES ('admin', '12345', 'Admin');

-------------------------


USE Erensoft;

CREATE TABLE Items(
    Id INT PRIMARY KEY IDENTITY(1,1),
    ItemCode NVARCHAR(50) NOT NULL,
    ItemName NVARCHAR(100) NOT NULL,
    Category NVARCHAR(50),
    UnitPrice DECIMAL(18,2) NOT NULL

);

ALTER TABLE Items
ADD PurchasePrice DECIMAL(18,2) DEFAULT 0,
    SalePrice DECIMAL(18,2) DEFAULT 0;

INSERT INTO Items(ItemCode, ItemName, Category, UnitPrice)
VALUES
('U001', 'Kalem', 'Kırtasiye', 15.50),
('U002', 'Defter', 'Kırtasiye', 45.00);

-------------------------

CREATE TABLE WareHouses(
    Id INT PRIMARY KEY IDENTITY(1,1),
    WarehouseName NVARCHAR(100) NOT NULL,
    Address NVARCHAR(200)
);

INSERT INTO WareHouses(WarehouseName, Address)
VALUES
('Ana Depo', 'İstanbul'),
('Şube Depo', 'Ankara');

-------------------------

USE Erensoft;

CREATE TABLE Stocks(
    Id INT PRIMARY KEY IDENTITY(1,1),
    ItemId INT NOT NULL,
    WarehouseId INT NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (ItemId) REFERENCES Items(Id),
    FOREIGN KEY (WarehouseId) REFERENCES WareHouses(Id)
);

INSERT INTO Stocks(ItemId, WarehouseId, Quantity)
VALUES
(1, 1, 50),
(2, 1, 30);

-------------------------

CREATE TABLE Invoices(
    Id INT PRIMARY KEY IDENTITY(1,1),
    ItemId INT NOT NULL,
    Quantity INT NOT NULL,
    InvoiceType NVARCHAR(20) NOT NULL,
    InvoiceDate DATETIME NOT NULL,
    FOREIGN KEY (ItemId) REFERENCES Items(Id)
);
ALTER TABLE Invoices
ADD WarehouseId INT NULL;



INSERT INTO Invoices(ItemId, Quantity, InvoiceType, InvoiceDate)
VALUES
(1, 10, 'Giriş', GETDATE()),
(2, 5, 'Çıkış', GETDATE());


-------------------------

INSERT INTO Users(UserName, Password, Role)
VALUES ('user', '12345', 'User');

ALTER TABLE Invoices
ADD CustomerName NVARCHAR(100),
    UnitPrice DECIMAL(18,2),
    TotalPrice DECIMAL(18,2);
