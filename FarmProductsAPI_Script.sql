-- Creating database
CREATE DATABASE FarmProductsDB;
GO

USE FarmProductsDB;
GO

-- Set date format to ensure consistent date parsing
SET DATEFORMAT ymd;
GO

-- Table for Accounts (Unified for Owner, Staff, Customer)
CREATE TABLE Accounts (
    AccountID INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Username VARCHAR(50) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Role TINYINT NOT NULL, -- 1: Owner, 2: Staff, 3: Customer
    PhoneNumber VARCHAR(15),
    Email VARCHAR(100),
    Address NVARCHAR(200),
    CreatedDate DATETIME DEFAULT GETDATE(),
    Status BIT DEFAULT 1 -- 1: Active, 0: Inactive
);
GO

-- Index for Accounts table to optimize login and role-based queries
CREATE NONCLUSTERED INDEX IX_Accounts_Username ON Accounts(Username);
CREATE NONCLUSTERED INDEX IX_Accounts_Role ON Accounts(Role);
CREATE NONCLUSTERED INDEX IX_Accounts_Email ON Accounts(Email) WHERE Email IS NOT NULL;
GO

-- Table for Product Categories
CREATE TABLE ProductCategories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200)
);
GO

-- Index for ProductCategories to optimize category lookups
CREATE NONCLUSTERED INDEX IX_ProductCategories_CategoryName ON ProductCategories(CategoryName);
GO

-- Combined Table for Products (merging Products and Stock, without LastUpdated, UpdatedBy, Notes)
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    CategoryID INT,
    ProductName NVARCHAR(100) NOT NULL,
    Unit NVARCHAR(20) NOT NULL, -- e.g., kg, bag, bunch
    SellingPrice DECIMAL(18,2) NOT NULL,
    Description NVARCHAR(500),
    Quantity INT NOT NULL DEFAULT 0,
    FOREIGN KEY (CategoryID) REFERENCES ProductCategories(CategoryID)
);
GO

-- Index for Products to optimize product searches and joins
CREATE NONCLUSTERED INDEX IX_Products_CategoryID ON Products(CategoryID);
CREATE NONCLUSTERED INDEX IX_Products_ProductName ON Products(ProductName);
GO

-- Table for Orders
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    StaffID INT,
    CustomerID INT NULL, -- Customer can be null for guest purchases
    OrderDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    FOREIGN KEY (StaffID) REFERENCES Accounts(AccountID),
    FOREIGN KEY (CustomerID) REFERENCES Accounts(AccountID)
);
GO

-- Index for Orders to optimize order retrieval and joins
CREATE NONCLUSTERed INDEX IX_Orders_StaffID ON Orders(StaffID);
CREATE NONCLUSTERED INDEX IX_Orders_CustomerID ON Orders(CustomerID) WHERE CustomerID IS NOT NULL;
CREATE NONCLUSTERED INDEX IX_Orders_OrderDate ON Orders(OrderDate);
GO

-- Table for Order Details
CREATE TABLE OrderDetails (
    DetailID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT,
    ProductID INT,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    Total AS (Quantity * UnitPrice), -- Computed column for total
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);
GO

-- Index for OrderDetails to optimize order detail queries
CREATE NONCLUSTERED INDEX IX_OrderDetails_OrderID ON OrderDetails(OrderID);
CREATE NONCLUSTERED INDEX IX_OrderDetails_ProductID ON OrderDetails(ProductID);
GO

