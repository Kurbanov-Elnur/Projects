create table Cars (
    [Id] binary(32) primary key default (HASHBYTES('SHA2_256', CONVERT(NVARCHAR(36), NEWID()))),
    [Brand] nvarchar(MAX) not null check (LEN([Brand]) >= 3 and [Brand] NOT LIKE '%[^A-Za-z]%'),
    [Model] nvarchar(MAX) not null check ([Model] NOT LIKE '%[^A-Za-z0-9]%'),
    [Year] int not null check ([Year] >= (YEAR(GETDATE()) - 30) and [Year] <= YEAR(GETDATE())),
    [Fuel_Type_ID] binary(32) not null foreign key references FuelTypes([Id]),
    [Body_Type_ID] binary(32) not null foreign key references BodyTypes([Id]),
    [Color_ID] binary(32) not null foreign key references Colors([Id]),
    [Image_link] nvarchar(max) default N'https://www.pngall.com/wp-content/uploads/5/Vehicle.png'
);

create table Users(
    [Id] binary(32) primary key default (HASHBYTES('SHA2_256', CONVERT(NVARCHAR(36), NEWID()))),
    [Name] nvarchar(max) not null check (LEN([Name]) >= 3 and [Name] NOT LIKE '% %'),
    [Password] nvarchar(30) not null check (LEN([Password]) >= 8 and [Password] NOT LIKE '% %'),
    [Email] nvarchar(30) not null check ([Email] LIKE '%[a-zA-Z0-9._%+-]@[a-zA-Z0-9.-]%'),
);

create table ProductList(
    [Id] binary(32) primary key default (HASHBYTES('SHA2_256', CONVERT(NVARCHAR(36), NEWID()))),
    [Car_ID] binary(32) not null foreign key references Cars([Id]),
    [Seller_ID] binary(32) not null foreign key references Sellers([Id]),
    [Price] smallmoney not null check ([Price] >= 0),
    [Quantity] smallint not null check ([Quantity] >= 0)
);

create table ManufacturingCountries(
    [Id] binary(32) primary key default (HASHBYTES('SHA2_256', CONVERT(NVARCHAR(36), NEWID()))),
    [Country_name] nvarchar(MAX) not null check ([Country_name] NOT LIKE '%[^A-Z a-z]%')
);

create table FuelTypes(
    [Id] binary(32) primary key default (HASHBYTES('SHA2_256', CONVERT(NVARCHAR(36), NEWID()))),
    [Fuel_type] nvarchar(MAX) not null check ([Fuel_type] NOT LIKE '%[^A-Za-z]%')
);

create table BodyTypes(
    [Id] binary(32) primary key default (HASHBYTES('SHA2_256', CONVERT(NVARCHAR(36), NEWID()))),
    [Body_type] nvarchar(MAX) not null check ([Body_type] NOT LIKE '%[^A-Za-z]%')
);

create table Colors(
    [Id] binary(32) primary key default (HASHBYTES('SHA2_256', CONVERT(NVARCHAR(36), NEWID()))),
    [Color_name] nvarchar(MAX) not null check ([Color_name] NOT LIKE '%[^A-Za-z]%')
);

create table Sellers(
    [Id] binary(32) primary key default (HASHBYTES('SHA2_256', CONVERT(NVARCHAR(36), NEWID()))),
    [User_ID] binary(32) not null foreign key references Users([Id]),
    [Company_name] nvarchar(MAX) not null check ([Company_name] NOT LIKE '%[^A-Z a-z]%') default N'--',
    [Contact_number] nvarchar(MAX) not null check ([Contact_number] NOT LIKE '%[0-9 ]%'),
    [Country_ID] binary(32) not null foreign key references ManufacturingCountries([Id]),
    [Rating] tinyint not null check ([Rating] >= 1 and [Rating] <= 5)
);