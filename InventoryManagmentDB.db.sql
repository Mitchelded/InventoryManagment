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
CREATE TABLE IF NOT EXISTS "OrderDetails" (
    "OrderDetailID" INTEGER NOT NULL CONSTRAINT "PK_OrderDetails" PRIMARY KEY AUTOINCREMENT,
    "OrderID" INTEGER NOT NULL,
    "EquipmentID" INTEGER NOT NULL,
    "Quantity" INTEGER NOT NULL,
    "Notes" TEXT NULL,
    CONSTRAINT "FK_OrderDetails_Equipments_EquipmentID" FOREIGN KEY ("EquipmentID") REFERENCES "Equipments" ("EquipmentID") ON DELETE CASCADE,
    CONSTRAINT "FK_OrderDetails_Orders_OrderID" FOREIGN KEY ("OrderID") REFERENCES "Orders" ("OrderID") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "Orders" (
    "OrderID" INTEGER NOT NULL CONSTRAINT "PK_Orders" PRIMARY KEY AUTOINCREMENT,
    "UserID" INTEGER NOT NULL,
    "OrderDate" TEXT NOT NULL,
    "Notes" TEXT NULL,
    "TotalCost" TEXT NOT NULL,
    "ShippingAddress" TEXT NOT NULL,
    "CustomerName" TEXT NOT NULL,
    "CustomerEmail" TEXT NOT NULL,
    CONSTRAINT "FK_Orders_Users_UserID" FOREIGN KEY ("UserID") REFERENCES "Users" ("UserID") ON DELETE CASCADE
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
    "CompanyName" TEXT NOT NULL,
    "ContactInfo" TEXT NULL,
    "ContactPerson" TEXT NULL,
    "Email" TEXT NULL,
    "Phone" TEXT NULL,
    "Website" TEXT NULL,
    "Address" TEXT NULL,
    "CategoryID" INTEGER NOT NULL,
    CONSTRAINT "FK_Suppliers_Category_CategoryID" FOREIGN KEY ("CategoryID") REFERENCES "Category" ("CategoryID") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "Transactions" (
    "TransactionID" INTEGER NOT NULL CONSTRAINT "PK_Transactions" PRIMARY KEY AUTOINCREMENT,
    "EquipmentID" INTEGER NOT NULL,
    "WarehouseID" INTEGER NOT NULL,
    "UserID" INTEGER NULL,
    "Quantity" INTEGER NOT NULL,
    "OrderDetailID" INTEGER NOT NULL,
    "TransactionType" TEXT NOT NULL,
    "TransactionDate" TEXT NOT NULL,
    CONSTRAINT "FK_Transactions_Equipments_EquipmentID" FOREIGN KEY ("EquipmentID") REFERENCES "Equipments" ("EquipmentID") ON DELETE CASCADE,
    CONSTRAINT "FK_Transactions_OrderDetails_OrderDetailID" FOREIGN KEY ("OrderDetailID") REFERENCES "OrderDetails" ("OrderDetailID") ON DELETE CASCADE,
    CONSTRAINT "FK_Transactions_Users_UserID" FOREIGN KEY ("UserID") REFERENCES "Users" ("UserID"),
    CONSTRAINT "FK_Transactions_Warehouses_WarehouseID" FOREIGN KEY ("WarehouseID") REFERENCES "Warehouses" ("WarehouseID") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "UserRoles" (
    "UserRoleID" INTEGER NOT NULL PRIMARY KEY,
    "UserID" INTEGER NOT NULL,
    "RoleID" INTEGER NOT NULL,
    CONSTRAINT "FK_UserRoles_Users_UserID" FOREIGN KEY("UserID") REFERENCES "Users"("UserID") ON DELETE CASCADE,
    CONSTRAINT "FK_UserRoles_Roles_RoleID" FOREIGN KEY("RoleID") REFERENCES "Roles"("RoleID") ON DELETE CASCADE
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
CREATE TABLE IF NOT EXISTS "WarrantyClaims" (
    "WarrantyClaimID" INTEGER NOT NULL CONSTRAINT "PK_WarrantyClaims" PRIMARY KEY AUTOINCREMENT,
    "EquipmentID" INTEGER NOT NULL,
    "ClaimDate" TEXT NOT NULL,
    "IssueDescription" TEXT NOT NULL,
    "Resolution" TEXT NOT NULL,
    "ResolutionDate" TEXT NULL,
    "StatusID" INTEGER NOT NULL,
    CONSTRAINT "FK_WarrantyClaims_Equipments_EquipmentID" FOREIGN KEY ("EquipmentID") REFERENCES "Equipments" ("EquipmentID") ON DELETE CASCADE,
    CONSTRAINT "FK_WarrantyClaims_Statuses_StatusID" FOREIGN KEY ("StatusID") REFERENCES "Statuses" ("StatusID") ON DELETE CASCADE
);
INSERT INTO "Category" ("CategoryID","Name","Description") VALUES (1,'Очки','Различные виды очков и аксессуары');
INSERT INTO "Category" ("CategoryID","Name","Description") VALUES (2,'Линзы','Контактные линзы всех типов');
INSERT INTO "Category" ("CategoryID","Name","Description") VALUES (3,'Диагностическое оборудование','Приборы для проверки зрения');
INSERT INTO "Category" ("CategoryID","Name","Description") VALUES (4,'Расходные материалы','Средства ухода за линзами и очками');
INSERT INTO "Category" ("CategoryID","Name","Description") VALUES (5,'Аксессуары','Чехлы, салфетки и другие мелочи');
INSERT INTO "Departments" ("DepartmentID","Name","HeadOfDepartment") VALUES (1,'Продажи','Иванов Иван Иванович');
INSERT INTO "Departments" ("DepartmentID","Name","HeadOfDepartment") VALUES (2,'Складская логистика','Петров Петр Петрович');
INSERT INTO "Departments" ("DepartmentID","Name","HeadOfDepartment") VALUES (3,'Техническая поддержка','Сидоров Алексей Павлович');
INSERT INTO "Departments" ("DepartmentID","Name","HeadOfDepartment") VALUES (4,'Маркетинг','Морозова Инна Сергеевна');
INSERT INTO "Departments" ("DepartmentID","Name","HeadOfDepartment") VALUES (5,'Финансы','Кузнецов Олег Александрович');
INSERT INTO "EquipmentMovements" ("EquipmentMovementID","EquipmentID","SourceWarehouseID","DestinationWarehouseID","UserID","MovementDate","MovementType","Notes") VALUES (6,1,1,2,1,'2024-01-10','Перемещение','Перемещение оправ из центрального склада на склад в Москве');
INSERT INTO "EquipmentMovements" ("EquipmentMovementID","EquipmentID","SourceWarehouseID","DestinationWarehouseID","UserID","MovementDate","MovementType","Notes") VALUES (7,2,2,3,2,'2024-01-12','Перемещение','Перемещение линз из склада в Москве на склад в Питере');
INSERT INTO "EquipmentMovements" ("EquipmentMovementID","EquipmentID","SourceWarehouseID","DestinationWarehouseID","UserID","MovementDate","MovementType","Notes") VALUES (8,3,3,4,3,'2024-01-14','Перемещение','Перемещение ретиноскопов для технического обслуживания');
INSERT INTO "EquipmentMovements" ("EquipmentMovementID","EquipmentID","SourceWarehouseID","DestinationWarehouseID","UserID","MovementDate","MovementType","Notes") VALUES (9,4,1,5,4,'2024-01-16','Перемещение','Перемещение средств для линз на склад в Казани');
INSERT INTO "EquipmentMovements" ("EquipmentMovementID","EquipmentID","SourceWarehouseID","DestinationWarehouseID","UserID","MovementDate","MovementType","Notes") VALUES (10,5,5,1,5,'2024-01-18','Перемещение','Перемещение салфеток для очков на главный склад');
INSERT INTO "Equipments" ("EquipmentID","Name","Photo","CategoryID","Cost","SerialNumber","PurchaseDate","WarrantyExpiration","StatusID","SupplierID") VALUES (1,'Оправы классические',X'89504e470d0a1a0a0000000d49484452',1,'5000','FR12345','2023-01-15','2026-01-15',2,1);
INSERT INTO "Equipments" ("EquipmentID","Name","Photo","CategoryID","Cost","SerialNumber","PurchaseDate","WarrantyExpiration","StatusID","SupplierID") VALUES (2,'Линзы ежедневные',X'ffd8ffe000104a464946000101010048',2,'1200','LN67890','2023-03-01','2024-03-01',1,2);
INSERT INTO "Equipments" ("EquipmentID","Name","Photo","CategoryID","Cost","SerialNumber","PurchaseDate","WarrantyExpiration","StatusID","SupplierID") VALUES (3,'Ретиноскоп',X'47494638396101000100800000ffffff',3,'150000','RT112233','2022-06-20','2025-06-20',1,3);
INSERT INTO "Equipments" ("EquipmentID","Name","Photo","CategoryID","Cost","SerialNumber","PurchaseDate","WarrantyExpiration","StatusID","SupplierID") VALUES (4,'Средство для линз',X'424d3e00000000000000360000002800',4,'700','SM56473','2023-07-10','2024-07-10',1,4);
INSERT INTO "Equipments" ("EquipmentID","Name","Photo","CategoryID","Cost","SerialNumber","PurchaseDate","WarrantyExpiration","StatusID","SupplierID") VALUES (5,'Салфетка для очков',X'424d7605000000000000760000002800',5,'100','SF34567','2023-09-01','2024-09-01',1,5);
INSERT INTO "Maintenances" ("MaintenanceID","EquipmentID","MaintenanceDate","Notes") VALUES (11,1,'2024-01-15','Плановое обслуживание оправ. Замена витков и фиксация повреждений.');
INSERT INTO "Maintenances" ("MaintenanceID","EquipmentID","MaintenanceDate","Notes") VALUES (12,2,'2024-01-17','Плановое обслуживание линз. Проверка на дефекты и очистка от загрязнений.');
INSERT INTO "Maintenances" ("MaintenanceID","EquipmentID","MaintenanceDate","Notes") VALUES (13,3,'2024-01-20','Техническое обслуживание ретиноскопа. Калибровка и замена части деталей.');
INSERT INTO "Maintenances" ("MaintenanceID","EquipmentID","MaintenanceDate","Notes") VALUES (14,4,'2024-01-22','Плановое обслуживание средства для линз. Проверка на срок годности и упаковку.');
INSERT INTO "Maintenances" ("MaintenanceID","EquipmentID","MaintenanceDate","Notes") VALUES (15,5,'2024-01-23','Проверка и очистка салфеток для очков на складе.');
INSERT INTO "OrderDetails" ("OrderDetailID","OrderID","EquipmentID","Quantity","Notes") VALUES (1,1,1,10,'10 оправ классических для нового заказа');
INSERT INTO "OrderDetails" ("OrderDetailID","OrderID","EquipmentID","Quantity","Notes") VALUES (2,1,2,50,'50 упаковок линз для заказа в Москве');
INSERT INTO "OrderDetails" ("OrderDetailID","OrderID","EquipmentID","Quantity","Notes") VALUES (3,2,3,5,'5 ретиноскопов для медицинского учреждения');
INSERT INTO "OrderDetails" ("OrderDetailID","OrderID","EquipmentID","Quantity","Notes") VALUES (4,3,4,30,'30 упаковок средств для линз для розничной сети');
INSERT INTO "OrderDetails" ("OrderDetailID","OrderID","EquipmentID","Quantity","Notes") VALUES (5,4,5,100,'100 салфеток для очков для оптовой продажи');
INSERT INTO "Orders" ("OrderID","UserID","OrderDate","Notes","TotalCost","ShippingAddress","CustomerName","CustomerEmail") VALUES (1,1,'2024-01-15','Заказ очков для клиента','15000','г. Москва, ул. Ленина, д. 25','Мария Иванова','maria.ivanova@example.com');
INSERT INTO "Orders" ("OrderID","UserID","OrderDate","Notes","TotalCost","ShippingAddress","CustomerName","CustomerEmail") VALUES (2,2,'2024-01-16','Линзы для клиента','5000','г. Санкт-Петербург, ул. Некрасова, д. 12','Петр Смирнов','petr.smirnov@example.com');
INSERT INTO "Orders" ("OrderID","UserID","OrderDate","Notes","TotalCost","ShippingAddress","CustomerName","CustomerEmail") VALUES (3,3,'2024-01-17','Диагностическое оборудование','200000','г. Казань, ул. Чистопольская, д. 15','Сергей Сидоров','sergey.sid@example.com');
INSERT INTO "Orders" ("OrderID","UserID","OrderDate","Notes","TotalCost","ShippingAddress","CustomerName","CustomerEmail") VALUES (4,4,'2024-01-18','Средства для ухода','8000','г. Самара, ул. Гагарина, д. 20','Анна Морозова','anna.m@example.com');
INSERT INTO "Orders" ("OrderID","UserID","OrderDate","Notes","TotalCost","ShippingAddress","CustomerName","CustomerEmail") VALUES (5,5,'2024-01-19','Аксессуары','3000','г. Екатеринбург, ул. Мира, д. 30','Игорь Кузнецов','igor.k@example.com');
INSERT INTO "Roles" ("RoleID","Name","Description") VALUES (1,'Администратор','Administrator with full access');
INSERT INTO "Roles" ("RoleID","Name","Description") VALUES (2,'Менеджер','Department manager');
INSERT INTO "Roles" ("RoleID","Name","Description") VALUES (3,'Техническая поддержка','Responsible for equipment maintenance');
INSERT INTO "Roles" ("RoleID","Name","Description") VALUES (4,'Кладовщик','General staff member');
INSERT INTO "Statuses" ("StatusID","Name","Description") VALUES (1,'Доступно','Оборудование готово к использованию');
INSERT INTO "Statuses" ("StatusID","Name","Description") VALUES (2,'На складе','Хранится на складе');
INSERT INTO "Statuses" ("StatusID","Name","Description") VALUES (3,'В ремонте','Нуждается в ремонте');
INSERT INTO "Statuses" ("StatusID","Name","Description") VALUES (4,'Списано','Списано из использования');
INSERT INTO "Statuses" ("StatusID","Name","Description") VALUES (5,'Зарезервировано','Зарезервировано для клиента');
INSERT INTO "Stocks" ("StockID","EquipmentID","WarehouseID","Quantity") VALUES (6,1,1,100);
INSERT INTO "Stocks" ("StockID","EquipmentID","WarehouseID","Quantity") VALUES (7,2,2,500);
INSERT INTO "Stocks" ("StockID","EquipmentID","WarehouseID","Quantity") VALUES (8,3,3,20);
INSERT INTO "Stocks" ("StockID","EquipmentID","WarehouseID","Quantity") VALUES (9,4,4,150);
INSERT INTO "Stocks" ("StockID","EquipmentID","WarehouseID","Quantity") VALUES (10,5,5,300);
INSERT INTO "Suppliers" ("SupplierID","CompanyName","ContactInfo","ContactPerson","Email","Phone","Website","Address","CategoryID") VALUES (1,'ОптикМастер','info@opticmaster.ru','Иванов Иван','contact@opticmaster.ru','+7 495 123-45-67','www.opticmaster.ru','г. Москва, ул. Ленина, д. 1',1);
INSERT INTO "Suppliers" ("SupplierID","CompanyName","ContactInfo","ContactPerson","Email","Phone","Website","Address","CategoryID") VALUES (2,'Линзы.РФ','sales@linzy.rf','Петрова Анна','support@linzy.rf','+7 812 987-65-43','www.linzy.rf','г. Санкт-Петербург, ул. Пушкина, д. 2',2);
INSERT INTO "Suppliers" ("SupplierID","CompanyName","ContactInfo","ContactPerson","Email","Phone","Website","Address","CategoryID") VALUES (3,'ДиагностикаПлюс','diagplus@company.ru','Сидоров Олег','diagplus@company.ru','+7 495 321-54-76','www.diagplus.ru','г. Казань, ул. Чистопольская, д. 10',3);
INSERT INTO "Suppliers" ("SupplierID","CompanyName","ContactInfo","ContactPerson","Email","Phone","Website","Address","CategoryID") VALUES (4,'РасходникиПро','contact@rashpro.ru','Морозова Инна','contact@rashpro.ru','+7 499 876-54-32','www.rashpro.ru','г. Самара, ул. Гагарина, д. 18',4);
INSERT INTO "Suppliers" ("SupplierID","CompanyName","ContactInfo","ContactPerson","Email","Phone","Website","Address","CategoryID") VALUES (5,'АксессуарыОпт','info@accessories.ru','Кузнецов Максим','info@accessories.ru','+7 812 654-32-10','www.accessories.ru','г. Екатеринбург, ул. Мира, д. 22',5);
INSERT INTO "Transactions" ("TransactionID","EquipmentID","WarehouseID","UserID","Quantity","OrderDetailID","TransactionType","TransactionDate") VALUES (6,1,1,1,10,1,'Продажа','2024-01-10');
INSERT INTO "Transactions" ("TransactionID","EquipmentID","WarehouseID","UserID","Quantity","OrderDetailID","TransactionType","TransactionDate") VALUES (7,2,2,2,50,2,'Продажа','2024-01-12');
INSERT INTO "Transactions" ("TransactionID","EquipmentID","WarehouseID","UserID","Quantity","OrderDetailID","TransactionType","TransactionDate") VALUES (8,3,3,3,5,3,'Продажа','2024-01-14');
INSERT INTO "Transactions" ("TransactionID","EquipmentID","WarehouseID","UserID","Quantity","OrderDetailID","TransactionType","TransactionDate") VALUES (9,4,4,4,30,4,'Продажа','2024-01-16');
INSERT INTO "Transactions" ("TransactionID","EquipmentID","WarehouseID","UserID","Quantity","OrderDetailID","TransactionType","TransactionDate") VALUES (10,5,5,5,100,5,'Продажа','2024-01-18');
INSERT INTO "UserRoles" ("UserRoleID","UserID","RoleID") VALUES (1,1,1);
INSERT INTO "UserRoles" ("UserRoleID","UserID","RoleID") VALUES (2,2,2);
INSERT INTO "UserRoles" ("UserRoleID","UserID","RoleID") VALUES (3,3,3);
INSERT INTO "UserRoles" ("UserRoleID","UserID","RoleID") VALUES (4,4,4);
INSERT INTO "Users" ("UserID","Username","Password","Email","CreatedAt","DepartmentID","FullName") VALUES (1,'manager1','5a105e8b9d40e1329780d62ea2265d8a','manager1@optic.ru','2024-01-01',1,'Сидоров Алексей');
INSERT INTO "Users" ("UserID","Username","Password","Email","CreatedAt","DepartmentID","FullName") VALUES (2,'techsupport1','098f6bcd4621d373cade4e832627b4f6','techsupport1@optic.ru','2024-01-01',3,'Ковалев Сергей');
INSERT INTO "Users" ("UserID","Username","Password","Email","CreatedAt","DepartmentID","FullName") VALUES (3,'marketer1','d8578edf8458ce06fbc5bb76a58c5ca4','marketer1@optic.ru','2024-01-01',4,'Романова Анна');
INSERT INTO "Users" ("UserID","Username","Password","Email","CreatedAt","DepartmentID","FullName") VALUES (4,'finance1','db4bfc5e6eb42d65031e4253ebfcfbf0','finance1@optic.ru','2024-01-01',5,'Смирнов Иван');
INSERT INTO "Users" ("UserID","Username","Password","Email","CreatedAt","DepartmentID","FullName") VALUES (5,'logistics1','21232f297a57a5a743894a0e4a801fc3','logistics1@optic.ru','2024-01-01',2,'Васильев Дмитрий');
INSERT INTO "UtilizationRecords" ("UtilizationRecordID","EquipmentID","UserID","StartDate","EndDate","Purpose","Notes") VALUES (6,1,1,'2024-01-10','2024-01-15','Использование в ремонте оправ','Оправы использовались в процессе ремонта для клиента.');
INSERT INTO "UtilizationRecords" ("UtilizationRecordID","EquipmentID","UserID","StartDate","EndDate","Purpose","Notes") VALUES (7,2,2,'2024-01-12','2024-01-14','Использование для тестирования линз','Линзы использовались для тестирования качества в лаборатории.');
INSERT INTO "UtilizationRecords" ("UtilizationRecordID","EquipmentID","UserID","StartDate","EndDate","Purpose","Notes") VALUES (8,3,3,'2024-01-15','2024-01-20','Использование в клинических исследованиях','Ретиноскоп использовался для проведения клинических исследований в больнице.');
INSERT INTO "UtilizationRecords" ("UtilizationRecordID","EquipmentID","UserID","StartDate","EndDate","Purpose","Notes") VALUES (9,4,4,'2024-01-17','2024-01-20','Использование на складе','Средства для линз использовались для очистки на складе.');
INSERT INTO "UtilizationRecords" ("UtilizationRecordID","EquipmentID","UserID","StartDate","EndDate","Purpose","Notes") VALUES (10,5,5,'2024-01-18','2024-01-22','Использование в магазине','Салфетки для очков использовались в магазине для обслуживания клиентов.');
INSERT INTO "Warehouses" ("WarehouseID","Name","Location") VALUES (1,'Центральный склад','г. Москва, ул. Первомайская, д. 10');
INSERT INTO "Warehouses" ("WarehouseID","Name","Location") VALUES (2,'Склад филиала','г. Санкт-Петербург, ул. Большая Морская, д. 15');
INSERT INTO "Warehouses" ("WarehouseID","Name","Location") VALUES (3,'Склад в Казани','г. Казань, ул. Амирхана, д. 5');
INSERT INTO "Warehouses" ("WarehouseID","Name","Location") VALUES (4,'Склад в Самаре','г. Самара, ул. Советской Армии, д. 45');
INSERT INTO "Warehouses" ("WarehouseID","Name","Location") VALUES (5,'Склад в Екатеринбурге','г. Екатеринбург, ул. Карла Маркса, д. 7');
INSERT INTO "WarrantyClaims" ("WarrantyClaimID","EquipmentID","ClaimDate","IssueDescription","Resolution","ResolutionDate","StatusID") VALUES (6,1,'2024-01-15','Неисправность механизма оправ','Заменены детали механизма','2024-01-20',2);
INSERT INTO "WarrantyClaims" ("WarrantyClaimID","EquipmentID","ClaimDate","IssueDescription","Resolution","ResolutionDate","StatusID") VALUES (7,2,'2024-01-18','Дефект покрытия линз','Линзы заменены на новые','2024-01-22',3);
INSERT INTO "WarrantyClaims" ("WarrantyClaimID","EquipmentID","ClaimDate","IssueDescription","Resolution","ResolutionDate","StatusID") VALUES (8,3,'2024-01-20','Поломка ретиноскопа','Ремонт устройства',NULL,1);
INSERT INTO "WarrantyClaims" ("WarrantyClaimID","EquipmentID","ClaimDate","IssueDescription","Resolution","ResolutionDate","StatusID") VALUES (9,4,'2024-01-22','Неэффективность средства для линз','Заменено средство','2024-01-25',2);
INSERT INTO "WarrantyClaims" ("WarrantyClaimID","EquipmentID","ClaimDate","IssueDescription","Resolution","ResolutionDate","StatusID") VALUES (10,5,'2024-01-25','Повреждение упаковки салфеток для очков','Заменены поврежденные упаковки','2024-01-28',3);
CREATE INDEX "IX_EquipmentMovements_DestinationWarehouseID" ON "EquipmentMovements" ("DestinationWarehouseID");
CREATE INDEX "IX_EquipmentMovements_EquipmentID" ON "EquipmentMovements" ("EquipmentID");
CREATE INDEX "IX_EquipmentMovements_SourceWarehouseID" ON "EquipmentMovements" ("SourceWarehouseID");
CREATE INDEX "IX_EquipmentMovements_UserID" ON "EquipmentMovements" ("UserID");
CREATE INDEX "IX_Equipments_CategoryID" ON "Equipments" ("CategoryID");
CREATE INDEX "IX_Equipments_StatusID" ON "Equipments" ("StatusID");
CREATE INDEX "IX_Equipments_SupplierID" ON "Equipments" ("SupplierID");
CREATE INDEX "IX_Maintenances_EquipmentID" ON "Maintenances" ("EquipmentID");
CREATE INDEX "IX_OrderDetails_EquipmentID" ON "OrderDetails" ("EquipmentID");
CREATE INDEX "IX_OrderDetails_OrderID" ON "OrderDetails" ("OrderID");
CREATE INDEX "IX_Orders_UserID" ON "Orders" ("UserID");
CREATE INDEX "IX_Stocks_EquipmentID" ON "Stocks" ("EquipmentID");
CREATE INDEX "IX_Stocks_WarehouseID" ON "Stocks" ("WarehouseID");
CREATE INDEX "IX_Suppliers_CategoryID" ON "Suppliers" ("CategoryID");
CREATE INDEX "IX_Transactions_EquipmentID" ON "Transactions" ("EquipmentID");
CREATE INDEX "IX_Transactions_OrderDetailID" ON "Transactions" ("OrderDetailID");
CREATE INDEX "IX_Transactions_UserID" ON "Transactions" ("UserID");
CREATE INDEX "IX_Transactions_WarehouseID" ON "Transactions" ("WarehouseID");
CREATE INDEX "IX_Users_DepartmentID" ON "Users" ("DepartmentID");
CREATE INDEX "IX_UtilizationRecords_EquipmentID" ON "UtilizationRecords" ("EquipmentID");
CREATE INDEX "IX_UtilizationRecords_UserID" ON "UtilizationRecords" ("UserID");
CREATE INDEX "IX_WarrantyClaims_EquipmentID" ON "WarrantyClaims" ("EquipmentID");
CREATE INDEX "IX_WarrantyClaims_StatusID" ON "WarrantyClaims" ("StatusID");
COMMIT;