-- Table for ImportedStock to track stock import history
CREATE TABLE ImportedStock (
    ImportID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT NOT NULL,
    StockBeforeUpdate INT NOT NULL,
    UpdatedStockQuantity INT NOT NULL,
    StockAfterUpdate INT NOT NULL,
    Notes NVARCHAR(255(innerHTML),
    UpdatedBy INT NOT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    FOREIGN KEY (UpdatedBy) REFERENCES Accounts(AccountID)
);
GO

-- Index to optimize queries on ImportedStock
CREATE NONCLUSTERED INDEX IX_ImportedStock_ProductID ON ImportedStock(ProductID);
CREATE NONCLUSTERED INDEX IX_ImportedStock_UpdatedBy ON ImportedStock(UpdatedBy);
CREATE NONCLUSTERED INDEX IX_ImportedStock_UpdatedAt ON ImportedStock(UpdatedAt);
GO

-- Add check constraints to ensure non-negative stock values
ALTER TABLE ImportedStock
ADD CONSTRAINT CHK_ImportedStock_StockBeforeUpdate CHECK (StockBeforeUpdate >= 0);
ALTER TABLE ImportedStock
ADD CONSTRAINT CHK_ImportedStock_UpdatedStockQuantity CHECK (UpdatedStockQuantity >= 0);
ALTER TABLE ImportedStock
ADD CONSTRAINT CHK_ImportedStock_StockAfterUpdate CHECK (StockAfterUpdate >= 0);
GO

-- Add check constraint to ensure non-negative quantity in Products
ALTER TABLE Products
ADD CONSTRAINT CHK_Products_Quantity CHECK (Quantity >= 0);
GO

-- Sample data for ProductCategories
INSERT INTO ProductCategories (CategoryName, Description) VALUES
('Leafy Vegetables', 'Fresh, organic leafy greens'),
('Root & Fruit Vegetables', 'Nutritious root and fruit vegetables with clear origins'),
('Fruits', 'Seasonal fresh fruits from local farms'),
('Mushrooms', 'High quality fresh mushrooms');
GO

-- Sample data for Products
INSERT INTO Products (CategoryID, ProductName, Unit, SellingPrice, Description, Quantity) VALUES
(1, 'Mustard Greens', 'kg', 25000, 'Fresh mustard greens, organically grown in Lam Dong', 100),
(1, 'SpinPKach', 'kg', 30000, 'Organic spinach, rich in nutrients', 70),
(1, 'Water Spinach', 'kg', 20000, 'Fresh water spinach, grown in Hanoi', 120),
(1, 'Bok Choy', 'kg', 28000, 'Bok choy from Da Lat, crisp and sweet', 80),
(1, 'Lettuce', 'kg', 35000, 'Fresh lettuce, hydroponically grown', 60),
(1, 'Amaranth', 'kg', 22000, 'Red amaranth, organically grown in Hanoi', 90),
(1, 'Malabar Spinach', 'kg', 23000, 'Fresh Malabar spinach, grown in Long An', 100),
(1, 'Kale', 'kg', 26000, 'Fresh kale, grown in Da Lat', 85),
(1, 'Sweet Potato Leaves', 'kg', 24000, 'Fresh sweet potato leaves, organically grown in Lam Dong', 95),
(1, 'Banana Flower', 'kg', 27000, 'Fresh banana flower, grown in Tien Giang', 80),
(2, 'Cabbage', 'kg', 20000, 'Cabbage from Da Lat, crisp and sweet', 80),
(2, 'Carrot', 'kg', 22000, 'Carrots from Da Lat, rich in vitamin A', 90),
(2, 'Broccoli', 'kg', 35000, 'Fresh broccoli, cleanly grown', 60),
(2, 'White Radish', 'kg', 20000, 'Fresh white radish, grown in Hanoi', 85),
(2, 'Pumpkin', 'kg', 25000, 'Butternut pumpkin, sweet and nutty', 65),
(2, 'Potato', 'kg', 18000, 'High quality potatoes from Da Lat', 120),
(2, 'Tomato', 'kg', 30000, 'Tomatoes from Da Lat, naturally ripened', 100),
(2, 'Green Zucchini', 'kg', 21000, 'Fresh green zucchini, grown in Lam Dong', 75),
(2, 'Bell Pepper', 'kg', 40000, 'Bell peppers from Da Lat, multicolored', 50),
(2, 'Beetroot', 'kg', 23000, 'Red beetroot, grown in Da Lat', 70),
(2, 'Okra', 'kg', 28000, 'Fresh okra, grown in Long An', 80),
(2, 'Sweet Potato', 'kg', 19000, 'Japanese sweet potato, sweet and nutty, grown in Lam Dong', 90),
(3, 'Mango', 'kg', 45000, 'Cat Chu mango from Hoa Loc, sweet and fragrant', 50),
(3, 'Orange', 'kg', 35000, 'Juicy oranges from Vinh Long', 120),
(3, 'Banana', 'kg', 22000, 'Naturally ripened bananas from Mekong Delta', 90),
(3, 'Dragon Fruit', 'kg', 30000, 'Dragon fruit from Binh Thuan, white flesh', 70),
(3, 'Watermelon', 'kg', 15000, 'Sweet and juicy watermelon from Long An', 100),
(3, 'Pomelo', 'kg', 40000, 'Nam Roi pomelo from Vinh Long, aromatic', 60),
(3, 'Longan', 'kg', 35000, 'Sweet longan from Hung Yen', 80),
(3, 'Lychee', 'kg', 38000, 'Fresh lychee from Luc Ngan', 55),
(3, 'Plum', 'kg', 32000, 'Sweet-sour plum from Da Lat', 75),
(3, 'Durian', 'kg', 80000, 'Ri6 durian, rich and aromatic', 40),
(3, 'Rambutan', 'kg', 34000, 'Sweet rambutan from Vinh Long', 60),
(3, 'Pineapple', 'kg', 20000, 'Sweet and aromatic pineapple from Tien Giang', 80),
(3, 'Korean Pear', 'kg', 60000, 'Crisp and sweet imported Korean pear', 45),
(3, 'Pink Guava', 'kg', 25000, 'Sweet pink guava from Long An', 70),
(3, 'Sapodilla', 'kg', 30000, 'Sweet and soft sapodilla from Tien Giang', 65),
(4, 'Straw Mushroom', 'kg', 50000, 'Fresh straw mushroom, grown in Long An', 30),
(4, 'Enoki Mushroom', 'pack', 20000, 'Fresh enoki mushroom, 200g', 80),
(4, 'Oyster Mushroom', 'kg', 45000, 'Fresh oyster mushroom, grown in Da Lat', 50),
(4, 'King Oyster Mushroom', 'kg', 60000, 'Nutritious king oyster mushroom, cleanly grown', 45),
(4, 'Reishi Mushroom', 'kg', 80000, 'High quality natural reishi mushroom', 20),
(4, 'Cremini Mushroom', 'kg', 55000, 'Fresh cremini mushroom, grown in Lam Dong', 40),
(4, 'Shiitake Mushroom', 'kg', 70000, 'Fresh shiitake mushroom, grown in Da Lat', 35);
GO

-- Sample data for Accounts
INSERT INTO Accounts (FullName, Username, Password, Role, PhoneNumber, Email, Address) VALUES
('Nguyen Van Chu', 'chu1', 'owner123', 1, '0901234567', 'chu1@email.com', '123 Nguyen Trai, Hanoi'),
('Tran Thi Nhan Vien', 'nhanvien1', 'staff456', 2, '0912345678', 'staff1@email.com', '456 Le Loi, Ho Chi Minh City'),
('Le Van Nhan Vien', 'nhanvien2', 'staff789', 2, '0923456789', 'staff2@email.com', '789 Nguyen Hue, Hue'),
('Pham Thi Khach', 'khach1', 'cust101', 3, '0934567890', 'customer1@email.com', '321 Tran Phu, Da Nang'),
('Hoang Van Khach', 'khach2', 'cust202', 3, '0945678901', 'customer2@email.com', '654 Duong Lang, Hanoi'),
('Nguyen Thi Minh', 'khach3', 'cust303', 3, '0956789012', NULL, '987 Le Dai Hanh, Hanoi'),
('Tran Van Quan Ly', 'chu2', 'owner456', 1, '0967890123', 'owner2@email.com', '111 Pham Van Dong, Da Nang'),
('Le Thi Nhan Vien', 'nhanvien3', 'staff012', 2, '0978901234', 'staff3@email.com', '222 Nguyen Van Cu, Ho Chi Minh City'),
('Hoang Thi Khach', 'khach4', 'cust404', 3, '0989012345', 'customer4@email.com', '333 Dien Bien Phu, Hue'),
('Phan Van Khach', 'khach5', 'cust505', 3, '0990123456', NULL, '444 Hung Vuong, Da Lat'),
('Do Thi Khach', 'khach6', 'cust606', 3, '0902345678', 'customer6@email.com', '555 Tran Hung Dao, Hanoi'),
('Vu Van Nhan Vien', 'nhanvien4', 'staff345', 2, '0913456789', 'staff4@email.com', '666 Nguyen Dinh Chieu, Ho Chi Minh City');
GO

-- Sample data for Orders (original 10 + 20 new orders)
INSERT INTO Orders (StaffID, CustomerID, OrderDate, TotalAmount) VALUES
-- Original 10 orders
(2, 4, '20250720 10:30:00', 105000),
(3, NULL, '20250720 14:15:00', 58000),
(8, 5, '20250721 09:00:00', 125000),
(12, 9, '20250721 16:45:00', 66000),
(2, 11, '20250722 11:20:00', 100000),
(2, 6, '20250721 12:30:00', 96000),
(3, NULL, '20250721 15:45:00', 51000),
(8, 10, '20250722 08:00:00', 128000),
(12, 4, '20250722 10:15:00', 103000),
(2, 9, '20250722 14:00:00', 105000),
-- Additional 20 orders
(2, 4, '20250801 09:15:00', 75000),   -- OrderID 11
(3, NULL, '20250801 14:30:00', 48000), -- OrderID 12
(8, 5, '20250802 10:00:00', 110000),  -- OrderID 13
(12, 9, '20250802 16:00:00', 82000),  -- OrderID 14
(2, 11, '20250803 11:45:00', 90000),  -- OrderID 15
(2, 6, '20250803 13:20:00', 65000),   -- OrderID 16
(3, NULL, '20250804 15:10:00', 55000), -- OrderID 17
(8, 10, '20250804 08:30:00', 120000), -- OrderID 18
(12, 4, '20250805 10:45:00', 95000),  -- OrderID 19
(2, 9, '20250805 14:15:00', 70000),   -- OrderID 20
(3, 5, '20250806 09:00:00', 85000),   -- OrderID 21
(8, NULL, '20250806 12:30:00', 60000), -- OrderID 22
(12, 11, '20250807 11:00:00', 105000),-- OrderID 23
(2, 6, '20250807 15:50:00', 78000),   -- OrderID 24
(3, 10, '20250808 10:20:00', 92000),  -- OrderID 25
(8, 4, '20250808 14:00:00', 67000),   -- OrderID 26
(12, NULL, '20250809 09:30:00', 53000),-- OrderID 27
(2, 9, '20250809 13:15:00', 115000),  -- OrderID 28
(3, 5, '20250810 11:40:00', 88000),   -- OrderID 29
(8, 11, '20250810 15:00:00', 72000);  -- OrderID 30
GO

-- Sample data for OrderDetails (for original 10 + 20 new orders)
INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice) VALUES
-- Original 10 orders
(1, 1, 2, 25000),  -- Mustard Greens: 2kg * 25000 = 50000
(1, 3, 1, 20000),  -- Water Spinach: 1kg * 20000 = 20000
(1, 5, 1, 35000),  -- Lettuce: 1kg * 35000 = 35000
(2, 2, 1, 30000),  -- Spinach: 1kg * 30000 = 30000
(2, 4, 1, 28000),  -- Bok Choy: 1kg * 28000 = 28000
(3, 23, 2, 45000), -- Mango: 2kg * 45000 = 90000
(3, 24, 1, 35000), -- Orange: 1kg * 35000 = 35000
(4, 11, 2, 20000), -- Cabbage: 2kg * 20000 = 40000
(4, 12, 1, 22000), -- Carrot: 1kg * 22000 = 22000
(5, 32, 1, 80000), -- Durian: 1kg * 80000 = 80000
(5, 34, 1, 20000), -- Pineapple: 1kg * 20000 = 20000
(6, 7, 1, 23000),  -- Malabar Spinach: 1kg * 23000 = 23000
(6, 9, 2, 24000),  -- Sweet Potato Leaves: 2kg * 24000 = 48000
(6, 15, 1, 25000), -- Pumpkin: 1kg * 25000 = 25000
(7, 17, 1, 30000), -- Tomato: 1kg * 30000 = 30000
(7, 18, 1, 21000), -- Green Zucchini: 1kg * 21000 = 21000
(8, 29, 2, 35000), -- Longan: 2kg * 35000 = 70000
(8, 30, 1, 38000), -- Lychee: 1kg * 38000 = 38000
(8, 39, 1, 20000), -- Enoki Mushroom: 1pack * 20000 = 20000
(9, 13, 1, 35000), -- Broccoli: 1kg * 35000 = 35000
(9, 19, 1, 40000), -- Bell Pepper: 1kg * 40000 = 40000
(9, 21, 1, 28000), -- Okra: 1kg * 28000 = 28000
(10, 40, 1, 45000),-- Oyster Mushroom: 1kg * 45000 = 45000
(10, 41, 1, 60000),-- King Oyster Mushroom: 1kg * 60000 = 60000
-- Additional 20 orders
(11, 1, 2, 25000), -- Mustard Greens: 2kg * 25000 = 50000
(11, 2, 1, 25000), -- Spinach: 1kg * 25000 = 25000
(12, 3, 1, 20000), -- Water Spinach: 1kg * 20000 = 20000
(12, 4, 1, 28000), -- Bok Choy: 1kg * 28000 = 28000
(13, 23, 1, 45000),-- Mango: 1kg * 45000 = 45000
(13, 24, 2, 32500),-- Orange: 2kg * 32500 = 65000
(14, 11, 3, 20000),-- Cabbage: 3kg * 20000 = 60000
(14, 12, 1, 22000),-- Carrot: 1kg * 22000 = 22000
(15, 32, 1, 80000),-- Durian: 1kg * 80000 = 80000
(15, 34, 1, 10000),-- Pineapple: 1kg * 10000 = 10000
(16, 7, 2, 23000), -- Malabar Spinach: 2kg * 23000 = 46000
(16, 9, 1, 19000), -- Sweet Potato Leaves: 1kg * 19000 = 19000
(17, 17, 1, 30000),-- Tomato: 1kg * 30000 = 30000
(17, 18, 1, 25000),-- Green Zucchini: 1kg * 25000 = 25000
(18, 29, 2, 35000),-- Longan: 2kg * 35000 = 70000
(18, 30, 1, 50000),-- Lychee: 1kg * 50000 = 50000
(19, 13, 1, 35000),-- Broccoli: 1kg * 35000 = 35000
(19, 19, 1, 40000),-- Bell Pepper: 1kg * 40000 = 40000
(19, 21, 1, 20000),-- Okra: 1kg * 20000 = 20000
(20, 5, 1, 35000), -- Lettuce: 1kg * 35000 = 35000
(20, 6, 1, 35000), -- Am работают: 1kg * 35000 = 35000
(21, 25, 2, 22000),-- Banana: 2kg * 22000 = 44000
(21, 26, 1, 41000),-- Dragon Fruit: 1kg * 41000 = 41000
(22, 15, 1, 25000),-- Pumpkin: 1kg * 25000 = 25000
(22, 16, 2, 17500),-- Potato: 2kg * 17500 = 35000
(23, 31, 2, 32000),-- Plum: 2kg * 32000 = 64000
(23, 33, 1, 41000),-- Rambutan: 1kg * 41000 = 41000
(24, 8, 1, 26000), -- Kale: 1kg * 26000 = 26000
(24, 10, 2, 26000),-- Banana Flower: 2kg * 26000 = 52000
(25, 27, 1, 15000),-- Watermelon: 1kg * 15000 = 15000
(25, 28, 2, 38500),-- Pomelo: 2kg * 38500 = 77000
(26, 14, 1, 20000),-- White Radish: 1kg * 20000 = 20000
(26, 20, 2, 23500),-- Beetroot: 2kg * 23500 = 47000
(27, 39, 1, 20000),-- Enoki Mushroom: 1pack * 20000 = 20000
(27, 40, 1, 33000),-- Oyster Mushroom: 1kg * 33000 = 33000
(28, 41, 1, 60000),-- King Oyster Mushroom: 1kg * 60000 = 60000
(28, 42, 1, 55000),-- Reishi Mushroom: 1kg * 55000 = 55000
(29, 43, 2, 27500),-- Cremini Mushroom: 2kg * 27500 = 55000
(29, 44, 1, 33000),-- Shiitake Mushroom: 1kg * 33000 = 33000
(30, 22, 1, 19000),-- Sweet Potato: 1kg * 19000 = 19000
(30, 35, 2, 26500);-- Korean Pear: 2kg * 26500 = 53000
GO

