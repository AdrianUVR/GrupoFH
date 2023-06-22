CREATE DATABASE GrupoFH2


CREATE TABLE Empleado 
(
IdEmpleado INT PRIMARY KEY IDENTITY (1,1),
NombreEmpleado VARCHAR (100), 
Usuario VARCHAR (100),
Password  VARCHAR (100),
Email VARCHAR (100),
Telefono VARCHAR (100)
) 

CREATE TABLE Error 
(
IdError INT PRIMARY KEY IDENTITY (1,1),
DescripcionE VARCHAR (100),
Paso1 VARCHAR (100),
paso2 VARCHAR (100),
paso3 VARCHAR (100)

)


CREATE TABLE Area 
(
IdArea INT PRIMARY KEY IDENTITY (1,1),
NombreArea VARCHAR (100)
)


CREATE TABLE Departamento 
(
IdDepartamento INT PRIMARY KEY IDENTITY (1,1),
NombreDepartamento VARCHAR (100),
DescripcionD VARCHAR (100) 
)


CREATE TABLE Ticket (
    IdTicket INT PRIMARY KEY IDENTITY (1,1),
	AsignadoA int ,
CerradoPor int ,
Comentarios VARCHAR (100),
FechaAsignacion DATETIME,
IdArea int,
 
    FOREIGN KEY (AsignadoA) REFERENCES Empleado(IdEmpleado),
	   FOREIGN KEY (CerradoPor) REFERENCES Empleado(IdEmpleado),
	   	   FOREIGN KEY (IdArea) REFERENCES Area(IdArea)
); 
ALTER TABLE Ticket
ADD Status bit; 

ALTER TABLE Departamento
ADD IdArea INT REFERENCES Area (IdArea)


ALTER TABLE Ticket
ADD IdError INT REFERENCES Error (IdError)


CREATE PROCEDURE GetEmpleado 
AS
SELECT 
IdEmpleado,
NombreEmpleado,
Usuario,
Password,
Email,
Telefono
FROM Empleado

CREATE PROCEDURE DeleteEmpleado
@IdEmpleado INT 
AS
DELETE Empleado
WHERE IdEmpleado=@IdEmpleado

CREATE PROCEDURE GetByIdEmpleado 
@IdEmpleado int
AS 
SELECT 
IdEmpleado,
NombreEmpleado,
Usuario,
Password,
Email,
Telefono
FROM Empleado
WHERE IdEmpleado=@IdEmpleado

CREATE PROCEDURE AddEmpleado 
@NombreEmpleado VARCHAR (100),
@Usuario VARCHAR (100),
@Password VARCHAR (100),
@Email VARCHAR (100),
@Telefono VARCHAR (100)
AS
INSERT Empleado
(
NombreEmpleado,
Usuario,
Password,
Email,
Telefono
)
VALUES
(
@NombreEmpleado ,
@Usuario ,
@Password,
@Email ,
@Telefono 
) 


CREATE PROCEDURE UpdateEmpleado 
@IdEmpleado int,
@NombreEmpleado VARCHAR (100),
@Usuario VARCHAR (100),
@Password VARCHAR (100),
@Email VARCHAR (100),
@Telefono VARCHAR (100)
AS
UPDATE Empleado 
SET 
NombreEmpleado=@NombreEmpleado,
Usuario=@Usuario,
Password=@Password,
Email=@Email,
Telefono=@Telefono

WHERE IdEmpleado=@IdEmpleado


CREATE PROCEDURE GetDepartamento 
AS
SELECT  
IdDepartamento,
NombreDepartamento,
DescripcionD,
Area.IdArea,
Area.NombreArea

FROM Departamento
INNER JOIN Area ON Departamento.IdArea=Area.IdArea


CREATE PROCEDURE DeleteDepartamento 
@IdDepartamento int
AS
DELETE Departamento 
WHERE IdDepartamento=@IdDepartamento

CREATE PROCEDURE GetByIdDepartamento 1
@IdDepartamento int 
AS
SELECT  
IdDepartamento,
NombreDepartamento,
DescripcionD,
Area.IdArea,
Area.NombreArea

FROM Departamento
INNER JOIN Area ON Departamento.IdArea=Area.IdArea
WHERE IdDepartamento=@IdDepartamento



CREATE PROCEDURE AddDepartamento
@NombreDepartamento VARCHAR (100),
@DescripcionD VARCHAR (100)
AS
INSERT Departamento
(
NombreDepartamento,
DescripcionD
)
VALUES 
(
@NombreDepartamento ,
@DescripcionD 

)


CREATE PROCEDURE UpdateDepartamento 
@IdDepartamento int,
@NombreDepartamento VARCHAR (100),
@DescripcionD VARCHAR (100)
AS
Update Departamento
SET NombreDepartamento=@NombreDepartamento,
DescripcionD=@DescripcionD
WHERE IdDepartamento=@IdDepartamento