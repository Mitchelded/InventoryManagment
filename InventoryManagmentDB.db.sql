BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Category" (
    "CategoryID" INTEGER NOT NULL CONSTRAINT "PK_Category" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Description" TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS "Departments" (
    "DepartmentID" INTEGER NOT NULL CONSTRAINT "PK_Departments" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "HeadOfDepartment" TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS "EquipmentMovements" (
    "EquipmentMovementID" INTEGER NOT NULL CONSTRAINT "PK_EquipmentMovements" PRIMARY KEY AUTOINCREMENT,
    "EquipmentID" INTEGER NOT NULL,
    "SourceWarehouseID" INTEGER NOT NULL,
    "DestinationWarehouseID" INTEGER NOT NULL,
    "UserID" INTEGER NULL,
    "MovementDate" TEXT NOT NULL,
    "MovementType" TEXT NOT NULL,
    "Quantity" INTEGER NOT NULL,
    "Notes" TEXT NOT NULL,
    CONSTRAINT "FK_EquipmentMovements_Equipments_EquipmentID" FOREIGN KEY ("EquipmentID") REFERENCES "Equipments" ("EquipmentID") ON DELETE CASCADE,
    CONSTRAINT "FK_EquipmentMovements_Users_UserID" FOREIGN KEY ("UserID") REFERENCES "Users" ("UserID"),
    CONSTRAINT "FK_EquipmentMovements_Warehouses_DestinationWarehouseID" FOREIGN KEY ("DestinationWarehouseID") REFERENCES "Warehouses" ("WarehouseID") ON DELETE CASCADE,
    CONSTRAINT "FK_EquipmentMovements_Warehouses_SourceWarehouseID" FOREIGN KEY ("SourceWarehouseID") REFERENCES "Warehouses" ("WarehouseID") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "Equipments" (
    "EquipmentID" INTEGER NOT NULL CONSTRAINT "PK_Equipments" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Photo" BLOB NOT NULL,
    "CategoryID" INTEGER NOT NULL,
    "Cost" TEXT NULL,
    "SerialNumber" TEXT NOT NULL,
    "PurchaseDate" TEXT NULL,
    "WarrantyExpiration" TEXT NULL,
    "StatusID" INTEGER NOT NULL,
    "SupplierID" INTEGER NOT NULL,
    CONSTRAINT "FK_Equipments_Category_CategoryID" FOREIGN KEY ("CategoryID") REFERENCES "Category" ("CategoryID") ON DELETE CASCADE,
    CONSTRAINT "FK_Equipments_Statuses_StatusID" FOREIGN KEY ("StatusID") REFERENCES "Statuses" ("StatusID") ON DELETE CASCADE,
    CONSTRAINT "FK_Equipments_Suppliers_SupplierID" FOREIGN KEY ("SupplierID") REFERENCES "Suppliers" ("SupplierID") ON DELETE RESTRICT
);
CREATE TABLE IF NOT EXISTS "Maintenances" (
    "MaintenanceID" INTEGER NOT NULL CONSTRAINT "PK_Maintenances" PRIMARY KEY AUTOINCREMENT,
    "EquipmentID" INTEGER NOT NULL,
    "MaintenanceDate" TEXT NOT NULL,
    "Notes" TEXT NOT NULL,
    CONSTRAINT "FK_Maintenances_Equipments_EquipmentID" FOREIGN KEY ("EquipmentID") REFERENCES "Equipments" ("EquipmentID") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "Roles" (
    "RoleID" INTEGER NOT NULL CONSTRAINT "PK_Roles" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Description" TEXT NULL
);
CREATE TABLE IF NOT EXISTS "Statuses" (
    "StatusID" INTEGER NOT NULL CONSTRAINT "PK_Statuses" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Description" TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS "Stocks" (
    "StockID" INTEGER NOT NULL CONSTRAINT "PK_Stocks" PRIMARY KEY AUTOINCREMENT,
    "EquipmentID" INTEGER NOT NULL,
    "WarehouseID" INTEGER NOT NULL,
    "Quantity" INTEGER NOT NULL,
    CONSTRAINT "FK_Stocks_Equipments_EquipmentID" FOREIGN KEY ("EquipmentID") REFERENCES "Equipments" ("EquipmentID") ON DELETE CASCADE,
    CONSTRAINT "FK_Stocks_Warehouses_WarehouseID" FOREIGN KEY ("WarehouseID") REFERENCES "Warehouses" ("WarehouseID") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "Suppliers" (
    "SupplierID" INTEGER NOT NULL CONSTRAINT "PK_Suppliers" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "ContactInfo" TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS "Transactions" (
    "TransactionID" INTEGER NOT NULL CONSTRAINT "PK_Transactions" PRIMARY KEY AUTOINCREMENT,
    "EquipmentID" INTEGER NOT NULL,
    "WarehouseID" INTEGER NOT NULL,
    "UserID" INTEGER NULL,
    "Quantity" INTEGER NOT NULL,
    "TransactionType" TEXT NOT NULL,
    "TransactionDate" TEXT NOT NULL,
    CONSTRAINT "FK_Transactions_Equipments_EquipmentID" FOREIGN KEY ("EquipmentID") REFERENCES "Equipments" ("EquipmentID") ON DELETE CASCADE,
    CONSTRAINT "FK_Transactions_Users_UserID" FOREIGN KEY ("UserID") REFERENCES "Users" ("UserID"),
    CONSTRAINT "FK_Transactions_Warehouses_WarehouseID" FOREIGN KEY ("WarehouseID") REFERENCES "Warehouses" ("WarehouseID") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "UserRoles" (
    "UserID" INTEGER NOT NULL,
    "RoleID" INTEGER NOT NULL,
    "UserRoleID" INTEGER NOT NULL,
    CONSTRAINT "PK_UserRoles" PRIMARY KEY ("UserID", "RoleID"),
    CONSTRAINT "FK_UserRoles_Roles_RoleID" FOREIGN KEY ("RoleID") REFERENCES "Roles" ("RoleID") ON DELETE CASCADE,
    CONSTRAINT "FK_UserRoles_Users_UserID" FOREIGN KEY ("UserID") REFERENCES "Users" ("UserID") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "Users" (
    "UserID" INTEGER NOT NULL CONSTRAINT "PK_Users" PRIMARY KEY AUTOINCREMENT,
    "Username" TEXT NOT NULL,
    "Password" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "CreatedAt" TEXT NOT NULL,
    "DepartmentID" INTEGER NULL,
    "FullName" TEXT NOT NULL,
    CONSTRAINT "FK_Users_Departments_DepartmentID" FOREIGN KEY ("DepartmentID") REFERENCES "Departments" ("DepartmentID")
);
CREATE TABLE IF NOT EXISTS "UtilizationRecords" (
    "UtilizationRecordID" INTEGER NOT NULL CONSTRAINT "PK_UtilizationRecords" PRIMARY KEY AUTOINCREMENT,
    "EquipmentID" INTEGER NOT NULL,
    "UserID" INTEGER NOT NULL,
    "StartDate" TEXT NOT NULL,
    "EndDate" TEXT NULL,
    "Purpose" TEXT NOT NULL,
    "Notes" TEXT NOT NULL,
    CONSTRAINT "FK_UtilizationRecords_Equipments_EquipmentID" FOREIGN KEY ("EquipmentID") REFERENCES "Equipments" ("EquipmentID") ON DELETE CASCADE,
    CONSTRAINT "FK_UtilizationRecords_Users_UserID" FOREIGN KEY ("UserID") REFERENCES "Users" ("UserID") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "Warehouses" (
    "WarehouseID" INTEGER NOT NULL CONSTRAINT "PK_Warehouses" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Location" TEXT NOT NULL
);
INSERT INTO "Category" VALUES (1,'Electronics','Electronic devices and components');
INSERT INTO "Category" VALUES (2,'Furniture','Office and home furniture');
INSERT INTO "Category" VALUES (3,'Tools','Manual and power tools');
INSERT INTO "Category" VALUES (4,'Vehicles','Company vehicles and transport');
INSERT INTO "Category" VALUES (5,'IT Equipment','Computers and peripherals');
INSERT INTO "Category" VALUES (6,'Electronics','Electronic devices and components');
INSERT INTO "Category" VALUES (7,'Furniture','Office and home furniture');
INSERT INTO "Category" VALUES (8,'Tools','Manual and power tools');
INSERT INTO "Category" VALUES (9,'Vehicles','Company vehicles and transport');
INSERT INTO "Category" VALUES (10,'IT Equipment','Computers and peripherals');
INSERT INTO "Category" VALUES (11,'Electronics','Electronic devices and components');
INSERT INTO "Category" VALUES (12,'Furniture','Office and home furniture');
INSERT INTO "Category" VALUES (13,'Tools','Manual and power tools');
INSERT INTO "Category" VALUES (14,'Vehicles','Company vehicles and transport');
INSERT INTO "Category" VALUES (15,'IT Equipment','Computers and peripherals');
INSERT INTO "Category" VALUES (16,'Electronics','Electronic devices and components');
INSERT INTO "Category" VALUES (17,'Furniture','Office and home furniture');
INSERT INTO "Category" VALUES (18,'Tools','Manual and power tools');
INSERT INTO "Category" VALUES (19,'Vehicles','Company vehicles and transport');
INSERT INTO "Category" VALUES (20,'IT Equipment','Computers and peripherals');
INSERT INTO "Category" VALUES (21,'Electronics','Electronic devices and components');
INSERT INTO "Category" VALUES (22,'Furniture','Office and home furniture');
INSERT INTO "Category" VALUES (23,'Tools','Manual and power tools');
INSERT INTO "Category" VALUES (24,'Vehicles','Company vehicles and transport');
INSERT INTO "Category" VALUES (25,'IT Equipment','Computers and peripherals');
INSERT INTO "Category" VALUES (26,'Electronics','Electronic devices and components');
INSERT INTO "Category" VALUES (27,'Furniture','Office and home furniture');
INSERT INTO "Category" VALUES (28,'Tools','Manual and power tools');
INSERT INTO "Category" VALUES (29,'Vehicles','Company vehicles and transport');
INSERT INTO "Category" VALUES (30,'IT Equipment','Computers and peripherals');
INSERT INTO "Departments" VALUES (1,'IT','John Doe');
INSERT INTO "Departments" VALUES (2,'HR','Jane Smith');
INSERT INTO "Departments" VALUES (3,'Logistics','Michael Johnson');
INSERT INTO "Departments" VALUES (4,'Finance','Emily Davis');
INSERT INTO "Departments" VALUES (5,'Marketing','William Brown');
INSERT INTO "Departments" VALUES (6,'IT','John Doe');
INSERT INTO "Departments" VALUES (7,'HR','Jane Smith');
INSERT INTO "Departments" VALUES (8,'Logistics','Michael Johnson');
INSERT INTO "Departments" VALUES (9,'Finance','Emily Davis');
INSERT INTO "Departments" VALUES (10,'Marketing','William Brown');
INSERT INTO "Departments" VALUES (11,'IT','John Doe');
INSERT INTO "Departments" VALUES (12,'HR','Jane Smith');
INSERT INTO "Departments" VALUES (13,'Logistics','Michael Johnson');
INSERT INTO "Departments" VALUES (14,'Finance','Emily Davis');
INSERT INTO "Departments" VALUES (15,'Marketing','William Brown');
INSERT INTO "Departments" VALUES (16,'IT','John Doe');
INSERT INTO "Departments" VALUES (17,'HR','Jane Smith');
INSERT INTO "Departments" VALUES (18,'Logistics','Michael Johnson');
INSERT INTO "Departments" VALUES (19,'Finance','Emily Davis');
INSERT INTO "Departments" VALUES (20,'Marketing','William Brown');
INSERT INTO "Departments" VALUES (21,'IT','John Doe');
INSERT INTO "Departments" VALUES (22,'HR','Jane Smith');
INSERT INTO "Departments" VALUES (23,'Logistics','Michael Johnson');
INSERT INTO "Departments" VALUES (24,'Finance','Emily Davis');
INSERT INTO "Departments" VALUES (25,'Marketing','William Brown');
INSERT INTO "EquipmentMovements" VALUES (1,1,1,2,1,'2023-12-01','Transfer',10,'Moved to HR');
INSERT INTO "EquipmentMovements" VALUES (2,2,2,3,2,'2023-11-25','Transfer',5,'Moved to Logistics');
INSERT INTO "EquipmentMovements" VALUES (3,3,3,4,3,'2023-11-20','Transfer',7,'Moved to Finance');
INSERT INTO "EquipmentMovements" VALUES (4,4,4,5,4,'2023-10-15','Transfer',2,'Moved to Marketing');
INSERT INTO "EquipmentMovements" VALUES (5,5,5,1,5,'2023-09-30','Transfer',15,'Moved to IT');
INSERT INTO "Equipments" VALUES (1,'Laptop',X'89504e470d0a1a0a',1,'1000','SN12345','2023-01-01','2025-01-01',1,1);
INSERT INTO "Equipments" VALUES (2,'Chair',X'89504e470d0a1a0a',2,'200','SN22345','2022-06-15','2024-06-15',2,2);
INSERT INTO "Equipments" VALUES (3,'Drill',X'89504e470d0a1a0a',3,'150','SN32345','2021-03-10','2023-03-10',3,3);
INSERT INTO "Equipments" VALUES (4,'Truck',X'89504e470d0a1a0a',4,'30000','SN42345','2020-09-05','2025-09-05',4,4);
INSERT INTO "Equipments" VALUES (5,'Monitor',X'89504e470d0a1a0a',5,'300','SN52345','2023-07-21','2025-07-21',5,5);
INSERT INTO "Equipments" VALUES (6,'Laptop',X'89504e470d0a1a0a',1,'1000','SN12345','2023-01-01','2025-01-01',1,1);
INSERT INTO "Equipments" VALUES (7,'Chair',X'89504e470d0a1a0a',2,'200','SN22345','2022-06-15','2024-06-15',2,2);
INSERT INTO "Equipments" VALUES (8,'Drill',X'89504e470d0a1a0a',3,'150','SN32345','2021-03-10','2023-03-10',3,3);
INSERT INTO "Equipments" VALUES (9,'Truck',X'89504e470d0a1a0a',4,'30000','SN42345','2020-09-05','2025-09-05',4,4);
INSERT INTO "Equipments" VALUES (10,'Monitor',X'89504e470d0a1a0a',5,'300','SN52345','2023-07-21','2025-07-21',5,5);
INSERT INTO "Equipments" VALUES (11,'Laptop',X'89504e470d0a1a0a',1,'1000','SN12345','2023-01-01','2025-01-01',1,1);
INSERT INTO "Equipments" VALUES (12,'Chair',X'89504e470d0a1a0a',2,'200','SN22345','2022-06-15','2024-06-15',2,2);
INSERT INTO "Equipments" VALUES (13,'Drill',X'89504e470d0a1a0a',3,'150','SN32345','2021-03-10','2023-03-10',3,3);
INSERT INTO "Equipments" VALUES (14,'Truck',X'89504e470d0a1a0a',4,'30000','SN42345','2020-09-05','2025-09-05',4,4);
INSERT INTO "Equipments" VALUES (15,'Monitor',X'89504e470d0a1a0a',5,'300','SN52345','2023-07-21','2025-07-21',5,5);
INSERT INTO "Maintenances" VALUES (1,1,'2023-07-10','Replaced battery');
INSERT INTO "Maintenances" VALUES (2,2,'2023-08-15','Fixed seat cushion');
INSERT INTO "Maintenances" VALUES (3,3,'2023-06-05','Repaired motor');
INSERT INTO "Maintenances" VALUES (4,4,'2023-05-25','Engine check');
INSERT INTO "Maintenances" VALUES (5,5,'2023-04-10','Calibration');
INSERT INTO "Maintenances" VALUES (6,1,'2023-07-10','Replaced battery');
INSERT INTO "Maintenances" VALUES (7,2,'2023-08-15','Fixed seat cushion');
INSERT INTO "Maintenances" VALUES (8,3,'2023-06-05','Repaired motor');
INSERT INTO "Maintenances" VALUES (9,4,'2023-05-25','Engine check');
INSERT INTO "Maintenances" VALUES (10,5,'2023-04-10','Calibration');
INSERT INTO "Roles" VALUES (1,'Admin','Administrator with full access');
INSERT INTO "Roles" VALUES (2,'Manager','Department manager');
INSERT INTO "Roles" VALUES (3,'Technician','Responsible for equipment maintenance');
INSERT INTO "Roles" VALUES (4,'Staff','General staff member');
INSERT INTO "Roles" VALUES (5,'Guest','Temporary user with limited access');
INSERT INTO "Roles" VALUES (6,'Admin','Administrator with full access');
INSERT INTO "Roles" VALUES (7,'Manager','Department manager');
INSERT INTO "Roles" VALUES (8,'Technician','Responsible for equipment maintenance');
INSERT INTO "Roles" VALUES (9,'Staff','General staff member');
INSERT INTO "Roles" VALUES (10,'Guest','Temporary user with limited access');
INSERT INTO "Statuses" VALUES (1,'Available','Equipment is available for use');
INSERT INTO "Statuses" VALUES (2,'In Use','Equipment is currently being used');
INSERT INTO "Statuses" VALUES (3,'Under Maintenance','Equipment is being repaired');
INSERT INTO "Statuses" VALUES (4,'Out of Service','Equipment is out of order');
INSERT INTO "Statuses" VALUES (5,'Reserved','Equipment is reserved for future use');
INSERT INTO "Statuses" VALUES (6,'Available','Equipment is available for use');
INSERT INTO "Statuses" VALUES (7,'In Use','Equipment is currently being used');
INSERT INTO "Statuses" VALUES (8,'Under Maintenance','Equipment is being repaired');
INSERT INTO "Statuses" VALUES (9,'Out of Service','Equipment is out of order');
INSERT INTO "Statuses" VALUES (10,'Reserved','Equipment is reserved for future use');
INSERT INTO "Statuses" VALUES (11,'Available','Equipment is available for use');
INSERT INTO "Statuses" VALUES (12,'In Use','Equipment is currently being used');
INSERT INTO "Statuses" VALUES (13,'Under Maintenance','Equipment is being repaired');
INSERT INTO "Statuses" VALUES (14,'Out of Service','Equipment is out of order');
INSERT INTO "Statuses" VALUES (15,'Reserved','Equipment is reserved for future use');
INSERT INTO "Stocks" VALUES (1,1,1,50);
INSERT INTO "Stocks" VALUES (2,2,2,30);
INSERT INTO "Stocks" VALUES (3,3,3,40);
INSERT INTO "Stocks" VALUES (4,4,4,10);
INSERT INTO "Stocks" VALUES (5,5,5,60);
INSERT INTO "Suppliers" VALUES (1,'Supplier A','contact@supplierA.com');
INSERT INTO "Suppliers" VALUES (2,'Supplier B','contact@supplierB.com');
INSERT INTO "Suppliers" VALUES (3,'Supplier C','contact@supplierC.com');
INSERT INTO "Suppliers" VALUES (4,'Supplier D','contact@supplierD.com');
INSERT INTO "Suppliers" VALUES (5,'Supplier E','contact@supplierE.com');
INSERT INTO "Suppliers" VALUES (6,'Supplier A','contact@supplierA.com');
INSERT INTO "Suppliers" VALUES (7,'Supplier B','contact@supplierB.com');
INSERT INTO "Suppliers" VALUES (8,'Supplier C','contact@supplierC.com');
INSERT INTO "Suppliers" VALUES (9,'Supplier D','contact@supplierD.com');
INSERT INTO "Suppliers" VALUES (10,'Supplier E','contact@supplierE.com');
INSERT INTO "Suppliers" VALUES (11,'Supplier A','contact@supplierA.com');
INSERT INTO "Suppliers" VALUES (12,'Supplier B','contact@supplierB.com');
INSERT INTO "Suppliers" VALUES (13,'Supplier C','contact@supplierC.com');
INSERT INTO "Suppliers" VALUES (14,'Supplier D','contact@supplierD.com');
INSERT INTO "Suppliers" VALUES (15,'Supplier E','contact@supplierE.com');
INSERT INTO "Transactions" VALUES (1,1,1,1,10,'Purchase','2023-01-01');
INSERT INTO "Transactions" VALUES (2,2,2,2,5,'Purchase','2023-06-15');
INSERT INTO "Transactions" VALUES (3,3,3,3,7,'Sale','2023-03-10');
INSERT INTO "Transactions" VALUES (4,4,4,4,2,'Purchase','2023-09-05');
INSERT INTO "Transactions" VALUES (5,5,5,5,15,'Sale','2023-07-21');
INSERT INTO "UserRoles" VALUES (1,1,1);
INSERT INTO "UserRoles" VALUES (2,2,2);
INSERT INTO "UserRoles" VALUES (3,3,3);
INSERT INTO "UserRoles" VALUES (4,4,4);
INSERT INTO "UserRoles" VALUES (5,5,5);
INSERT INTO "Users" VALUES (1,'john_doe','7C6A180B36896A0A8C02787EEAFB0E4C','john@example.com','2023-01-01',1,'John Doe');
INSERT INTO "Users" VALUES (2,'jane_smith','6CB75F652A9B52798EB6CF2201057C73','jane@example.com','2023-01-05',2,'Jane Smith');
INSERT INTO "Users" VALUES (3,'michael_johnson','819B0643D6B89DC9B579FDFC9094F28E','michael@example.com','2023-01-10',3,'Michael Johnson');
INSERT INTO "Users" VALUES (4,'emily_davis','34CC93ECE0BA9E3F6F235D4AF979B16C','emily@example.com','2023-01-15',4,'Emily Davis');
INSERT INTO "Users" VALUES (5,'william_brown','DB0EDD04AAAC4506F7EDAB03AC855D56','william@example.com','2023-01-20',5,'William Brown');
INSERT INTO "UtilizationRecords" VALUES (1,1,1,'2023-07-01','2023-07-10','Work on project A','N/A');
INSERT INTO "UtilizationRecords" VALUES (2,2,2,'2023-08-01','2023-08-15','Office setup','N/A');
INSERT INTO "UtilizationRecords" VALUES (3,3,3,'2023-06-01','2023-06-10','Maintenance task','N/A');
INSERT INTO "UtilizationRecords" VALUES (4,4,4,'2023-05-01','2023-05-25','Project work','N/A');
INSERT INTO "UtilizationRecords" VALUES (5,5,5,'2023-04-01','2023-04-10','Development work','N/A');
INSERT INTO "Warehouses" VALUES (1,'Warehouse A','Location 1');
INSERT INTO "Warehouses" VALUES (2,'Warehouse B','Location 2');
INSERT INTO "Warehouses" VALUES (3,'Warehouse C','Location 3');
INSERT INTO "Warehouses" VALUES (4,'Warehouse D','Location 4');
INSERT INTO "Warehouses" VALUES (5,'Warehouse E','Location 5');
CREATE INDEX "IX_EquipmentMovements_DestinationWarehouseID" ON "EquipmentMovements" ("DestinationWarehouseID");
CREATE INDEX "IX_EquipmentMovements_EquipmentID" ON "EquipmentMovements" ("EquipmentID");
CREATE INDEX "IX_EquipmentMovements_SourceWarehouseID" ON "EquipmentMovements" ("SourceWarehouseID");
CREATE INDEX "IX_EquipmentMovements_UserID" ON "EquipmentMovements" ("UserID");
CREATE INDEX "IX_Equipments_CategoryID" ON "Equipments" ("CategoryID");
CREATE INDEX "IX_Equipments_StatusID" ON "Equipments" ("StatusID");
CREATE INDEX "IX_Equipments_SupplierID" ON "Equipments" ("SupplierID");
CREATE INDEX "IX_Maintenances_EquipmentID" ON "Maintenances" ("EquipmentID");
CREATE INDEX "IX_Stocks_EquipmentID" ON "Stocks" ("EquipmentID");
CREATE INDEX "IX_Stocks_WarehouseID" ON "Stocks" ("WarehouseID");
CREATE INDEX "IX_Transactions_EquipmentID" ON "Transactions" ("EquipmentID");
CREATE INDEX "IX_Transactions_UserID" ON "Transactions" ("UserID");
CREATE INDEX "IX_Transactions_WarehouseID" ON "Transactions" ("WarehouseID");
CREATE INDEX "IX_UserRoles_RoleID" ON "UserRoles" ("RoleID");
CREATE INDEX "IX_Users_DepartmentID" ON "Users" ("DepartmentID");
CREATE INDEX "IX_UtilizationRecords_EquipmentID" ON "UtilizationRecords" ("EquipmentID");
CREATE INDEX "IX_UtilizationRecords_UserID" ON "UtilizationRecords" ("UserID");
COMMIT;