-- Sample data for ImportedStock
INSERT INTO ImportedStock (ProductID, StockBeforeUpdate, UpdatedStockQuantity, StockAfterUpdate, Notes, UpdatedBy, UpdatedAt) VALUES
(1, 100, 50, 150, 'Imported additional mustard greens from Lam Dong', 1, '20250720 08:00:00'),
(2, 70, 30, 100, 'Restocked spinach from organic farm', 7, '20250720 09:30:00'),
(3, 120, 40, 160, 'Imported water spinach from Hanoi', 1, '20250720 10:00:00'),
(4, 80, 20, 100, 'Restocked bok choy from Da Lat', 7, '20250720 10:30:00'),
(5, 60, 40, 100, 'Imported lettuce from hydroponic farm', 1, '20250721 10:15:00'),
(6, 90, 30, 120, 'Restocked amaranth from Hanoi', 7, '20250721 10:45:00'),
(7, 100, 50, 150, 'Imported Malabar spinach from Long An', 1, '20250721 11:00:00'),
(8, 85, 25, 110, 'Restocked kale from Da Lat', 7, '20250721 11:30:00'),
(9, 95, 35, 130, 'Imported sweet potato leaves from Lam Dong', 1, '20250721 12:00:00'),
(10, 80, 20, 100, 'Restocked banana flower from Tien Giang', 7, '20250721 12:30:00'),
(11, 80, 20, 100, 'Restocked cabbage from Da Lat supplier', 1, '20250722 07:00:00'),
(12, 90, 30, 120, 'Imported carrots from Da Lat', 7, '20250722 07:30:00'),
(13, 60, 40, 100, 'Restocked broccoli from supplier', 1, '20250722 08:00:00'),
(14, 85, 25, 110, 'Imported white radish from Hanoi', 7, '20250722 08:30:00'),
(15, 65, 35, 100, 'Restocked pumpkin from supplier', 1, '20250722 09:00:00'),
(16, 120, 30, 150, 'Imported potatoes from Da Lat', 7, '20250722 09:30:00'),
(17, 100, 50, 150, 'Restocked tomatoes from Da Lat', 1, '20250722 10:00:00'),
(18, 75, 25, 100, 'Imported green zucchini from Lam Dong', 7, '20250722 10:30:00'),
(19, 50, 30, 80, 'Restocked bell peppers from Da Lat', 1, '20250722 11:00:00'),
(20, 70, 20, 90, 'Imported beetroot from Da Lat', 7, '20250722 11:30:00'),
(21, 80, 25, 105, 'Restocked okra from Long An', 1, '20250722 12:00:00'),
(22, 90, 30, 120, 'Imported sweet potatoes from Lam Dong', 7, '20250722 12:30:00'),
(23, 50, 25, 75, 'Restocked mangoes from Hoa Loc', 1, '20250723 07:00:00'),
(24, 120, 40, 160, 'Imported oranges from Vinh Long', 7, '20250723 07:30:00'),
(25, 90, 30, 120, 'Restocked bananas from Mekong Delta', 1, '20250723 08:00:00'),
(26, 70, 20, 90, 'Imported dragon fruit from Binh Thuan', 7, '20250723 08:30:00'),
(27, 100, 50, 150, 'Restocked watermelon from Long An', 1, '20250723 09:00:00'),
(28, 60, 30, 90, 'Imported pomelo from Vinh Long', 7, '20250723 09:30:00'),
(29, 80, 20, 100, 'Restocked longan from Hung Yen', 1, '20250723 10:00:00'),
(30, 55, 25, 80, 'Imported lychee from Luc Ngan', 7, '20250723 10:30:00'),
(31, 75, 30, 105, 'Restocked plums from Da Lat', 1, '20250723 11:00:00'),
(32, 40, 10, 50, 'Imported durian from supplier', 7, '20250723 11:30:00'),
(33, 60, 20, 80, 'Restocked rambutan from Vinh Long', 1, '20250723 12:00:00'),
(34, 80, 30, 110, 'Imported pineapple from Tien Giang', 7, '20250723 12:30:00'),
(35, 45, 15, 60, 'Restocked Korean pear from imported stock', 1, '20250724 07:00:00'),
(36, 70, 20, 90, 'Imported pink guava from Long An', 7, '20250724 07:30:00'),
(37, 65, 25, 90, 'Restocked sapodilla from Tien Giang', 1, '20250724 08:00:00'),
(38, 30, 20, 50, 'Imported straw mushroom from Long An', 7, '20250724 08:30:00'),
(39, 80, 20, 100, 'Restocked enoki mushroom from supplier', 1, '20250724 09:00:00'),
(40, 50, 30, 80, 'Imported oyster mushroom from Da Lat', 7, '20250724 09:30:00'),
(41, 45, 15, 60, 'Restocked king oyster mushroom from supplier', 1, '20250724 10:00:00'),
(42, 20, 10, 30, 'Imported reishi mushroom from farm', 7, '20250724 10:30:00'),
(43, 40, 20, 60, 'Restocked cremini mushroom from Lam Dong', 1, '20250724 11:00:00'),
(44, 35, 15, 50, 'Imported shiitake mushroom from Da Lat', 7, '20250724 11:30:00');
GO