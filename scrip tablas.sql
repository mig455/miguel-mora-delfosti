use Delfosti
drop table if exists Tareas;
drop table if exists Projects;
drop table if exists Users;
drop table if exists Roles;
-- Crear tabla de Roles
CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME  NULL,
    CreatedBy INT  NULL,
    ModifiedBy INT  NULL,
	Estado BIT NOT NULL
);
GO

-- Insertar roles 
INSERT INTO Roles (Name, CreatedDate, CreatedBy,Estado) 
VALUES ('Administrador', GETDATE(), NULL,1), ('Consumidor', GETDATE(), NULL,1);
GO

-- Crear tabla de Usuarios
drop table if exists Users;
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL,
    PasswordHash VARBINARY(MAX) NOT NULL,
    PasswordSalt VARBINARY(MAX) NOT NULL,
    Email NVARCHAR(256) NOT NULL UNIQUE, 
    RoleId INT NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME  NULL,
    CreatedBy INT  NULL,
    ModifiedBy INT  NULL,
	Estado BIT NOT NULL,
    FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);
GO

-- Crear tabla de Proyectos
CREATE TABLE Projects (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    CreatedBy INT NOT NULL,
    ModifiedBy INT NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME  NULL,
	Estado BIT NOT NULL,
    FOREIGN KEY (CreatedBy) REFERENCES Users(Id),
    FOREIGN KEY (ModifiedBy) REFERENCES Users(Id)
);
GO

-- Crear tabla de Tareas
CREATE TABLE Tareas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProjectId INT NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000) NULL,
    Status NVARCHAR(50) NOT NULL CHECK (Status IN ('Pendiente', 'En Progreso', 'Completado')),
    CreatedBy INT  NULL,
    ModifiedBy INT  NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME  NULL,
	Estado BIT NOT NULL,
    FOREIGN KEY (ProjectId) REFERENCES Projects(Id),
    FOREIGN KEY (CreatedBy) REFERENCES Users(Id),
    FOREIGN KEY (ModifiedBy) REFERENCES Users(Id)
);
GO



select * from  Tareas;
select * from  Projects;
select * from  Users;
select * from  Roles;